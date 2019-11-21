using ReframeCore;
using ReframeCore.Factories;
using ReframeCore.Nodes;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreExamples.E08.E1
{
    public class Whole_8_1
    {
        private IDependencyGraph graph;

        #region Properties

        public ReactiveCollection<Part_8_1> Parts { get; set; }

        public string Name { get; set; }
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public int D { get; set; }

        private int _coeffA;

        public int CoeffA
        {
            get { return _coeffA; }
            set
            {
                _coeffA = value;
                graph.PerformUpdate(this, "CoeffA");
            }
        }

        private int _coeffB;

        public int CoeffB
        {
            get { return _coeffB; }
            set
            {
                _coeffB = value;
                graph.PerformUpdate(this, "CoeffB");
            }
        }

        private int _coeffC;

        public int CoeffC
        {
            get
            {
                return _coeffC;
            }
            set
            {
                _coeffC = value;
                graph.PerformUpdate(this, "CoeffC");
            }
        }

        #endregion

        #region Methods

        public Whole_8_1()
        {
            graph = GraphRegistry.Instance.Get("GRAPH_CASE_8_1");

            CoeffA = 1;
            CoeffB = 2;
            CoeffC = 3;

            Parts = new ReactiveCollection<Part_8_1>();
            Parts.Add(new Part_8_1 { Name = "Part 1", A = 1, B = 2, C = 3});
            Parts.Add(new Part_8_1 { Name = "Part 2", A = 4, B = 5, C = 6 });
            Parts.Add(new Part_8_1 { Name = "Part 3", A = 7, B = 8, C = 9 });

            InitializeDependencies();
        }

        private void InitializeDependencies()
        {
            INode coeffA = graph.AddNode(this, "CoeffA");
            INode coeffB = graph.AddNode(this, "CoeffB");
            INode coeffC = graph.AddNode(this, "CoeffC");
            INode a = graph.AddNode(this, "A");
            INode b = graph.AddNode(this, "B");
            INode c = graph.AddNode(this, "C");
            INode d = graph.AddNode(this, "D");

            graph.AddDependency(coeffA, a);
            graph.AddDependency(coeffB, b);
            graph.AddDependency(coeffC, c);

            graph.AddDependency(a, b);
            graph.AddDependency(b, c);
            graph.AddDependency(b, d);
            graph.AddDependency(c, d);

            INode partsA = graph.AddNode(Parts, "A");
            INode partsB = graph.AddNode(Parts, "B");
            INode partsC = graph.AddNode(Parts, "C");

            graph.AddDependency(partsA, a);
            graph.AddDependency(partsB, b);
            graph.AddDependency(partsC, c);

            graph.Initialize();
        }

        private void Update_A()
        {
            A = 0;
            foreach (var p in Parts)
            {
                A += p.A;
            }

            A = CoeffA * A;
        }

        private void Update_B()
        {
            B = 0;
            foreach (var p in Parts)
            {
                B += p.B;
            }

            B = CoeffB * B + A;
        }

        private void Update_C()
        {
            C = 0;
            foreach (var p in Parts)
            {
                C += p.C;
            }

            C = CoeffC * C + B;
        }

        private void Update_D()
        {
            D = B + C;
        }

        #endregion

    }
}
