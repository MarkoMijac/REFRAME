using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreExamples.E09
{
    public class ClassC
    {
        public string Name { get; set; }

        public int PC1 { get; set; }
        public int PC2 { get; set; }
        public int PC3 { get; set; }

        public ClassC()
        {

        }

        public ClassC(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
