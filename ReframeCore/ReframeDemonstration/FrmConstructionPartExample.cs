using ReframeCore;
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
    public partial class FrmConstructionPartExample : Form
    {
        private BindingSource constructionPartsBindingSource = new BindingSource();

        public FrmConstructionPartExample()
        {
            InitializeComponent();
        }

        private void FrmConstructionPartExample_Load(object sender, EventArgs e)
        {
            DisplayConstructionParts();
        }

        private void DefaultGraph_UpdateCompleted(object sender, EventArgs e)
        {
            if (ParentForm.ActiveMdiChild == this || ActiveForm == this)
            {
                RefreshGUI();
            }
        }

        private void DisplayConstructionParts()
        {
            GUIManager.ShowObject(null);

            dgvConstructionParts.DataSource = constructionPartsBindingSource;
            constructionPartsBindingSource.DataSource = Repository.ConstructionParts;
        }

        private void dgvConstructionParts_SelectionChanged(object sender, EventArgs e)
        {
            ConstructionPart cPart = constructionPartsBindingSource.Current as ConstructionPart;
            GUIManager.ShowObject(cPart);
        }

        private void RefreshGUI()
        {
            constructionPartsBindingSource.ResetBindings(false);
            dgvConstructionParts.Refresh();
            GUIManager.MainPropertyGrid.Refresh();
        }

        private void btnAddConstructionPart_Click(object sender, EventArgs e)
        {
            /* SCENARIO 1:Initial values are set programmatically after the constructor (in which dependencies are created),
             * so REFRAME correctly performs update. However, since we change two properties, it performs update two times, i.e. after each value change.
             * For example: (1) create object and make changes to values
            */

            ConstructionPart part = new ConstructionPart() { Width = 2, Height = 3 };
            Repository.ConstructionParts.Add(part);

            /* SCENARIO 2::Initial values are set programmatically after the constructor (in which dependencies are created),
             * so REFRAME correctly performs update. However, since we change two properties, it performs update two times.
             * In cases where we programatically want to set up multiple values for properties, it might be reasonable to
             * to apply optimization. For example: (1) temporarily suspend updates, (2) create object and make changes to values, (3) resume updates, (4) invoke update manually.
            */

            //(DependencyManager.DefaultGraph as DependencyGraph).UpdateSuspended = true;
            //ConstructionPart part = new ConstructionPart() { Width = 2, Height = 3 };
            //Repository.ConstructionParts.Add(part);
            //(DependencyManager.DefaultGraph as DependencyGraph).UpdateSuspended = false;
            //DependencyManager.DefaultGraph.PerformUpdate();

            /* SCENARIO 3: No properties which represent reactive nodes are set programmatically after dependencies are created.
             * Instead changes to properties values are intended to be made by user, through GUI. Therefore, there is no need to suspend updates.
             * For example: (1) create object and make changes do values, (2) invoke update manually
             */

            //Repository.ConstructionParts.Add(new ConstructionPart());
            //DependencyManager.DefaultGraph.PerformUpdate();

            /* SCENARIO 4: Initial values for instantiated object are set, but not through reactive nodes. Therefore there is no need to suspend updates.
             * For example: (1) create object and pass initial values through constructor parameters, (2) invoke update manually.
             */

            //Repository.ConstructionParts.Add(new ConstructionPart(3, 2.5));
            //DependencyManager.DefaultGraph.PerformUpdate();

            /* SCENARIO 5: Initial values for instantiated object are set in constructor through reactive nodes. In the constructor statements for
             * suspending and resuming update are used. Therefore there is no need to suspend update outside of the class.
             * For example: (1) create object and set initial values within constructor, (2) wrap initializing statements in constructor with suspend/resume update statements (3) unvoke update manually.
             */

            //Repository.ConstructionParts.Add(new ConstructionPart(true));
            //DependencyManager.DefaultGraph.PerformUpdate();

            /* SCENARIO 6: If for example repository collections would be declared as reactive collections, then manual update could be avoided when adding object to collection.
             */

            RefreshGUI();
        }

        private void btnShowLayers_Click(object sender, EventArgs e)
        {
            ConstructionPart cPart = dgvConstructionParts.CurrentRow.DataBoundItem as ConstructionPart;
            GUIManager.ShowForm(new FrmLayers(cPart));
        }

        private void FrmConstructionPartExample_Activated(object sender, EventArgs e)
        {
            DependencyManager.DefaultReactor.UpdateCompleted += DefaultGraph_UpdateCompleted;
            RefreshGUI();
        }

        private void FrmConstructionPartExample_Deactivate(object sender, EventArgs e)
        {
            DependencyManager.DefaultReactor.UpdateCompleted -= DefaultGraph_UpdateCompleted;
        }
    }
}
