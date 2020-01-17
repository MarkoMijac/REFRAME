using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReframeTools.GUI
{
    public partial class FrmClassLevelAnalysis : FrmAnalysis
    {
        public FrmClassLevelAnalysis(string reactorIdentifier)
            :base(reactorIdentifier)
        {
            InitializeComponent();
        }

        protected override void SetFormTitle()
        {
            Text = $"Class-level analysis for Reactor [{ReactorIdentifier}]";
            ShowDescription($"Class-level analysis for Reactor [{ReactorIdentifier}]");
        }
    }
}
