using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCore;
using ReframeCore.Helpers;
using ReframeCore.Nodes;
using ReframeCoreExamples.E08.E2;
using ReframeCoreExamples.E08.E3;
using ReframeCoreExamples.E08.E4;
using ReframeCoreExamples.E08.E5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCoreTests
{
    public partial class DependencyGraphTests
    {
        #region PerformUpdate_CASE_8_2

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

        private Order_8_2 CreateCase_8_2()
        {
            GraphFactory.Clear();
            var graph = GraphFactory.Create("GRAPH_8_2");

            var order = new Order_8_2();
            order.Items.Add(new OrderItem_8_2());
            order.Items.Add(new OrderItem_8_2());
            order.Items.Add(new OrderItem_8_2());

            INode orderTotal = graph.AddNode(order, "Total");
            INode orderTotalVAT = graph.AddNode(order, "TotalVAT");
            INode itemsTotal = graph.AddNode(order.Items, "Total");

            graph.AddDependency(itemsTotal, orderTotal);
            graph.AddDependency(itemsTotal, orderTotalVAT);

            graph.Initialize();

            return order;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_2_PerformCompleteUpdate_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var order = CreateCase_8_2();
            var graph = GraphFactory.Get("GRAPH_8_2");

            //Act
            graph.PerformUpdate();

            //Assert
            Logger actualLogger = (graph as DependencyGraph).Logger;
            Logger expectedLogger = CreateExpectedLogger_Case_8_2_PerformCompleteUpdate(graph, order);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        private Logger CreateExpectedLogger_Case_8_2_PerformCompleteUpdate(IDependencyGraph graph, Order_8_2 order)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(order.Items, "Total"));
            logger.LogNodeToUpdate(graph.GetNode(order, "TotalVAT"));
            logger.LogNodeToUpdate(graph.GetNode(order, "Total"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_2_PerformCompleteUpdate_GivesCorrectResults()
        {
            //Arrange
            var order = CreateCase_8_2();
            var graph = GraphFactory.Get("GRAPH_8_2");

            //Act
            graph.PerformUpdate();

            //Assert
            Assert.IsTrue(order.Total == 3 && order.TotalVAT == 3.75);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_2_GivenItemTotalIsChanged_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var order = CreateCase_8_2();
            var graph = GraphFactory.Get("GRAPH_8_2");
            graph.PerformUpdate();

            //Act
            order.Items[1].Total = 2;

            //Assert
            Logger actualLogger = (graph as DependencyGraph).Logger;
            Logger expectedLogger = CreateExpectedLogger_Case_8_2_GivenItemTotalIsChanged(graph, order);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        private Logger CreateExpectedLogger_Case_8_2_GivenItemTotalIsChanged(IDependencyGraph graph, Order_8_2 order)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(order, "TotalVAT"));
            logger.LogNodeToUpdate(graph.GetNode(order, "Total"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_2_GivenItemTotalIsChanged_GivesCorrectResults()
        {
            //Arrange
            var order = CreateCase_8_2();
            var graph = GraphFactory.Get("GRAPH_8_2");
            graph.PerformUpdate();

            //Act
            order.Items[1].Total = 2;

            //Assert
            Assert.IsTrue(order.Total == 4 && order.TotalVAT == 5);
        }

        #endregion

        #region PerformUpdate_CASE_8_3

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

        private Order_8_3 CreateCase_8_3()
        {
            GraphFactory.Clear();
            var graph = GraphFactory.Create("GRAPH_8_3");

            var order = new Order_8_3();
            order.Items.Add(new OrderItem_8_3(order) { FixedValue = 10});
            order.Items.Add(new OrderItem_8_3(order) { FixedValue = 20});
            order.Items.Add(new OrderItem_8_3(order) { FixedValue = 30});

            INode discountA = graph.AddNode(order, "DiscountA");
            INode discountB = graph.AddNode(order, "DiscountB");
            INode itemsTotal = graph.AddNode(order.Items, "Total");

            graph.AddDependency(discountA, itemsTotal);
            graph.AddDependency(discountB, itemsTotal);

            graph.Initialize();

            return order;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_3_PerformCompleteUpdate_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var order = CreateCase_8_3();
            var graph = GraphFactory.Get("GRAPH_8_3");

            //Act
            graph.PerformUpdate();

            //Assert
            Logger actualLogger = (graph as DependencyGraph).Logger;
            Logger expectedLogger = CreateExpectedLogger_Case_8_3_PerformCompleteUpdate(graph, order);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        private Logger CreateExpectedLogger_Case_8_3_PerformCompleteUpdate(IDependencyGraph graph, Order_8_3 order)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(order, "DiscountB"));
            logger.LogNodeToUpdate(graph.GetNode(order, "DiscountA"));
            logger.LogNodeToUpdate(graph.GetNode(order.Items, "Total"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_3_PerformCompleteUpdate_GivesCorrectResults()
        {
            //Arrange
            var order = CreateCase_8_3();
            var graph = GraphFactory.Get("GRAPH_8_3");

            //Act
            graph.PerformUpdate();

            //Assert
            Assert.IsTrue(order.Items[0].Total == 10 && order.Items[1].Total == 20 && order.Items[2].Total == 30);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_3_GivenDiscountAIsChanged_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var order = CreateCase_8_3();
            var graph = GraphFactory.Get("GRAPH_8_3");
            graph.PerformUpdate();

            //Act
            order.DiscountA = 10;

            //Assert
            Logger actualLogger = (graph as DependencyGraph).Logger;
            Logger expectedLogger = CreateExpectedLogger_Case_8_3_GivenDiscountAIsChanged(graph, order);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        private Logger CreateExpectedLogger_Case_8_3_GivenDiscountAIsChanged(IDependencyGraph graph, Order_8_3 order)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(order.Items, "Total"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_3_GivenDiscountAIsChanged_GivesCorrectResults()
        {
            //Arrange
            var order = CreateCase_8_3();
            var graph = GraphFactory.Get("GRAPH_8_3");
            graph.PerformUpdate();

            //Act
            order.DiscountA = 10;

            //Assert
            Assert.IsTrue(order.Items[0].Total == 9 && order.Items[1].Total == 18 && order.Items[2].Total == 27);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_3_GivenDiscountBIsChanged_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var order = CreateCase_8_3();
            var graph = GraphFactory.Get("GRAPH_8_3");
            graph.PerformUpdate();

            //Act
            order.DiscountB = 20;

            //Assert
            Logger actualLogger = (graph as DependencyGraph).Logger;
            Logger expectedLogger = CreateExpectedLogger_Case_8_3_GivenDiscountBIsChanged(graph, order);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        private Logger CreateExpectedLogger_Case_8_3_GivenDiscountBIsChanged(IDependencyGraph graph, Order_8_3 order)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(order.Items, "Total"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_3_GivenDiscountBIsChanged_GivesCorrectResults()
        {
            //Arrange
            var order = CreateCase_8_3();
            var graph = GraphFactory.Get("GRAPH_8_3");
            graph.PerformUpdate();

            //Act
            order.DiscountB = 20;

            //Assert
            Assert.IsTrue(order.Items[0].Total == 8 && order.Items[1].Total == 16 && order.Items[2].Total == 24);
        }

        #endregion

        #region PerformUpdate_CASE_8_4

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

        private Order_8_4 CreateCase_8_4()
        {
            GraphFactory.Clear();
            var graph = GraphFactory.Create("GRAPH_8_4");

            var order = new Order_8_4();
            order.Items.Add(new OrderItem_8_4(order) { FixedValue = 10});
            order.Items.Add(new OrderItem_8_4(order) { FixedValue = 20});
            order.Items.Add(new OrderItem_8_4(order) { FixedValue = 30});

            INode orderTotal = graph.AddNode(order, "Total");
            INode orderTotalVAT = graph.AddNode(order, "TotalVAT");
            INode orderDiscountA = graph.AddNode(order, "DiscountA");
            INode orderDiscountB = graph.AddNode(order, "DiscountB");
            INode itemsTotal = graph.AddNode(order.Items, "Total");

            graph.AddDependency(orderDiscountA, itemsTotal);
            graph.AddDependency(orderDiscountB, itemsTotal);
            graph.AddDependency(itemsTotal, orderTotal);
            graph.AddDependency(itemsTotal, orderTotalVAT);

            graph.Initialize();

            return order;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_4_PerformCompleteUpdate_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var order = CreateCase_8_4();
            var graph = GraphFactory.Get("GRAPH_8_4");

            //Act
            graph.PerformUpdate();

            //Assert
            Logger actualLogger = (graph as DependencyGraph).Logger;
            Logger expectedLogger = CreateExpectedLogger_Case_8_4_PerformCompleteUpdate(graph, order);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        private Logger CreateExpectedLogger_Case_8_4_PerformCompleteUpdate(IDependencyGraph graph, Order_8_4 order)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(order, "DiscountB"));
            logger.LogNodeToUpdate(graph.GetNode(order, "DiscountA"));
            logger.LogNodeToUpdate(graph.GetNode(order.Items, "Total"));
            logger.LogNodeToUpdate(graph.GetNode(order, "TotalVAT"));
            logger.LogNodeToUpdate(graph.GetNode(order, "Total"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_4_PerformCompleteUpdate_GivesCorrectResults()
        {
            //Arrange
            var order = CreateCase_8_4();
            var graph = GraphFactory.Get("GRAPH_8_4");

            //Act
            graph.PerformUpdate();

            //Assert
            Assert.IsTrue(order.Items[0].Total == 10 && order.Items[1].Total == 20 && order.Items[2].Total == 30
                && order.Total == 60 && order.TotalVAT == 75);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_4_GivenDiscountAIsChanged_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var order = CreateCase_8_4();
            var graph = GraphFactory.Get("GRAPH_8_4");
            graph.PerformUpdate();

            //Act
            order.DiscountA = 10;

            //Assert
            Logger actualLogger = (graph as DependencyGraph).Logger;
            Logger expectedLogger = CreateExpectedLogger_Case_8_4_GivenDiscountAIsChanged(graph, order);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        private Logger CreateExpectedLogger_Case_8_4_GivenDiscountAIsChanged(IDependencyGraph graph, Order_8_4 order)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(order.Items, "Total"));
            logger.LogNodeToUpdate(graph.GetNode(order, "TotalVAT"));
            logger.LogNodeToUpdate(graph.GetNode(order, "Total"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_4_GivenDiscountAIsChanged_GivesCorrectResults()
        {
            //Arrange
            var order = CreateCase_8_4();
            var graph = GraphFactory.Get("GRAPH_8_4");
            graph.PerformUpdate();

            //Act
            order.DiscountA = 10;

            //Assert
            Assert.IsTrue(order.Items[0].Total == 9 && order.Items[1].Total == 18 && order.Items[2].Total == 27
                && order.Total == 54 && order.TotalVAT == 67.5);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_4_GivenDiscountBIsChanged_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var order = CreateCase_8_4();
            var graph = GraphFactory.Get("GRAPH_8_4");
            graph.PerformUpdate();

            //Act
            order.DiscountB = 20;

            //Assert
            Logger actualLogger = (graph as DependencyGraph).Logger;
            Logger expectedLogger = CreateExpectedLogger_Case_8_4_GivenDiscountBIsChanged(graph, order);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        private Logger CreateExpectedLogger_Case_8_4_GivenDiscountBIsChanged(IDependencyGraph graph, Order_8_4 order)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(order.Items, "Total"));
            logger.LogNodeToUpdate(graph.GetNode(order, "TotalVAT"));
            logger.LogNodeToUpdate(graph.GetNode(order, "Total"));

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_4_GivenDiscountBIsChanged_GivesCorrectResults()
        {
            //Arrange
            var order = CreateCase_8_4();
            var graph = GraphFactory.Get("GRAPH_8_4");
            graph.PerformUpdate();

            //Act
            order.DiscountB = 20;

            //Assert
            Assert.IsTrue(order.Items[0].Total == 8 && order.Items[1].Total == 16 && order.Items[2].Total == 24
                && order.Total == 48 && order.TotalVAT == 60);
        }

        #endregion

        #region PerformUpdate_CASE_8_5

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

        private Order_8_5 CreateCase_8_5()
        {
            GraphFactory.Clear();
            var graph = GraphFactory.Create("GRAPH_8_5");

            var order = new Order_8_5();
            order.Items.Add(new OrderItem_8_5() { Amount = 1, UnitPrice = 3.5 });
            order.Items.Add(new OrderItem_8_5() { Amount = 2, UnitPrice = 4.5 });

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

            graph.Initialize();

            return order;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_5_PerformCompleteUpdate_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var order = CreateCase_8_5();
            var graph = GraphFactory.Get("GRAPH_8_5");

            //Act
            graph.PerformUpdate();

            //Assert
            Logger actualLogger = (graph as DependencyGraph).Logger;
            Logger expectedLogger = CreateExpectedLogger_Case_8_5_PerformCompleteUpdate(graph, order);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        private Logger CreateExpectedLogger_Case_8_5_PerformCompleteUpdate(IDependencyGraph graph, Order_8_5 order)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(order.Items[1], "UnitPrice"));
            logger.LogNodeToUpdate(graph.GetNode(order.Items[0], "UnitPrice"));
            logger.LogNodeToUpdate(graph.GetNode(order.Items[1], "Amount"));
            logger.LogNodeToUpdate(graph.GetNode(order.Items[1], "Total"));
            logger.LogNodeToUpdate(graph.GetNode(order.Items[0], "Amount"));
            logger.LogNodeToUpdate(graph.GetNode(order.Items[0], "Total"));
            logger.LogNodeToUpdate(graph.GetNode(order.Items, "Total"));

            logger.LogNodeToUpdate(graph.GetNode(order, "TotalVAT"));
            logger.LogNodeToUpdate(graph.GetNode(order, "Total"));
            

            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_5_PerformCompleteUpdate_GivesCorrectResults()
        {
            //Arrange
            var order = CreateCase_8_5();
            var graph = GraphFactory.Get("GRAPH_8_5");

            //Act
            graph.PerformUpdate();

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
            var order = CreateCase_8_5();
            var graph = GraphFactory.Get("GRAPH_8_5");
            graph.PerformUpdate();

            //Act
            OrderItem_8_5 item = order.Items[0];
            item.Amount = 2;

            //Assert
            Logger actualLogger = (graph as DependencyGraph).Logger;
            Logger expectedLogger = CreateExpectedLogger_Case_8_5_GivenAmountIsChanged(graph, order);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        private Logger CreateExpectedLogger_Case_8_5_GivenAmountIsChanged(IDependencyGraph graph, Order_8_5 order)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(order.Items[0], "Total"));
            logger.LogNodeToUpdate(graph.GetNode(order.Items, "Total"));

            logger.LogNodeToUpdate(graph.GetNode(order, "TotalVAT"));
            logger.LogNodeToUpdate(graph.GetNode(order, "Total"));


            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_5_GivenAmountIsChanged_GivesCorrectResults()
        {
            //Arrange
            var order = CreateCase_8_5();
            var graph = GraphFactory.Get("GRAPH_8_5");
            graph.PerformUpdate();

            OrderItem_8_5 oItem1 = order.Items[0];
            OrderItem_8_5 oItem2 = order.Items[1];

            //Act
            oItem1.Amount = 2;

            //Assert
            Assert.IsTrue(oItem1.Amount == 2 && oItem1.UnitPrice == 3.5 && oItem1.Total == 7 &&
                oItem2.Amount == 2 && oItem2.UnitPrice == 4.5 && oItem2.Total == 9 &&
                order.Total == 16 && order.TotalVAT == 20);
        }

        [TestMethod]
        public void PerformUpdate_Case_8_5_GivenUnitPriceIsChanged_SchedulesCorrectUpdateOrder()
        {
            //Arrange
            var order = CreateCase_8_5();
            var graph = GraphFactory.Get("GRAPH_8_5");
            graph.PerformUpdate();
            OrderItem_8_5 item = order.Items[1];

            //Act
            item.UnitPrice = 5;

            //Assert
            Logger actualLogger = (graph as DependencyGraph).Logger;
            Logger expectedLogger = CreateExpectedLogger_Case_8_5_GivenUnitPriceIsChanged(graph, order);

            Assert.AreEqual(expectedLogger.GetNodesToUpdate(), actualLogger.GetNodesToUpdate());
        }

        private Logger CreateExpectedLogger_Case_8_5_GivenUnitPriceIsChanged(IDependencyGraph graph, Order_8_5 order)
        {
            Logger logger = new Logger();

            logger.LogNodeToUpdate(graph.GetNode(order.Items[1], "Total"));
            logger.LogNodeToUpdate(graph.GetNode(order.Items, "Total"));

            logger.LogNodeToUpdate(graph.GetNode(order, "TotalVAT"));
            logger.LogNodeToUpdate(graph.GetNode(order, "Total"));


            return logger;
        }

        [TestMethod]
        public void PerformUpdate_Case_8_5_GivenUnitPriceIsChanged_GivesCorrectResults()
        {
            //Arrange
            var order = CreateCase_8_5();
            var graph = GraphFactory.Get("GRAPH_8_5");
            graph.PerformUpdate();

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
    }
}
