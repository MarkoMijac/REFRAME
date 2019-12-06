using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCore.Helpers;
using ReframeCore.Nodes;
using ReframeCoreExamples.E08.E2;
using System.Threading.Tasks;
using ReframeCoreExamples.E08.E3;
using ReframeCoreExamples.E08.E4;
using ReframeCoreExamples.E08.E5;
using ReframeCoreExamples.E08.E6;
using ReframeCoreExamples.E08.E7;
using ReframeCoreExamples.E08.E8;
using ReframeCore.Factories;

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
            updater.SuspendUpdate();

            var order = new Order_8_2();
            order.Items.Add(new OrderItem_8_2(updater));
            order.Items.Add(new OrderItem_8_2(updater));
            order.Items.Add(new OrderItem_8_2(updater));

            INode orderTotal = graph.AddNode(order, "Total");
            INode orderTotalVAT = graph.AddNode(order, "TotalVAT");
            INode itemsTotal = graph.AddNode(order.Items, "Total");

            graph.AddDependency(itemsTotal, orderTotal);
            graph.AddDependency(itemsTotal, orderTotalVAT);

            updater.ResumeUpdate();

            return new Tuple<IDependencyGraph, Updater, Order_8_2>(graph, updater, order);
        }

        private NodeLog CreateExpectedLogger_Case_8_2_PerformCompleteUpdate(IDependencyGraph graph, Order_8_2 order)
        {
            NodeLog logger = new NodeLog();

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
            NodeLog actualLog = updater.NodeLog;
            NodeLog expectedLog = CreateExpectedLogger_Case_8_2_PerformCompleteUpdate(graph, order);

            Assert.AreEqual(expectedLog, actualLog);
        }

        [TestMethod]
        public async Task PerformUpdate_Case_8_2_PerformCompleteUpdateInSeparateThread_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var caseParameters = CreateCase_8_2();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;

            //Act
            updater.AsynchronousUpdateEnabled = true;
            await updater.PerformUpdate();

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case_8_2_PerformCompleteUpdate(graph, order);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_2_PerformCompleteUpdate_GivesCorrectResults()
        {
            //Arrange
            var caseParameters = CreateCase_8_2();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;

            //Act
            updater.PerformUpdate();

            //Assert
            Assert.IsTrue(order.Total == 3 && order.TotalVAT == 3.75);
        }

        private NodeLog CreateExpectedLogger_Case_8_2_GivenItemTotalIsChanged(IDependencyGraph graph, Order_8_2 order)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(order, "TotalVAT"));
            logger.Log(graph.GetNode(order, "Total"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_2_GivenItemTotalIsChanged_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var caseParameters = CreateCase_8_2();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;
            updater.PerformUpdate();

            //Act
            order.Items[1].Total = 2;

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case_8_2_GivenItemTotalIsChanged(graph, order);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_2_GivenItemTotalIsChanged_GivesCorrectResults()
        {
            //Arrange
            var caseParameters = CreateCase_8_2();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;
            updater.PerformUpdate();

            //Act
            order.Items[1].Total = 2;

            //Assert
            Assert.IsTrue(order.Total == 4 && order.TotalVAT == 5);
        }

        #endregion

        #region CASE_8_3

        /**
             * Example diagram file: CollectionNodeSimpleScenarios
             * SCENARIJ S2
            ------------------------
            * Postoji kolekcijski čvor (npr. {Stavke, "Iznos"}).
            * Ne postoje pojedinačni čvorovi za parove {Stavka, "Iznos"}
            iz kolekcije
            * Kolekcijski čvor je završni čvor (sink node)
            ------------------------
            Kada korisnik unese npr. "PopustA", pokreće se proces
            ažuriranja, te se kolekcijski čvor ažurira tako da ažurira
            svaki objekt u kolekciji.
        */

        private Tuple<IDependencyGraph, Updater, Order_8_3> CreateCase_8_3()
        {
            var graph = new DependencyGraph("GRAPH_8_3");
            var updater = CreateUpdater(graph);
            updater.SuspendUpdate();

            var order = new Order_8_3(updater);
            order.Items.Add(new OrderItem_8_3(order, updater) { FixedValue = 10 });
            order.Items.Add(new OrderItem_8_3(order, updater) { FixedValue = 20 });
            order.Items.Add(new OrderItem_8_3(order, updater) { FixedValue = 30 });

            INode discountA = graph.AddNode(order, "DiscountA");
            INode discountB = graph.AddNode(order, "DiscountB");
            INode itemsTotal = graph.AddNode(order.Items, "Total");

            graph.AddDependency(discountA, itemsTotal);
            graph.AddDependency(discountB, itemsTotal);

            updater.ResumeUpdate();

            return new Tuple<IDependencyGraph, Updater, Order_8_3>(graph, updater, order);
        }

        private NodeLog CreateExpectedLogger_Case_8_3_PerformCompleteUpdate(IDependencyGraph graph, Order_8_3 order)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(order, "DiscountB"));
            logger.Log(graph.GetNode(order, "DiscountA"));
            logger.Log(graph.GetNode(order.Items, "Total"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_3_PerformCompleteUpdate_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var caseParameters = CreateCase_8_3();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;

            //Act
            updater.PerformUpdate();

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case_8_3_PerformCompleteUpdate(graph, order);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_3_PerformCompleteUpdate_GivesCorrectResults()
        {
            //Arrange
            var caseParameters = CreateCase_8_3();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;

            //Act
            updater.PerformUpdate();

            //Assert
            Assert.IsTrue(order.Items[0].Total == 10 && order.Items[1].Total == 20 && order.Items[2].Total == 30);
        }

        private NodeLog CreateExpectedLogger_Case_8_3_GivenDiscountAIsChanged(IDependencyGraph graph, Order_8_3 order)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(order.Items, "Total"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_3_GivenDiscountAIsChanged_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var caseParameters = CreateCase_8_3();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;
            updater.PerformUpdate();

            //Act
            order.DiscountA = 10;

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case_8_3_GivenDiscountAIsChanged(graph, order);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_3_GivenDiscountAIsChanged_GivesCorrectResults()
        {
            //Arrange
            var caseParameters = CreateCase_8_3();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;
            updater.PerformUpdate();

            //Act
            order.DiscountA = 10;

            //Assert
            Assert.IsTrue(order.Items[0].Total == 9 && order.Items[1].Total == 18 && order.Items[2].Total == 27);
        }

        private NodeLog CreateExpectedLogger_Case_8_3_GivenDiscountBIsChanged(IDependencyGraph graph, Order_8_3 order)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(order.Items, "Total"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_3_GivenDiscountBIsChanged_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var caseParameters = CreateCase_8_3();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;
            updater.PerformUpdate();

            //Act
            order.DiscountB = 20;

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case_8_3_GivenDiscountBIsChanged(graph, order);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_3_GivenDiscountBIsChanged_GivesCorrectResults()
        {
            //Arrange
            var caseParameters = CreateCase_8_3();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;
            updater.PerformUpdate();

            //Act
            order.DiscountB = 20;

            //Assert
            Assert.IsTrue(order.Items[0].Total == 8 && order.Items[1].Total == 16 && order.Items[2].Total == 24);
        }

        #endregion

        #region CASE_8_4

        /*
         * Example diagram file: CollectionNodeSimpleScenarios
         SCENARIJ S3
        ------------------------
        * Postoji kolekcijski čvor (npr. {Stavke, "Iznos"}).
        * Ne postoje pojedinačni čvorovi za parove {Stavka, "Iznos"}
        iz kolekcije
        * Kolekcijski čvor je među-čvor (intermediary node)
        ------------------------
        Kada korisnik unese npr. "PopustA", pokreće se proces
        ažuriranja, u kojem u topološkom sortiranju sudjeluje i kolekcijski čvor.
        Kada dođe na red kolekcijski čvor se ažurira tako da ažurira svaki objekt u kolekciji.
         */

        private Tuple<IDependencyGraph, Updater, Order_8_4> CreateCase_8_4()
        {
            var graph = new DependencyGraph("GRAPH_8_4");
            var updater = CreateUpdater(graph);
            updater.SuspendUpdate();

            var order = new Order_8_4(updater);
            order.Items.Add(new OrderItem_8_4(order, updater) { FixedValue = 10 });
            order.Items.Add(new OrderItem_8_4(order, updater) { FixedValue = 20 });
            order.Items.Add(new OrderItem_8_4(order, updater) { FixedValue = 30 });

            INode orderTotal = graph.AddNode(order, "Total");
            INode orderTotalVAT = graph.AddNode(order, "TotalVAT");
            INode orderDiscountA = graph.AddNode(order, "DiscountA");
            INode orderDiscountB = graph.AddNode(order, "DiscountB");
            INode itemsTotal = graph.AddNode(order.Items, "Total");

            graph.AddDependency(orderDiscountA, itemsTotal);
            graph.AddDependency(orderDiscountB, itemsTotal);
            graph.AddDependency(itemsTotal, orderTotal);
            graph.AddDependency(itemsTotal, orderTotalVAT);

            updater.ResumeUpdate();

            return new Tuple<IDependencyGraph, Updater, Order_8_4>(graph, updater, order);
        }

        private NodeLog CreateExpectedLogger_Case_8_4_PerformCompleteUpdate(IDependencyGraph graph, Order_8_4 order)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(order, "DiscountB"));
            logger.Log(graph.GetNode(order, "DiscountA"));
            logger.Log(graph.GetNode(order.Items, "Total"));
            logger.Log(graph.GetNode(order, "TotalVAT"));
            logger.Log(graph.GetNode(order, "Total"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_4_PerformCompleteUpdate_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var caseParameters = CreateCase_8_4();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;

            //Act
            updater.PerformUpdate();

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case_8_4_PerformCompleteUpdate(graph, order);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_4_PerformCompleteUpdate_GivesCorrectResults()
        {
            //Arrange
            var caseParameters = CreateCase_8_4();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;

            //Act
            updater.PerformUpdate();

            //Assert
            Assert.IsTrue(order.Items[0].Total == 10 && order.Items[1].Total == 20 && order.Items[2].Total == 30
                && order.Total == 60 && order.TotalVAT == 75);
        }

        private NodeLog CreateExpectedLogger_Case_8_4_GivenDiscountAIsChanged(IDependencyGraph graph, Order_8_4 order)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(order.Items, "Total"));
            logger.Log(graph.GetNode(order, "TotalVAT"));
            logger.Log(graph.GetNode(order, "Total"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_4_GivenDiscountAIsChanged_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var caseParameters = CreateCase_8_4();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;
            updater.PerformUpdate();

            //Act
            order.DiscountA = 10;

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case_8_4_GivenDiscountAIsChanged(graph, order);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_4_GivenDiscountAIsChanged_GivesCorrectResults()
        {
            //Arrange
            var caseParameters = CreateCase_8_4();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;
            updater.PerformUpdate();

            //Act
            order.DiscountA = 10;

            //Assert
            Assert.IsTrue(order.Items[0].Total == 9 && order.Items[1].Total == 18 && order.Items[2].Total == 27
                && order.Total == 54 && order.TotalVAT == 67.5);
        }

        private NodeLog CreateExpectedLogger_Case_8_4_GivenDiscountBIsChanged(IDependencyGraph graph, Order_8_4 order)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(order.Items, "Total"));
            logger.Log(graph.GetNode(order, "TotalVAT"));
            logger.Log(graph.GetNode(order, "Total"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_4_GivenDiscountBIsChanged_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var caseParameters = CreateCase_8_4();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;
            updater.PerformUpdate();

            //Act
            order.DiscountB = 20;

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case_8_4_GivenDiscountBIsChanged(graph, order);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_4_GivenDiscountBIsChanged_GivesCorrectResults()
        {
            //Arrange
            var caseParameters = CreateCase_8_4();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;
            updater.PerformUpdate();

            //Act
            order.DiscountB = 20;

            //Assert
            Assert.IsTrue(order.Items[0].Total == 8 && order.Items[1].Total == 16 && order.Items[2].Total == 24
                && order.Total == 48 && order.TotalVAT == 60);
        }

        #endregion

        #region CASE_8_5

        /*
         * Example diagram file: CollectionNodeAdvancedScenario1
         SCENARIJ A1
        ------------------------
        * Postoji kolekcijski čvor (npr. {Stavke, "Iznos"}).
        * Postoje i pojedinačni čvorovi za parove {Stavka, "Iznos"}
        iz kolekcije
        * Pojedinačni čvorovi  {Stavka, "Iznos"} su (virtualni) prethodnici isključivo kolekcijskom čvoru
        * Kolekcijski čvor nema drugih prethodnika
        * Kolekcijski čvor je među-čvor (ne može biti izvorišni čvor).
        ------------------------
        Kad korisnik unese "Kolicina" bilo koje stavke u kolekciji,
        pokreće se proces ažuriranja sa {Stavka, "Kolicina"} čvorom kao početnim.
        Kolekcijski čvor{Stavke, "Iznos"} sudjeluje u topološkom sortiranju, ali
        kada dođe red na njega ne ažurira svoje objekte, jer su oni kao prethodnici
        već ažurirani (moglo bi se pojednostaviti pa ipak ažurirati sve redundantno).
        S obzirom da je pojedinačni čvor {Stavka, "Iznos"} samo virtualno prethodnik,
        trebalo bi ga eksplicitno dodati kao prethodnika kolekcijskom čvoru tijekom
        procesa ažuriranja.
         */

        private Tuple<IDependencyGraph, Updater, Order_8_5> CreateCase_8_5()
        {
            var graph = new DependencyGraph("GRAPH_8_5");
            var updater = CreateUpdater(graph);
            updater.SuspendUpdate();

            var order = new Order_8_5();
            order.Items.Add(new OrderItem_8_5(updater) { Amount = 1, UnitPrice = 3.5 });
            order.Items.Add(new OrderItem_8_5(updater) { Amount = 2, UnitPrice = 4.5 });

            INode item1_amount = graph.AddNode(order.Items[0], "Amount");
            INode item2_amount = graph.AddNode(order.Items[1], "Amount");

            INode item1_unitPrice = graph.AddNode(order.Items[0], "UnitPrice");
            INode item2_unitPrice = graph.AddNode(order.Items[1], "UnitPrice");

            INode item1_total = graph.AddNode(order.Items[0], "Total");
            INode item2_total = graph.AddNode(order.Items[1], "Total");

            INode itemsTotal = graph.AddNode(order.Items, "Total");

            INode orderTotal = graph.AddNode(order, "Total");
            INode orderTotalVAT = graph.AddNode(order, "TotalVAT");

            graph.AddDependency(item1_amount, item1_total);
            graph.AddDependency(item1_unitPrice, item1_total);

            graph.AddDependency(item2_amount, item2_total);
            graph.AddDependency(item2_unitPrice, item2_total);

            graph.AddDependency(itemsTotal, orderTotal);
            graph.AddDependency(itemsTotal, orderTotalVAT);

            updater.ResumeUpdate();

            return new Tuple<IDependencyGraph, Updater, Order_8_5>(graph, updater, order);
        }

        private NodeLog CreateExpectedLogger_Case_8_5_PerformCompleteUpdate(IDependencyGraph graph, Order_8_5 order)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(order.Items[1], "UnitPrice"));
            logger.Log(graph.GetNode(order.Items[0], "UnitPrice"));
            logger.Log(graph.GetNode(order.Items[1], "Amount"));
            logger.Log(graph.GetNode(order.Items[1], "Total"));
            logger.Log(graph.GetNode(order.Items[0], "Amount"));
            logger.Log(graph.GetNode(order.Items[0], "Total"));
            logger.Log(graph.GetNode(order.Items, "Total"));

            logger.Log(graph.GetNode(order, "TotalVAT"));
            logger.Log(graph.GetNode(order, "Total"));


            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_5_PerformCompleteUpdate_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var caseParameters = CreateCase_8_5();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;

            //Act
            updater.PerformUpdate();

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case_8_5_PerformCompleteUpdate(graph, order);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_5_PerformCompleteUpdate_GivesCorrectResults()
        {
            //Arrange
            var caseParameters = CreateCase_8_5();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;

            //Act
            updater.PerformUpdate();

            //Assert
            OrderItem_8_5 oItem1 = order.Items[0];
            OrderItem_8_5 oItem2 = order.Items[1];

            Assert.IsTrue(oItem1.Amount == 1 && oItem1.UnitPrice == 3.5 && oItem1.Total == 3.5 &&
                oItem2.Amount == 2 && oItem2.UnitPrice == 4.5 && oItem2.Total == 9 &&
                order.Total == 12.5 && order.TotalVAT == 15.625);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_5_GivenAmountIsChanged_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var caseParameters = CreateCase_8_5();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;
            updater.PerformUpdate();

            //Act
            OrderItem_8_5 item = order.Items[0];
            item.Amount = 2;

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case_8_5_GivenAmountIsChanged(graph, order);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        private NodeLog CreateExpectedLogger_Case_8_5_GivenAmountIsChanged(IDependencyGraph graph, Order_8_5 order)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(order.Items[0], "Total"));
            logger.Log(graph.GetNode(order.Items, "Total"));

            logger.Log(graph.GetNode(order, "TotalVAT"));
            logger.Log(graph.GetNode(order, "Total"));


            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_5_GivenAmountIsChanged_GivesCorrectResults()
        {
            //Arrange
            var caseParameters = CreateCase_8_5();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;
            updater.PerformUpdate();

            OrderItem_8_5 oItem1 = order.Items[0];
            OrderItem_8_5 oItem2 = order.Items[1];

            //Act
            oItem1.Amount = 2;

            //Assert
            Assert.IsTrue(oItem1.Amount == 2 && oItem1.UnitPrice == 3.5 && oItem1.Total == 7 &&
                oItem2.Amount == 2 && oItem2.UnitPrice == 4.5 && oItem2.Total == 9 &&
                order.Total == 16 && order.TotalVAT == 20);
        }

        private NodeLog CreateExpectedLogger_Case_8_5_GivenUnitPriceIsChanged(IDependencyGraph graph, Order_8_5 order)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(order.Items[1], "Total"));
            logger.Log(graph.GetNode(order.Items, "Total"));

            logger.Log(graph.GetNode(order, "TotalVAT"));
            logger.Log(graph.GetNode(order, "Total"));


            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_5_GivenUnitPriceIsChanged_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var caseParameters = CreateCase_8_5();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;
            updater.PerformUpdate();
            OrderItem_8_5 item = order.Items[1];

            //Act
            item.UnitPrice = 5;

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case_8_5_GivenUnitPriceIsChanged(graph, order);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_5_GivenUnitPriceIsChanged_GivesCorrectResults()
        {
            //Arrange
            var caseParameters = CreateCase_8_5();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;
            updater.PerformUpdate();

            OrderItem_8_5 oItem1 = order.Items[0];
            OrderItem_8_5 oItem2 = order.Items[1];

            //Act
            oItem2.UnitPrice = 5;

            //Assert
            Assert.IsTrue(oItem1.Amount == 1 && oItem1.UnitPrice == 3.5 && oItem1.Total == 3.5 &&
                oItem2.Amount == 2 && oItem2.UnitPrice == 5 && oItem2.Total == 10 &&
                order.Total == 13.5 && order.TotalVAT == 16.875);
        }

        #endregion

        #region CASE_8_6

        /*
         * Example diagram file: CollectionNodeSimpleScenarios
        SCENARIJ S4
        ------------------------
        * Postoji kolekcijski čvor (npr. {Stavke, "Iznos"}).
        * Postoje pojedinačni čvorovi za parove {Stavka, "Iznos"}
        iz kolekcije
        * Pojedinačni čvorovi {Stavka, "Iznos"} su izvorišni (source) čvorovi
        * Kolekcijski čvor je među-čvor (intermediary node)
        ------------------------
        Kada korisnik unese npr. {Stavka, "Iznos"} kolekcijski čvor
        {Stavke, "Iznos"} sudjeluje u topološkom sortiranju te omogućava
        ispravnu propagaciju ažuriranja prema ovisnim čvorovima.
         */

        private Tuple<IDependencyGraph, Updater, Order_8_6> CreateCase_8_6()
        {
            var graph = new DependencyGraph("GRAPH_8_6");
            var updater = CreateUpdater(graph);
            updater.SuspendUpdate();

            var order = new Order_8_6();
            order.Items.Add(new OrderItem_8_6(updater) { Total = 10 });
            order.Items.Add(new OrderItem_8_6(updater) { Total = 20 });

            INode itemsTotal = graph.AddNode(order.Items, "Total");
            INode oItem1_Total = graph.AddNode(order.Items[0], "Total");
            INode oItem2_Total = graph.AddNode(order.Items[1], "Total");

            INode orderTotal = graph.AddNode(order, "Total");
            INode orderTotalVAT = graph.AddNode(order, "TotalVAT");


            graph.AddDependency(itemsTotal, orderTotal);
            graph.AddDependency(itemsTotal, orderTotalVAT);

            updater.ResumeUpdate();

            return new Tuple<IDependencyGraph, Updater, Order_8_6>(graph, updater, order);
        }

        private NodeLog CreateExpectedLogger_Case_8_6_PerformCompleteUpdate(IDependencyGraph graph, Order_8_6 order)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(order.Items[1], "Total"));
            logger.Log(graph.GetNode(order.Items[0], "Total"));
            logger.Log(graph.GetNode(order.Items, "Total"));
            logger.Log(graph.GetNode(order, "TotalVAT"));
            logger.Log(graph.GetNode(order, "Total"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_6_PerformCompleteUpdate_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var caseParameters = CreateCase_8_6();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;

            //Act
            updater.PerformUpdate();

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case_8_6_PerformCompleteUpdate(graph, order);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_6_PerformCompleteUpdate_GivesCorrectResults()
        {
            //Arrange
            var caseParameters = CreateCase_8_6();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;

            //Act
            updater.PerformUpdate();

            //Assert
            Assert.IsTrue(order.Items[0].Total == 10 && order.Items[1].Total == 20
                && order.Total == 30 && order.TotalVAT == 37.5);
        }

        private NodeLog CreateExpectedLogger_Case_8_6_GivenItem1TotalIsChanged(IDependencyGraph graph, Order_8_6 order)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(order.Items, "Total"));
            logger.Log(graph.GetNode(order, "TotalVAT"));
            logger.Log(graph.GetNode(order, "Total"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_6_GivenItem1TotalIsChanged_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var caseParameters = CreateCase_8_6();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;
            updater.PerformUpdate();

            //Act
            order.Items[0].Total = 15;

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case_8_6_GivenItem1TotalIsChanged(graph, order);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_6_GivenItem1TotalIsChanged_GivesCorrectResults()
        {
            //Arrange
            var caseParameters = CreateCase_8_6();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;
            updater.PerformUpdate();

            //Act
            order.Items[0].Total = 15;

            //Assert
            Assert.IsTrue(order.Items[0].Total == 15 && order.Items[1].Total == 20
                && order.Total == 35 && order.TotalVAT == 43.75);
        }

        private NodeLog CreateExpectedLogger_Case_8_6_GivenItem2TotalIsChanged(IDependencyGraph graph, Order_8_6 order)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(order.Items, "Total"));
            logger.Log(graph.GetNode(order, "TotalVAT"));
            logger.Log(graph.GetNode(order, "Total"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_6_GivenItem2TotalIsChanged_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var caseParameters = CreateCase_8_6();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;
            updater.PerformUpdate();

            //Act
            order.Items[1].Total = 25;

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case_8_6_GivenItem2TotalIsChanged(graph, order);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_6_GivenItem2TotalIsChanged_GivesCorrectResults()
        {
            //Arrange
            var caseParameters = CreateCase_8_6();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;
            updater.PerformUpdate();

            //Act
            order.Items[1].Total = 30;

            //Assert
            Assert.IsTrue(order.Items[0].Total == 10 && order.Items[1].Total == 30
                && order.Total == 40 && order.TotalVAT == 50);
        }

        #endregion

        #region CASE_8_7

        private Tuple<IDependencyGraph, Updater, Order_8_7> CreateCase_8_7()
        {
            var graph = new DependencyGraph("GRAPH_8_7");
            var updater = CreateUpdater(graph);
            updater.SuspendUpdate();

            var order = new Order_8_7(updater) { DiscountA = 5, DiscountB = 10 };
            order.Items.Add(new OrderItem_8_7(order, updater) { Amount = 1, UnitPrice = (decimal)3.5 });
            order.Items.Add(new OrderItem_8_7(order, updater) { Amount = 2, UnitPrice = (decimal)4.5 });

            INode item1_Amount = graph.AddNode(order.Items[0], "Amount");
            INode item2_Amount = graph.AddNode(order.Items[1], "Amount");

            INode item1_UnitPrice = graph.AddNode(order.Items[0], "UnitPrice");
            INode item2_UnitPrice = graph.AddNode(order.Items[1], "UnitPrice");

            INode item1_Total = graph.AddNode(order.Items[0], "Total");
            INode item2_Total = graph.AddNode(order.Items[1], "Total");

            INode item1_TotalVAT = graph.AddNode(order.Items[0], "TotalVAT");
            INode item2_TotalVAT = graph.AddNode(order.Items[1], "TotalVAT");

            INode itemsTotal = graph.AddNode(order.Items, "Total");

            INode orderTotal = graph.AddNode(order, "Total");
            INode orderTotalVAT = graph.AddNode(order, "TotalVAT");

            INode orderDiscountA = graph.AddNode(order, "DiscountA");
            INode orderDiscountB = graph.AddNode(order, "DiscountB");
            INode orderTotalDiscount = graph.AddNode(order, "TotalDiscount");

            graph.AddDependency(item1_Amount, item1_Total);
            graph.AddDependency(item1_UnitPrice, item1_Total);
            graph.AddDependency(item1_Total, item1_TotalVAT);
            graph.AddDependency(item2_Amount, item2_Total);
            graph.AddDependency(item2_UnitPrice, item2_Total);
            graph.AddDependency(item2_Total, item2_TotalVAT);

            graph.AddDependency(itemsTotal, orderTotal);
            graph.AddDependency(itemsTotal, orderTotalVAT);

            graph.AddDependency(orderDiscountA, orderTotalDiscount);
            graph.AddDependency(orderDiscountB, orderTotalDiscount);
            graph.AddDependency(orderTotalDiscount, itemsTotal);

            updater.ResumeUpdate();

            return new Tuple<IDependencyGraph, Updater, Order_8_7>(graph, updater, order);
        }

        private NodeLog CreateExpectedLogger_Case_8_7_PerformCompleteUpdate(IDependencyGraph graph, Order_8_7 order)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(order, "DiscountB"));
            logger.Log(graph.GetNode(order, "DiscountA"));
            logger.Log(graph.GetNode(order, "TotalDiscount"));
            logger.Log(graph.GetNode(order.Items[1], "UnitPrice"));
            logger.Log(graph.GetNode(order.Items[0], "UnitPrice"));
            logger.Log(graph.GetNode(order.Items[1], "Amount"));
            logger.Log(graph.GetNode(order.Items[1], "Total"));
            logger.Log(graph.GetNode(order.Items[1], "TotalVAT"));
            logger.Log(graph.GetNode(order.Items[0], "Amount"));
            logger.Log(graph.GetNode(order.Items[0], "Total"));
            logger.Log(graph.GetNode(order.Items, "Total"));
            logger.Log(graph.GetNode(order, "TotalVAT"));
            logger.Log(graph.GetNode(order, "Total"));
            logger.Log(graph.GetNode(order.Items[0], "TotalVAT"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_7_PerformCompleteUpdate_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var caseParameters = CreateCase_8_7();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;

            //Act
            updater.PerformUpdate();

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case_8_7_PerformCompleteUpdate(graph, order);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_7_PerformCompleteUpdate_GivesCorrectResults()
        {
            //Arrange
            var caseParameters = CreateCase_8_7();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;

            //Act
            updater.PerformUpdate();

            //Assert
            Assert.IsTrue(order.Items[0].Amount == 1 && order.Items[0].UnitPrice == (decimal)3.5 && order.Items[0].Total == (decimal)2.975 && order.Items[0].TotalVAT == 3.71875m);
            Assert.IsTrue(order.Items[1].Amount == 2 && order.Items[1].UnitPrice == (decimal)4.5 && order.Items[1].Total == (decimal)7.65 && order.Items[1].TotalVAT == 9.5625m);
            Assert.IsTrue(order.DiscountA == 5 && order.DiscountB == 10 && order.TotalDiscount == 15 && order.Total == (decimal)10.625 && order.TotalVAT == (decimal)13.28125);
        }

        private NodeLog CreateExpectedLogger_Case_8_7_GivenDiscountAIsChanged(IDependencyGraph graph, Order_8_7 order)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(order, "TotalDiscount"));
            logger.Log(graph.GetNode(order.Items[1], "Total"));
            logger.Log(graph.GetNode(order.Items[1], "TotalVAT"));
            logger.Log(graph.GetNode(order.Items[0], "Total"));
            logger.Log(graph.GetNode(order.Items, "Total"));
            logger.Log(graph.GetNode(order, "TotalVAT"));
            logger.Log(graph.GetNode(order, "Total"));
            logger.Log(graph.GetNode(order.Items[0], "TotalVAT"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_7_GivenDiscountAIsChanged_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var caseParameters = CreateCase_8_7();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;
            updater.PerformUpdate();

            //Act
            order.DiscountA = 10;

            //Assert
            NodeLog actualLogger = updater.NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case_8_7_GivenDiscountAIsChanged(graph, order);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_7_GivenDiscountAIsChanged_GivesCorrectResults()
        {
            //Arrange
            var caseParameters = CreateCase_8_7();
            var graph = caseParameters.Item1;
            var updater = caseParameters.Item2;
            var order = caseParameters.Item3;
            updater.PerformUpdate();

            //Act
            order.DiscountA = 10;

            //Assert
            Assert.IsTrue(order.Items[0].Amount == 1 && order.Items[0].UnitPrice == (decimal)3.5 && order.Items[0].Total == (decimal)2.8 && order.Items[0].TotalVAT == 3.5m);
            Assert.IsTrue(order.Items[1].Amount == 2 && order.Items[1].UnitPrice == (decimal)4.5 && order.Items[1].Total == (decimal)7.2 && order.Items[1].TotalVAT == 9);
            Assert.IsTrue(order.DiscountA == 10 && order.DiscountB == 10 && order.TotalDiscount == 20 && order.Total == (decimal)10 && order.TotalVAT == (decimal)12.5);
        }

        #endregion

        #region CASE_8_8

        /*Testing update process when items are added or removed from reactive collection*/

        private Tuple<IReactor, Class_C_8_8> CreateCase_8_8()
        {
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("R1");
            reactor.Updater.SuspendUpdate();

            var objA1 = new Class_A_8_8 { Reactor = reactor, A = 1 };
            var objA2 = new Class_A_8_8 { Reactor = reactor, A = 2 };

            var objB1 = new Class_B_8_8 { A = 1, PartA = objA1 };
            var objB2 = new Class_B_8_8 { A = 2, PartA = objA2 };

            var objC = new Class_C_8_8 { A = 3 };
            objC.PartsB.Add(objB1);
            objC.PartsB.Add(objB2);

            INode a1_a = reactor.AddNode(objA1, "A");
            INode a1_b = reactor.AddNode(objA1, "B");
            INode a2_a = reactor.AddNode(objA2, "A");
            INode a2_b = reactor.AddNode(objA2, "B");

            reactor.AddDependency(a1_a, a1_b);
            reactor.AddDependency(a2_a, a2_b);

            INode b1_a = reactor.AddNode(objB1, "A");
            INode b1_b = reactor.AddNode(objB1, "B");
            INode b2_a = reactor.AddNode(objB2, "A");
            INode b2_b = reactor.AddNode(objB2, "B");

            reactor.AddDependency(b1_a, b1_b);
            reactor.AddDependency(b2_a, b2_b);
            reactor.AddDependency(a1_a, b1_a);
            reactor.AddDependency(a1_b, b1_b);
            reactor.AddDependency(a2_a, b2_a);
            reactor.AddDependency(a2_b, b2_b);

            INode c_a = reactor.AddNode(objC, "A");
            INode c_b = reactor.AddNode(objC, "B");
            INode c_partsB_A = reactor.AddNode(objC.PartsB, "A");
            INode c_partsB_B = reactor.AddNode(objC.PartsB, "B");

            reactor.AddDependency(c_a, c_b);
            reactor.AddDependency(c_partsB_A, c_a);
            reactor.AddDependency(c_partsB_B, c_b);

            reactor.Updater.ResumeUpdate();

            return new Tuple<IReactor, Class_C_8_8>(reactor, objC);
        }

        private NodeLog CreateExpectedLogger_Case_8_8_PerformCompleteUpdate(IDependencyGraph graph, Class_C_8_8 objC)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(objC.PartsB[1].PartA, "A"));
            logger.Log(graph.GetNode(objC.PartsB[1], "A"));
            logger.Log(graph.GetNode(objC.PartsB[1].PartA, "B"));
            logger.Log(graph.GetNode(objC.PartsB[1], "B"));
            logger.Log(graph.GetNode(objC.PartsB[0].PartA, "A"));
            logger.Log(graph.GetNode(objC.PartsB[0], "A"));
            logger.Log(graph.GetNode(objC.PartsB, "A"));
            logger.Log(graph.GetNode(objC, "A"));
            logger.Log(graph.GetNode(objC.PartsB[0].PartA, "B"));
            logger.Log(graph.GetNode(objC.PartsB[0], "B"));
            logger.Log(graph.GetNode(objC.PartsB, "B"));
            logger.Log(graph.GetNode(objC, "B"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_8_PerformCompleteUpdate_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var caseParameters = CreateCase_8_8();
            var reactor = caseParameters.Item1;
            var objC = caseParameters.Item2;

            //Act
            reactor.PerformUpdate();

            //Assert
            NodeLog actualLogger = (reactor.Updater as ILoggable).NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case_8_8_PerformCompleteUpdate(reactor.Graph, objC);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_8_GivenNewItemIsAddedToReactiveCollection_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var caseParameters = CreateCase_8_8();
            var reactor = caseParameters.Item1;
            var objC = caseParameters.Item2;

            //Act
            Class_B_8_8 newB = new Class_B_8_8()
            {
                PartA = new Class_A_8_8()
            };

            objC.PartsB.Add(newB);

            //Assert
            NodeLog actualLogger = (reactor.Updater as ILoggable).NodeLog;
            NodeLog expectedLogger = CreateExpectedLogger_Case_8_8_PerformCompleteUpdate(reactor.Graph, objC);

            Assert.AreEqual(expectedLogger, actualLogger);
        }

        private NodeLog CreateExpectedLogger_Case_8_8_RemovedItem(IDependencyGraph graph, Class_C_8_8 objC)
        {
            NodeLog logger = new NodeLog();

            logger.Log(graph.GetNode(objC.PartsB[1].PartA, "A"));
            logger.Log(graph.GetNode(objC.PartsB[1], "A"));
            logger.Log(graph.GetNode(objC.PartsB[1].PartA, "B"));
            logger.Log(graph.GetNode(objC.PartsB[1], "B"));
            logger.Log(graph.GetNode(objC.PartsB[0].PartA, "A"));
            logger.Log(graph.GetNode(objC.PartsB[0], "A"));
            logger.Log(graph.GetNode(objC.PartsB, "A"));
            logger.Log(graph.GetNode(objC, "A"));
            logger.Log(graph.GetNode(objC.PartsB[0].PartA, "B"));
            logger.Log(graph.GetNode(objC.PartsB[0], "B"));
            logger.Log(graph.GetNode(objC.PartsB, "B"));
            logger.Log(graph.GetNode(objC, "B"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_8_GivenExistingItemIsRemovedFromReactiveCollection_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var caseParameters = CreateCase_8_8();
            var reactor = caseParameters.Item1;
            var objC = caseParameters.Item2;
            NodeLog expectedLogger = CreateExpectedLogger_Case_8_8_RemovedItem(reactor.Graph, objC);

            //Act
            objC.PartsB.Remove(objC.PartsB[1]);

            //Assert
            NodeLog actualLogger = (reactor.Updater as ILoggable).NodeLog;
            Assert.AreEqual(expectedLogger, actualLogger);
        }

        #endregion
    }
}
