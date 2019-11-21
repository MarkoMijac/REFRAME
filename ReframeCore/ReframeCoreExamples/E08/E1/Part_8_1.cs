using ReframeCore;
using ReframeCore.Factories;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreExamples.E08.E1
{
    public class Part_8_1 : ICollectionNodeItem
    {
        private IDependencyGraph graph;

        public event EventHandler UpdateTriggered;

        #region Properties

        public string Name { get; set; }

        private int _a;

        public int A
        {
            get { return _a; }
            set
            {
                _a = value;
                graph.PerformUpdate(this, "A");
            }
        }

        private int _b;

        public int B
        {
            get { return _b; }
            set
            {
                _b = value;
                graph.PerformUpdate(this, "B");
            }
        }

        private int _c;

        public int C
        {
            get { return _c; }
            set
            {
                _c = value;
                graph.PerformUpdate(this, "C");
            }
        }


        #endregion

        #region Methods

        public Part_8_1()
        {
            graph = GraphRegistry.Instance.Get("GRAPH_CASE_8_1");
        }

        #endregion
    }
}
