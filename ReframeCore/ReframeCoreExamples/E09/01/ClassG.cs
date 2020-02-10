using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreExamples.E09._01
{
    public class ClassG
    {
        public string Name { get; set; }

        public int PG1 { get; set; }
        public int PG2 { get; set; }
        public int PG3 { get; set; }

        public ClassG()
        {

        }

        public ClassG(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
