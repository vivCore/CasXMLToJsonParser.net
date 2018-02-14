using cTakesXMLParser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasXMLToJsonParser.Model
{
    public class CasJsonStructure
    {
        public string ClinicalTerm { get; set; }
        public List<List<ClinicalMention>> Mentions { get; set; }

    }
}
