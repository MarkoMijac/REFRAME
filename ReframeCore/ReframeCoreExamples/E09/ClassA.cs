using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreExamples.E09
{
    public class ClassA
    {
        public string Name { get; set; }

        public int PA1 { get; set; }
        public int PA2 { get; set; }
        public int PA3 { get; set; }

        public ClassA()
        {

        }

        public ClassA(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
