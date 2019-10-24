using ReframeCore;
using ReframeCore.FluentAPI;
using ReframeDemonstration.BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReframeDemonstration
{
    public partial class FrmLayers : Form
    {
        private ConstructionPart constructionPart;
        private BindingSource layersBindingSource = new BindingSource();

        public FrmLayers(ConstructionPart cPart)
        {
            InitializeComponent();
            constructionPart = cPart;
        }

        private void FrmLayers_Load(object sender, EventArgs e)
        {
            DisplayLayers();
        }

        private void DefaultGraph_UpdateCompleted(object sender, EventArgs e)
        {
            if (ParentForm.ActiveMdiChild == this || ActiveForm == this)
            {
                RefreshGUI();
            }
        }

        private void DisplayLayers()
        {
            GUIManager.ShowObject(null);

            dgvLayers.DataSource = layersBindingSource;
            layersBindingSource.DataSource = constructionPart.Layers;
        }

        private void RefreshGUI()
        {
            layersBindingSource.ResetBindings(false);
            dgvLayers.Refresh();
            GUIManager.MainPropertyGrid.Refresh();
        }

        private void dgvLayers_SelectionChanged(object sender, EventArgs e)
        {
            Layer layer = layersBindingSource.Current as Layer;
            GUIManager.ShowObject(layer);
        }

        private void btnAddLayer_Click(object sender, EventArgs e)
        {
            /* SCENARIO 1: No reactive nodes properties are set during instantiation. Therefore, there is
             * no need to suspend update, and there is no need to manually update. When the object is added to ReactiveCollection, update is invoked automatically. 
             * For example: (1) create object and add it to reactive collection
             */

            constructionPart.Layers.Add(new Layer() { Name = "New Layer"});

            /* SCENARIO 2: 
             * 
             */

            //(DependencyManager.DefaultGraph as DependencyGraph).UpdateSuspended = true;
            //constructionPart.Layers.Add(new Layer() { Name = "New Layer", Thickness = 1});
            //(DependencyManager.DefaultGraph as DependencyGraph).UpdateSuspended = false;
            //DependencyManager.DefaultGraph.PerformUpdate();

            RefreshGUI();
        }

        private void btnRemoveLayer_Click(object sender, EventArgs e)
        {
            Layer layer = dgvLayers.CurrentRow.DataBoundItem as Layer;
            constructionPart.Layers.Remove(layer);
        }

        private void FrmLayers_Activated(object sender, EventArgs e)
        {
            DependencyManager.DefaultGraph.UpdateCompleted += DefaultGraph_UpdateCompleted;
            RefreshGUI();
        }

        private void FrmLayers_Deactivate(object sender, EventArgs e)
        {
            DependencyManager.DefaultGraph.UpdateCompleted -= DefaultGraph_UpdateCompleted;
        }
    }
}
