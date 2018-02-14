using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cTakesXMLParser.Model
{
    public class ClinicalMention
    {
        public int? Id { get; set; }
        public string MentionName { get; set; }
        public int RefSofa { get; set; }
        public int Begin { get; set; }
        public int End { get; set; }
        public int RefOntologyConceptArr { get; set; }
        public int TypeID { get; set; }

        public List<UmlsConcept> UmlsConcepts { get; set; }

    }
}
