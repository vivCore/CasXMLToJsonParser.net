using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cTakesXMLParser.Model
{
    public class UmlsConcept
    {
        public int Id { get; set; }
        public long Code { get; set; }
        public string CodingScheme { get; set; }
        public decimal Score { get; set; }
        public string CUI { get; set; }
        public string TUI { get; set; }
        public string PreferredText { get; set; }
    }
}
