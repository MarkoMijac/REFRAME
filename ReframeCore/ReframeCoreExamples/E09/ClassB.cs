using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreExamples.E09
{
    public class ClassB
    {
        public string Name { get; set; }

        public int PB1 { get; set; }
        public int PB2 { get; set; }
        public int PB3 { get; set; }

        public ClassB()
        {

        }

        public ClassB(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
