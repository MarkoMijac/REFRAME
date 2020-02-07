using ReframeAnalyzer.Graph;
using ReframeTools.Controllers;
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
    public partial class FrmReactorDetails : Form
    {
        public string ReactorIdentifier { get; private set; }

        public string GraphIdentifier { get; set; }
        public string GraphTotalNodeCount { get; set; }
        public string GraphSourceNodesCount { get; set; }
        public string GraphSinkNodesCount { get; set; }
        public string GraphIntermediateNodesCount { get; set; }
        public string GraphOrphanNodesCount { get; set; }
        public string GraphNumOfDependencies { get; set; }
        public string GraphMaxNumberOfDependencies { get; set; }
        public string GraphDensity { get; set; }
        public string ObjectsCount { get; set; }
        public string ClassesCount { get; set; }
        public string MembersCount { get; set; }
        public string NamespacesCount { get; set; }
        public string AssembliesCount { get; set; }


        public FrmReactorDetails(string reactorIdentifier)
        {
            InitializeComponent();
            ReactorIdentifier = reactorIdentifier;
        }

        private void FrmReactorDetails_Load(object sender, EventArgs e)
        {
            ReactorDetailsController controller = new ReactorDetailsController(this);
            controller.ShowReactorDetails();
        }

        public void DisplayBasicInfo(IAnalysisGraph objectMemberGraph)
        {
            txtGraphIdentifier.Text = objectMemberGraph.Identifier;
            txtGraphTotalNodeCount.Text = objectMemberGraph.Nodes.Count().ToString();
        }

        public void DisplayDetails()
        {
            txtGraphIdentifier.Text = GraphIdentifier;
            txtGraphTotalNodeCount.Text = GraphTotalNodeCount;
            txtSourceNodesCount.Text = GraphSourceNodesCount;
            txtSinkNodesCount.Text = GraphSinkNodesCount;
            txtIntermediateNodesCount.Text = GraphIntermediateNodesCount;
            txtOrphanNodesCount.Text = GraphOrphanNodesCount;

            txtNumberOfDependencies.Text = GraphNumOfDependencies;
            txtMaxNumOfDependencies.Text = GraphMaxNumberOfDependencies;
            txtGraphDensity.Text = GraphDensity;

            txtNumberOfObjects.Text = ObjectsCount;
            txtNumberOfClasses.Text = ClassesCount;
            txtNumberOfMembers.Text = MembersCount;
            txtNumberOfNamespaces.Text = NamespacesCount;
            txtNumberOfAssemblies.Text = AssembliesCount;
        }
    }
}
