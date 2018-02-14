using CasXMLToJsonParser.Model;
using cTakesXMLParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using log4net;
namespace CasXMLToJsonParser
{
    class Program
    {
        static XmlDocument xmlDoc = new XmlDocument();

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
        (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            //args[0] = "C:\\apache-ctakes-4.0.0\\ctakes\\output";
            ILog log = log4net.LogManager.GetLogger(typeof(Program));
            log.Info("=========================================================== ");
            log.Info("Starting CAS XML To Json File Conversion Utility.");
            if (args.Count() > 0)
            {
                var directoryName = args[0].ToString();
                if (Directory.Exists(directoryName))
                {
                    log.Info("Found Directory to process CAS XML files: " + directoryName);
                    foreach (var file in Directory.GetFiles(directoryName, "*.xml"))
                    {
                        if (File.Exists(file))
                        {
                            log.Info("Processing file: " + file);
                            if (FileCasXMLType(file))
                            {
                                var data = ParseCasXMLToJson(file.ToString());
                                log.Info("XML File Parsed.");
                                var jsonSerializeText = JsonConvert.SerializeObject(data);

                                var newJsonFileName = file + ".json";

                                if (File.Exists(newJsonFileName))
                                {
                                    log.Info("File Exists, Deleted: " + newJsonFileName);
                                    File.Delete(newJsonFileName);
                                }

                                File.WriteAllText(newJsonFileName, jsonSerializeText);

                                log.Info("Json File Created: " + newJsonFileName);

                            }
                            else
                            {
                                Console.WriteLine(file + " - is not a valid CAS xml file.");
                            }

                        }
                    }
                }
                else
                {
                    Console.Write("Directory you have provided doesn't exists");
                    log.Error(directoryName + ", directory name you have provided doesn't exists.");
                    return;
                }
                log.Info("Process Completed.");

                Console.Write("Process Completed.");
            }
            else
            {
                Console.WriteLine("Please provide CAS XML Directory Path.");
            }
        }

        private static bool FileCasXMLType(string file)
        {
            xmlDoc.Load(file);
            if (xmlDoc.SelectNodes("//CAS").Count > 0)
            {
                log.Info("Verification Passed. File " + file + " is a cTakes CAS XML file.");

                return true;
            }
            else
            {
                log.Error("Verification Failed. File " + file + " is not a cTakes CAS XML file");
                return false;
            }
        }

        static List<CasJsonStructure> ParseCasXMLToJson(string fileName)
        {

            List<CasJsonStructure> casJsonStructureList = new List<CasJsonStructure>();
            var casJsonStructure = new CasJsonStructure();
            xmlDoc.Load(fileName);

            var IdentifiedWords = xmlDoc.SelectNodes("//org.apache.ctakes.typesystem.type.syntax.ConllDependencyNode");

            foreach (XmlNode node in IdentifiedWords)
            {
                if (node.Attributes["form"] != null)
                {
                    var begin = Convert.ToInt32(node.Attributes["begin"].Value);
                    var clinicalTerm = node.Attributes["form"].Value;


                    if (clinicalTerm != null && (checkWordExistsinMentions(begin, "MedicationMention")
                        || checkWordExistsinMentions(begin, "DiseaseDisorderMention")
                        || checkWordExistsinMentions(begin, "SignSymptomMention")
                        || checkWordExistsinMentions(begin, "ProcedureMention")
                        || checkWordExistsinMentions(begin, "AnatomicalSiteMention")
                        ))
                    {
                        var termExists = casJsonStructureList.Where(w => w.ClinicalTerm.ToLower() == clinicalTerm.ToLower()).FirstOrDefault();

                        if (termExists != null)
                        {
                            var mentionList = GetAllMentionList(begin);

                            foreach (var mentions in mentionList)
                            {
                                termExists.Mentions.Add(mentions);
                            }

                           // casJsonStructureList.Add(termExists);
                        }
                        else
                        {
                            casJsonStructure = new CasJsonStructure
                            {
                                ClinicalTerm = clinicalTerm,
                                Mentions = GetAllMentionList(begin)
                            };
                            casJsonStructureList.Add(casJsonStructure);
                        }

                    }
                }

            }
            return casJsonStructureList;
        }

