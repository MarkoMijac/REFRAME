using ReframeAnalyzer.Filters;
using ReframeAnalyzer.Graph;
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
    public partial class FrmAnalysisFilter : Form
    {
        public List<IAnalysisNode> OriginalNodes { get; set; }

        public IAnalysisFilter Filter { get; set; }

        public FrmAnalysisFilter()
        {
            InitializeComponent();
        }

        protected void FillListBoxes(CheckedListBox listBox, IFilterOption filterOption)
        {
            listBox.Items.Clear();

            foreach (var node in filterOption.GetNodes())
            {
                bool checkedItem = filterOption.IsSelected(node);
                listBox.Items.Add(node, checkedItem);
            }
        }

        protected void FillListBoxes(CheckedListBox listBox, IFilterOption filterOption, Predicate<IAnalysisNode> condition)
        {
            listBox.Items.Clear();

            foreach (var node in filterOption.GetNodes(condition))
            {
                bool checkedItem = filterOption.IsSelected(node);
                listBox.Items.Add(node, checkedItem);
            }
        }

        protected void CheckListBoxItem(CheckedListBox listBox, IFilterOption filterOption, ItemCheckEventArgs e)
        {
            IAnalysisNode node = listBox.SelectedItem as IAnalysisNode;

            if (e.NewValue == CheckState.Checked)
            {
                filterOption.SelectNode(node);
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                filterOption.DeselectNode(node);
            }
        }
    }
}
