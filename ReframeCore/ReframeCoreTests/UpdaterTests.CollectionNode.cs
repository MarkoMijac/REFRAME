using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCore.Helpers;
using ReframeCore.Nodes;
using ReframeCoreExamples.E08.E2;

namespace ReframeCoreTests
{
    public partial class UpdaterTests
    {
        #region CASE_8_2

        /**
             * Example diagram file: CollectionNodeSimpleScenarios
             * SCENARIJ S1
                ------------------------
                * Postoji kolekcijski čvor (npr. {Stavke, "Iznos"}).
                * Ne postoje pojedinačni čvorovi za parove {Stavka, "Iznos"}
                iz kolekcije
                * Kolekcijski čvor je izvorišni (source) čvor
                ------------------------
                Kad korisnik unese "Iznos" bilo koje stavke u kolekciji,
                pokreće se proces ažuriranja sa kolekcijskim čvorom kao početnim.
        */

        private Tuple<IDependencyGraph, Updater, Order_8_2> CreateCase_8_2()
        {
            var graph = new DependencyGraph("GRAPH_8_2");
            var updater = CreateUpdater(graph);

            var order = new Order_8_2();
            order.Items.Add(new OrderItem_8_2(updater));
            order.Items.Add(new OrderItem_8_2(updater));
            order.Items.Add(new OrderItem_8_2(updater));

            INode orderTotal = graph.AddNode(order, "Total");
            INode orderTotalVAT = graph.AddNode(order, "TotalVAT");
            INode itemsTotal = graph.AddNode(order.Items, "Total");

            graph.AddDependency(itemsTotal, orderTotal);
            graph.AddDependency(itemsTotal, orderTotalVAT);

            graph.Initialize();

            return new Tuple<IDependencyGraph, Updater, Order_8_2>(graph, updater, order);
        }

        private UpdateLogger CreateExpectedLogger_Case_8_2_PerformCompleteUpdate(IDependencyGraph graph, Order_8_2 order)
        {
            UpdateLogger logger = new UpdateLogger();

            logger.Log(graph.GetNode(order.Items, "Total"));
            logger.Log(graph.GetNode(order, "TotalVAT"));
            logger.Log(graph.GetNode(order, "Total"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_2_PerformCompleteUpdate_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var caseParameters = CreateCase_8_2();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;

            //Act
            updater.PerformUpdate();

            //Assert
            UpdateLogger actualLog = updater.NodeLog;
            UpdateLogger expectedLog = CreateExpectedLogger_Case_8_2_PerformCompleteUpdate(graph, order);

            Assert.AreEqual(expectedLog, actualLog);
        }

        #endregion
    }
}