        private static List<List<ClinicalMention>> GetAllMentionList(int begin)
        {
            var mentionList = new List<List<ClinicalMention>>();
            var mentions = new List<ClinicalMention>();
            mentions = GetMentionList(begin, "MedicationMention");
            // log.Info("Medication Mention Found: " + mentions.Count);
            if (mentions.Count > 0)
                mentionList.Add(mentions);

            mentions = new List<ClinicalMention>();
            mentions = GetMentionList(begin, "DiseaseDisorderMention");
            //log.Info("DiseaseDisorder Mention Found: " + mentions.Count);
            if (mentions.Count > 0)
            {
                mentionList.Add(mentions);
            }

            mentions = new List<ClinicalMention>();
            mentions = GetMentionList(begin, "SignSymptomMention");
            //log.Info("SignSymptom Mention Found: " + mentions.Count);
            if (mentions.Count > 0)
                mentionList.Add(mentions);

            mentions = new List<ClinicalMention>();
            mentions = GetMentionList(begin, "ProcedureMention");
            //log.Info("Procedure Mention Found: " + mentions.Count);
            if (mentions.Count > 0)
                mentionList.Add(mentions);

            mentions = new List<ClinicalMention>();
            mentions = GetMentionList(begin, "AnatomicalSiteMention");
            //log.Info("AnatomicalSite Mention Found: " + mentions.Count);
            if (mentions.Count > 0)
                mentionList.Add(mentions);

            return mentionList;
        }

        private static List<ClinicalMention> GetMentionList(int begin, string mentionName)
        {
            var mentionList = new List<ClinicalMention>();
            var mention = new ClinicalMention();

            var nodes = xmlDoc.SelectNodes("//org.apache.ctakes.typesystem.type.textsem." + mentionName + "[@begin=" + begin + "]");
            if (nodes.Count > 0)
            {
                foreach (XmlNode mentionElement in nodes)
                {
                    mention = new ClinicalMention
                    {
                        Id = Convert.ToInt32(mentionElement.Attributes["_id"].Value),
                        RefSofa = Convert.ToInt32(mentionElement.Attributes["_ref_sofa"].Value),
                        Begin = Convert.ToInt32(mentionElement.Attributes["begin"].Value),
                        End = Convert.ToInt32(mentionElement.Attributes["end"].Value),
                        RefOntologyConceptArr = Convert.ToInt32(mentionElement.Attributes["_ref_ontologyConceptArr"].Value),
                        TypeID = Convert.ToInt32(mentionElement.Attributes["typeID"].Value),
                        MentionName = mentionName,
                        UmlsConcepts = GetUMLSConcepts(Convert.ToInt32(mentionElement.Attributes["_ref_ontologyConceptArr"].Value))
                    };
                    mentionList.Add(mention);
                }
            }

            return mentionList;


        }

        private static List<UmlsConcept> GetUMLSConcepts(int refOntologyConceptArr)
        {
            var fsArrary = GetFSArray(refOntologyConceptArr);
            var umlsConcepts = new List<UmlsConcept>();
            var umlsConcept = new UmlsConcept();

            foreach (var Id in fsArrary.Select(s => s.ConceptIdList).FirstOrDefault())
            {
                var nodeList = xmlDoc.SelectNodes("//org.apache.ctakes.typesystem.type.refsem.UmlsConcept[@_id=" + Id + "]");
                foreach (XmlNode node in nodeList)
                {
                    umlsConcept = new UmlsConcept
                    {

                        Id = Convert.ToInt32(node.Attributes["_id"].Value),
                        Code = Convert.ToInt32(node.Attributes["code"].Value),
                        CUI = node.Attributes["cui"].Value,
                        TUI = node.Attributes["tui"].Value,
                        PreferredText = node.Attributes["preferredText"].Value,
                        Score = Convert.ToDecimal(node.Attributes["score"].Value),
                        CodingScheme = node.Attributes["codingScheme"].Value
                    };
                    umlsConcepts.Add(umlsConcept);
                }
            }

            return umlsConcepts;
        }

        private static List<FSArray> GetFSArray(int FSArrayId)
        {
            var fsArrayList = new List<FSArray>();
            var fsArray = new FSArray();
            var elementName = "uima.cas.FSArray";
            var nodeList = xmlDoc.SelectNodes("//" + elementName + "[@_id=" + FSArrayId + "]");

            foreach (XmlNode mentionElement in nodeList)
            {

                fsArray = new FSArray
                {
                    Id = Convert.ToInt32(mentionElement.Attributes["_id"].Value),
                    Size = Convert.ToInt32(mentionElement.Attributes["size"].Value),

                };

                var childNodeList = new List<int>();
                foreach (XmlNode child in mentionElement.ChildNodes)
                {
                    childNodeList.Add(Convert.ToInt32(child.InnerText));
                }
                fsArray.ConceptIdList = childNodeList;
                fsArrayList.Add(fsArray);
            };

            return fsArrayList;

        }

        static bool checkWordExistsinMentions(int begin, string mentionName)
        {
            var elementName = "org.apache.ctakes.typesystem.type.textsem." + mentionName;
            var node = xmlDoc.SelectNodes("//" + elementName + "[@begin=" + begin + "]");

            if (node.Count > 0)
                return true;
            else
                return false;
        }
    }
}
