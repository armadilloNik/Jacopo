using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jacopo.Tests
{
    public class ParsingTests
    {
    }

    public class JacapoComponent
    {
        public string Name { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Properties { get; set; }  
 
        public IEnumerable<JacapoComponent> SubComponents { get; set; }

        /*

        deck -> card // 
		card: 2
		card: 3


        so when parsing this:
            A Component with name = deck that has many Subcomponents named card
                so i could create a closure over 'createComponent' that takes a name, but allows properties & components to be added pottentially
    */

    }
}
