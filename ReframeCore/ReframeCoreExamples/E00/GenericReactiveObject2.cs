﻿using ReframeCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreExamples.E00
{
    public class GenericReactiveObject2
    {
        #region Properties

        private IDependencyGraph _graph;

        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public int D { get; set; }
        public int E { get; set; }
        public int F { get; set; }
        public int G { get; set; }
        public int H { get; set; }
        public int I { get; set; }
        public int J { get; set; }

        #endregion

        #region Methods

        public GenericReactiveObject2()
        {
            _graph = GraphFactory.Get("GRAPH_CASE_9");
        }

        public void Update_A()
        {
            _graph.PerformUpdate(this, "Update_A");
        }

        private void Update_B()
        {
            _graph.PerformUpdate(this, "Update_B");
        }

        private void Update_C()
        {
            _graph.PerformUpdate(this, "Update_C");
        }

        private void Update_D()
        {
            _graph.PerformUpdate(this, "Update_D");
        }

        private void Update_E()
        {
            _graph.PerformUpdate(this, "Update_E");
        }

        public void Update_F()
        {
            _graph.PerformUpdate(this, "Update_F");
        }

        private void Update_G()
        {
            _graph.PerformUpdate(this, "Update_G");
        }

        private void Update_H()
        {
            _graph.PerformUpdate(this, "Update_H");
        }

        private void Update_I()
        {
            _graph.PerformUpdate(this, "Update_I");
        }

        private void Update_J()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
