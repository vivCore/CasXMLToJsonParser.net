using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cTakesXMLParser.Model
{
    public class FSArray
    {
        public int Id { get; set; }
        public int Size { get; set; }
        public List<int> ConceptIdList { get; set; }
    }
}
