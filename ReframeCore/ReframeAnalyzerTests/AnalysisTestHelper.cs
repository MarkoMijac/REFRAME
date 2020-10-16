using ReframeAnalyzer;
using ReframeAnalyzer.Graph;
using ReframeAnalyzer.GraphFactories;
using ReframeCore;
using ReframeCore.Factories;
using ReframeCoreExamples.E09;
using ReframeCoreExamples.E09._01;
using ReframeCoreFluentAPI;
using ReframeExporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReframeAnalyzerTests
{
    public static class AnalysisTestHelper
    {
        public static IAnalysisGraph CreateAnalysisGraph(IReactor reactor, AnalysisLevel level)
        {
            var xmlSource = new XmlReactorDetailExporter(reactor.Identifier).Export();
            IAnalysisGraph result;

            switch (level)
            {
                case AnalysisLevel.ObjectMemberLevel:
                    {
                        var factory = new ObjectMemberAnalysisGraphFactory();
                        result = factory.CreateGraph(xmlSource);
                        break;
                    }
                case AnalysisLevel.ObjectLevel:
                    {
                        var factory = new ObjectAnalysisGraphFactory();
                        result = factory.CreateGraph(xmlSource);
                        break;
                    }
                case AnalysisLevel.ClassMemberLevel:
                    {
                        var factory = new ClassMemberAnalysisGraphFactory();
                        result = factory.CreateGraph(xmlSource);
                        break;
                    }
                case AnalysisLevel.ClassLevel:
                    {
                        var factory = new ClassAnalysisGraphFactory();
                        result = factory.CreateGraph(xmlSource);
                        break;
                    }
                case AnalysisLevel.AssemblyLevel:
                    {
                        var factory = new AssemblyAnalysisGraphFactory();
                        result = factory.CreateGraph(xmlSource);
                        break;
                    }
                case AnalysisLevel.NamespaceLevel:
                    {
                        var factory = new NamespaceAnalysisGraphFactory();
                        result = factory.CreateGraph(xmlSource);
                        break;
                    }
                default:
                    result = null;
                    break;
            }
            return result;
        }

        public static IReactor CreateEmptyReactor()
        {
            ReactorRegistry.Instance.Clear();
            return ReactorRegistry.Instance.CreateReactor("R1");
        }

        public static IReactor CreateCase1()
        {
            List<object> repository = new List<object>();
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");

            ClassA objA = new ClassA("First object A");
            ClassB objB = new ClassB("Second object B");
            ClassC objC = new ClassC("Third object C");
            repository.Add(objA);
            repository.Add(objB);
            repository.Add(objC);

            reactor.Let(() => objA.PA1)
                .DependOn(() => objB.PB1, () => objC.PC1);
            return reactor;
        }

        public static IReactor CreateCase2()
        {
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");

            List<object> repository = new List<object>();
            ClassA objA = new ClassA("Object A");
            ClassB objB = new ClassB("Object B");
            ClassC objC = new ClassC("Object C");
            ClassG objG = new ClassG("Object G");
            repository.Add(objA);
            repository.Add(objB);
            repository.Add(objC);
            repository.Add(objG);

            reactor.Let(() => objG.PG1)
                .DependOn(() => objB.PB1, () => objC.PC1, () => objG.PG2);
            reactor.Let(() => objB.PB1, () => objC.PC1).DependOn(() => objA.PA1);
            return reactor;
        }

        public static IReactor CreateCase3()
        {
            /*
             * Same class, different objects, same member.
             */
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");
            List<object> repository = new List<object>();

            ClassA firstObject = new ClassA("First object A");
            ClassA secondObject = new ClassA("Second object A");
            repository.Add(firstObject);
            repository.Add(secondObject);

            reactor.Let(() => firstObject.PA1)
                .DependOn(() => secondObject.PA1);
            return reactor;
        }

        /// <summary>
        /// Same class, same objects, different members.
        /// </summary>
        /// <returns></returns>
        public static IReactor CreateCase4()
        {
            /*
             * Same class, same objects, different members.
             */
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");

            ClassA objA = new ClassA("First object A");
            reactor.Let(() => objA.PA1)
                .DependOn(() => objA.PA2);

            List<object> repository = new List<object>();
            repository.Add(objA);

            return reactor;
        }

        /// <summary>
        /// Two different classes, two objects, 4 members.
        /// </summary>
        /// <returns></returns>
        public static IReactor CreateCase5()
        {
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");
            ClassA objA = new ClassA("First object A");
            ClassB objB = new ClassB("Second object B");

            reactor.Let(() => objA.PA1).DependOn(() => objB.PB1);
            reactor.Let(() => objB.PB2).DependOn(() => objA.PA2);

            List<object> repository = new List<object>();
            repository.Add(objA);
            repository.Add(objB);

            return reactor;
        }

        /// <summary>
        /// More complex graph with orphan nodes.
        /// </summary>
        /// <returns></returns>
        public static IReactor CreateCase6()
        {
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");
            ClassA objA = new ClassA();
            ClassB objB = new ClassB();
            ClassC objC = new ClassC();
            ClassD objD = new ClassD();
            ClassE objE = new ClassE();
            ClassF objF = new ClassF();
            ClassG objG = new ClassG();

            reactor.Let(() => objG.PG1).DependOn(() => objF.PF1, () => objE.PE1);
            reactor.Let(() => objF.PF1).DependOn(() => objB.PB1);
            reactor.Let(() => objE.PE1).DependOn(() => objB.PB1, () => objC.PC1);
            reactor.Let(() => objB.PB1).DependOn(() => objA.PA1);
            reactor.Let(() => objC.PC1).DependOn(() => objA.PA1, () => objD.PD1);

            //Add few orphan nodes
            reactor.AddNode(objA, nameof(objA.PA2));
            reactor.AddNode(objB, nameof(objB.PB2));

            List<object> repository = new List<object>();
            repository.Add(objA);
            repository.Add(objB);
            repository.Add(objC);
            repository.Add(objD);
            repository.Add(objE);
            repository.Add(objF);
            repository.Add(objG);

            return reactor;
        }

        /// <summary>
        /// Graph with only orphan nodes.
        /// </summary>
        /// <returns></returns>
        public static IReactor CreateCase7()
        {
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");
            ClassA objA = new ClassA();
            ClassB objB = new ClassB();

            //Add few orphan nodes
            reactor.AddNode(objA, nameof(objA.PA2));
            reactor.AddNode(objB, nameof(objB.PB2));

            List<object> repository = new List<object>();
            repository.Add(objA);
            repository.Add(objB);

            return reactor;
        }

        /// <summary>
        /// Graph with no intermediary nodes.
        /// </summary>
        /// <returns></returns>
        public static IReactor CreateCase8()
        {
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");
            ClassA objA = new ClassA();
            ClassB objB = new ClassB();

            reactor.Let(() => objB.PB1).DependOn(() => objA.PA1);
            reactor.Let(() => objB.PB2).DependOn(() => objA.PA2);

            List<object> repository = new List<object>();
            repository.Add(objA);
            repository.Add(objB);

            return reactor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IReactor CreateCase9()
        {
            ReactorRegistry.Instance.Clear();
            var reactor = ReactorRegistry.Instance.CreateReactor("S1");
            ClassA objA = new ClassA();
            ClassB objB = new ClassB();
            ClassC objC = new ClassC();

            reactor.Let(() => objC.PC1).DependOn(() => objB.PB1);
            reactor.Let(() => objB.PB1).DependOn(() => objA.PA1);

            List<object> repository = new List<object>();
            repository.Add(objA);
            repository.Add(objB);
            repository.Add(objC);

            return reactor;
        }

        #region GetNodeXElement

        public static XElement GetAssemblyNode_XElement()
        {
            string assemblyNode = "<OwnerAssembly><Identifier>63906962</Identifier><Name>ReframeCoreExamples.dll</Name></OwnerAssembly>";

            return XElement.Parse(assemblyNode);

        }

        public static XElement GetNamespaceNode_XElement()
        {
            string namespaceNode = "<OwnerNamespace><Identifier>3679577347</Identifier><Name>ReframeCoreExamples.E09</Name></OwnerNamespace>";

            return XElement.Parse(namespaceNode);
        }

        public static XElement GetClassNode_XElement()
        {
            string classNode = "<OwnerClass><Identifier>776132068</Identifier><Name>ClassB</Name><FullName>ReframeCoreExamples.E09.ClassB</FullName><OwnerNamespace><Identifier>3679577347</Identifier><Name>ReframeCoreExamples.E09</Name></OwnerNamespace><OwnerAssembly><Identifier>63906962</Identifier><Name>ReframeCoreExamples.dll</Name></OwnerAssembly></OwnerClass>";

            return XElement.Parse(classNode);
        }

        public static XElement GetObjectNode_XElement()
        {
            string objectNode = "<OwnerObject><Identifier>12852035</Identifier><Name>First object A</Name><OwnerClass><Identifier>776132068</Identifier><Name>ClassB</Name><FullName>ReframeCoreExamples.E09.ClassB</FullName><OwnerNamespace><Identifier>3679577347</Identifier><Name>ReframeCoreExamples.E09</Name></OwnerNamespace><OwnerAssembly><Identifier>63906962</Identifier><Name>ReframeCoreExamples.dll</Name></OwnerAssembly></OwnerClass></OwnerObject>";

            return XElement.Parse(objectNode);
        }

        public static XElement GetObjectMemberNode_XElement()
        {
            string objectMemberNode = "<Node><Identifier>1526585190</Identifier><MemberName>PA1</MemberName><NodeType>PropertyNode</NodeType><CurrentValue>0</CurrentValue><PreviousValue></PreviousValue><OwnerObject><Identifier>12852035</Identifier><Name>First object A</Name><OwnerClass><Identifier>776132068</Identifier><Name>ClassB</Name><FullName>ReframeCoreExamples.E09.ClassB</FullName><OwnerNamespace><Identifier>3679577347</Identifier><Name>ReframeCoreExamples.E09</Name></OwnerNamespace><OwnerAssembly><Identifier>63906962</Identifier><Name>ReframeCoreExamples.dll</Name></OwnerAssembly></OwnerClass></OwnerObject><Predecessors><Predecessor><Identifier>1520527567</Identifier><MemberName>PB1</MemberName></Predecessor></Predecessors><Successors/></Node>";

            return XElement.Parse(objectMemberNode);
        }

        public static XElement GetUpdateNode_XElement()
        {
            string updateNode = "<Node><Identifier>3451459271</Identifier><MemberName>B3</MemberName><OwnerObject>ClassB</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>958745</ObjectIdentifier><CurrentValue>2</CurrentValue><PreviousValue>2</PreviousValue><UpdateOrder>1</UpdateOrder><UpdateLayer>1</UpdateLayer><UpdateStartedAt>14:2:29:848</UpdateStartedAt><UpdateCompletedAt>14:2:29:848</UpdateCompletedAt><UpdateDuration>00:00:00.0000058</UpdateDuration><Predecessors /><Successors><Successor><Identifier>3451393735</Identifier><MemberName>B4</MemberName><OwnerObject>ClassB</OwnerObject></Successor></Successors></Node>";

            return XElement.Parse(updateNode);
        }

        #endregion

        #region GetUpdateInfo

        public static string GetUpdateInfoString()
        {
            return "<UpdateProcess><UpdateSuccessful>True</UpdateSuccessful><UpdateStartedAt>14:2:29:848</UpdateStartedAt><UpdateEndedAt>14:2:29:849</UpdateEndedAt><UpdateDuration>00:00:00.0010000</UpdateDuration><UpdateCause><Message>Complete graph update requested!</Message></UpdateCause><Reactor><Identifier>Example_1_6</Identifier><Graph><Identifier>Example_1_6</Identifier><TotalNodeCount>16</TotalNodeCount></Graph></Reactor><Nodes><Node><Identifier>3451459271</Identifier><MemberName>B3</MemberName><OwnerObject>ClassB</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>958745</ObjectIdentifier><CurrentValue>2</CurrentValue><PreviousValue>2</PreviousValue><UpdateOrder>1</UpdateOrder><UpdateLayer>1</UpdateLayer><UpdateStartedAt>14:2:29:848</UpdateStartedAt><UpdateCompletedAt>14:2:29:848</UpdateCompletedAt><UpdateDuration>00:00:00.0000058</UpdateDuration><Predecessors /><Successors><Successor><Identifier>3451393735</Identifier><MemberName>B4</MemberName><OwnerObject>ClassB</OwnerObject></Successor></Successors></Node><Node><Identifier>3451262663</Identifier><MemberName>B2</MemberName><OwnerObject>ClassB</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>958745</ObjectIdentifier><CurrentValue>2</CurrentValue><PreviousValue>2</PreviousValue><UpdateOrder>2</UpdateOrder><UpdateLayer>1</UpdateLayer><UpdateStartedAt>14:2:29:848</UpdateStartedAt><UpdateCompletedAt>14:2:29:848</UpdateCompletedAt><UpdateDuration>00:00:00.0000046</UpdateDuration><Predecessors /><Successors><Successor><Identifier>3451393735</Identifier><MemberName>B4</MemberName><OwnerObject>ClassB</OwnerObject></Successor></Successors></Node><Node><Identifier>3451328199</Identifier><MemberName>B1</MemberName><OwnerObject>ClassB</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>958745</ObjectIdentifier><CurrentValue>2</CurrentValue><PreviousValue>2</PreviousValue><UpdateOrder>3</UpdateOrder><UpdateLayer>1</UpdateLayer><UpdateStartedAt>14:2:29:848</UpdateStartedAt><UpdateCompletedAt>14:2:29:848</UpdateCompletedAt><UpdateDuration>00:00:00.0000300</UpdateDuration><Predecessors /><Successors><Successor><Identifier>3451393735</Identifier><MemberName>B4</MemberName><OwnerObject>ClassB</OwnerObject></Successor></Successors></Node><Node><Identifier>3452227813</Identifier><MemberName>B3</MemberName><OwnerObject>ClassB</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>7563067</ObjectIdentifier><CurrentValue>2</CurrentValue><PreviousValue>2</PreviousValue><UpdateOrder>4</UpdateOrder><UpdateLayer>1</UpdateLayer><UpdateStartedAt>14:2:29:848</UpdateStartedAt><UpdateCompletedAt>14:2:29:848</UpdateCompletedAt><UpdateDuration>00:00:00.0000045</UpdateDuration><Predecessors /><Successors><Successor><Identifier>3452293349</Identifier><MemberName>B4</MemberName><OwnerObject>ClassB</OwnerObject></Successor></Successors></Node><Node><Identifier>3452686565</Identifier><MemberName>B2</MemberName><OwnerObject>ClassB</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>7563067</ObjectIdentifier><CurrentValue>2</CurrentValue><PreviousValue>2</PreviousValue><UpdateOrder>5</UpdateOrder><UpdateLayer>1</UpdateLayer><UpdateStartedAt>14:2:29:848</UpdateStartedAt><UpdateCompletedAt>14:2:29:848</UpdateCompletedAt><UpdateDuration>00:00:00.0000039</UpdateDuration><Predecessors /><Successors><Successor><Identifier>3452293349</Identifier><MemberName>B4</MemberName><OwnerObject>ClassB</OwnerObject></Successor></Successors></Node><Node><Identifier>3452621029</Identifier><MemberName>B1</MemberName><OwnerObject>ClassB</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>7563067</ObjectIdentifier><CurrentValue>2</CurrentValue><PreviousValue>2</PreviousValue><UpdateOrder>6</UpdateOrder><UpdateLayer>1</UpdateLayer><UpdateStartedAt>14:2:29:848</UpdateStartedAt><UpdateCompletedAt>14:2:29:848</UpdateCompletedAt><UpdateDuration>00:00:00.0000179</UpdateDuration><Predecessors /><Successors><Successor><Identifier>3452293349</Identifier><MemberName>B4</MemberName><OwnerObject>ClassB</OwnerObject></Successor></Successors></Node><Node><Identifier>3466523524</Identifier><MemberName>A3</MemberName><OwnerObject>ClassA</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>53036123</ObjectIdentifier><CurrentValue>1</CurrentValue><PreviousValue>1</PreviousValue><UpdateOrder>7</UpdateOrder><UpdateLayer>2</UpdateLayer><UpdateStartedAt>14:2:29:848</UpdateStartedAt><UpdateCompletedAt>14:2:29:848</UpdateCompletedAt><UpdateDuration>00:00:00.0000042</UpdateDuration><Predecessors /><Successors><Successor><Identifier>3466589060</Identifier><MemberName>A4</MemberName><OwnerObject>ClassA</OwnerObject></Successor></Successors></Node><Node><Identifier>3465671556</Identifier><MemberName>A2</MemberName><OwnerObject>ClassA</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>53036123</ObjectIdentifier><CurrentValue>1</CurrentValue><PreviousValue>1</PreviousValue><UpdateOrder>8</UpdateOrder><UpdateLayer>2</UpdateLayer><UpdateStartedAt>14:2:29:848</UpdateStartedAt><UpdateCompletedAt>14:2:29:848</UpdateCompletedAt><UpdateDuration>00:00:00.0000042</UpdateDuration><Predecessors /><Successors><Successor><Identifier>3466589060</Identifier><MemberName>A4</MemberName><OwnerObject>ClassA</OwnerObject></Successor></Successors></Node><Node><Identifier>3465606020</Identifier><MemberName>A1</MemberName><OwnerObject>ClassA</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>53036123</ObjectIdentifier><CurrentValue>1</CurrentValue><PreviousValue>1</PreviousValue><UpdateOrder>9</UpdateOrder><UpdateLayer>2</UpdateLayer><UpdateStartedAt>14:2:29:848</UpdateStartedAt><UpdateCompletedAt>14:2:29:848</UpdateCompletedAt><UpdateDuration>00:00:00.0000046</UpdateDuration><Predecessors /><Successors><Successor><Identifier>3466589060</Identifier><MemberName>A4</MemberName><OwnerObject>ClassA</OwnerObject></Successor></Successors></Node><Node><Identifier>3466589060</Identifier><MemberName>A4</MemberName><OwnerObject>ClassA</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>53036123</ObjectIdentifier><CurrentValue>1</CurrentValue><PreviousValue>1</PreviousValue><UpdateOrder>10</UpdateOrder><UpdateLayer>1</UpdateLayer><UpdateStartedAt>14:2:29:849</UpdateStartedAt><UpdateCompletedAt>14:2:29:849</UpdateCompletedAt><UpdateDuration>00:00:00.0000049</UpdateDuration><Predecessors><Predecessor><Identifier>3465606020</Identifier><MemberName>A1</MemberName><OwnerObject>ClassA</OwnerObject></Predecessor><Predecessor><Identifier>3465671556</Identifier><MemberName>A2</MemberName><OwnerObject>ClassA</OwnerObject></Predecessor><Predecessor><Identifier>3466523524</Identifier><MemberName>A3</MemberName><OwnerObject>ClassA</OwnerObject></Predecessor></Predecessors><Successors><Successor><Identifier>3451393735</Identifier><MemberName>B4</MemberName><OwnerObject>ClassB</OwnerObject></Successor></Successors></Node><Node><Identifier>3451393735</Identifier><MemberName>B4</MemberName><OwnerObject>ClassB</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>958745</ObjectIdentifier><CurrentValue>2</CurrentValue><PreviousValue>2</PreviousValue><UpdateOrder>11</UpdateOrder><UpdateLayer>0</UpdateLayer><UpdateStartedAt>14:2:29:849</UpdateStartedAt><UpdateCompletedAt>14:2:29:849</UpdateCompletedAt><UpdateDuration>00:00:00.0000048</UpdateDuration><Predecessors><Predecessor><Identifier>3451328199</Identifier><MemberName>B1</MemberName><OwnerObject>ClassB</OwnerObject></Predecessor><Predecessor><Identifier>3451262663</Identifier><MemberName>B2</MemberName><OwnerObject>ClassB</OwnerObject></Predecessor><Predecessor><Identifier>3451459271</Identifier><MemberName>B3</MemberName><OwnerObject>ClassB</OwnerObject></Predecessor><Predecessor><Identifier>3466589060</Identifier><MemberName>A4</MemberName><OwnerObject>ClassA</OwnerObject></Predecessor></Predecessors><Successors /></Node><Node><Identifier>3467846158</Identifier><MemberName>A3</MemberName><OwnerObject>ClassA</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>50632145</ObjectIdentifier><CurrentValue>1</CurrentValue><PreviousValue>1</PreviousValue><UpdateOrder>12</UpdateOrder><UpdateLayer>2</UpdateLayer><UpdateStartedAt>14:2:29:849</UpdateStartedAt><UpdateCompletedAt>14:2:29:849</UpdateCompletedAt><UpdateDuration>00:00:00.0000041</UpdateDuration><Predecessors /><Successors><Successor><Identifier>3467780622</Identifier><MemberName>A4</MemberName><OwnerObject>ClassA</OwnerObject></Successor></Successors></Node><Node><Identifier>3468435982</Identifier><MemberName>A2</MemberName><OwnerObject>ClassA</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>50632145</ObjectIdentifier><CurrentValue>1</CurrentValue><PreviousValue>1</PreviousValue><UpdateOrder>13</UpdateOrder><UpdateLayer>2</UpdateLayer><UpdateStartedAt>14:2:29:849</UpdateStartedAt><UpdateCompletedAt>14:2:29:849</UpdateCompletedAt><UpdateDuration>00:00:00.0000035</UpdateDuration><Predecessors /><Successors><Successor><Identifier>3467780622</Identifier><MemberName>A4</MemberName><OwnerObject>ClassA</OwnerObject></Successor></Successors></Node><Node><Identifier>3468501518</Identifier><MemberName>A1</MemberName><OwnerObject>ClassA</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>50632145</ObjectIdentifier><CurrentValue>1</CurrentValue><PreviousValue>1</PreviousValue><UpdateOrder>14</UpdateOrder><UpdateLayer>2</UpdateLayer><UpdateStartedAt>14:2:29:849</UpdateStartedAt><UpdateCompletedAt>14:2:29:849</UpdateCompletedAt><UpdateDuration>00:00:00.0000038</UpdateDuration><Predecessors /><Successors><Successor><Identifier>3467780622</Identifier><MemberName>A4</MemberName><OwnerObject>ClassA</OwnerObject></Successor></Successors></Node><Node><Identifier>3467780622</Identifier><MemberName>A4</MemberName><OwnerObject>ClassA</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>50632145</ObjectIdentifier><CurrentValue>1</CurrentValue><PreviousValue>1</PreviousValue><UpdateOrder>15</UpdateOrder><UpdateLayer>1</UpdateLayer><UpdateStartedAt>14:2:29:849</UpdateStartedAt><UpdateCompletedAt>14:2:29:849</UpdateCompletedAt><UpdateDuration>00:00:00.0000041</UpdateDuration><Predecessors><Predecessor><Identifier>3468501518</Identifier><MemberName>A1</MemberName><OwnerObject>ClassA</OwnerObject></Predecessor><Predecessor><Identifier>3468435982</Identifier><MemberName>A2</MemberName><OwnerObject>ClassA</OwnerObject></Predecessor><Predecessor><Identifier>3467846158</Identifier><MemberName>A3</MemberName><OwnerObject>ClassA</OwnerObject></Predecessor></Predecessors><Successors><Successor><Identifier>3452293349</Identifier><MemberName>B4</MemberName><OwnerObject>ClassB</OwnerObject></Successor></Successors></Node><Node><Identifier>3452293349</Identifier><MemberName>B4</MemberName><OwnerObject>ClassB</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>7563067</ObjectIdentifier><CurrentValue>2</CurrentValue><PreviousValue>2</PreviousValue><UpdateOrder>16</UpdateOrder><UpdateLayer>0</UpdateLayer><UpdateStartedAt>14:2:29:849</UpdateStartedAt><UpdateCompletedAt>14:2:29:849</UpdateCompletedAt><UpdateDuration>00:00:00.0000041</UpdateDuration><Predecessors><Predecessor><Identifier>3452621029</Identifier><MemberName>B1</MemberName><OwnerObject>ClassB</OwnerObject></Predecessor><Predecessor><Identifier>3452686565</Identifier><MemberName>B2</MemberName><OwnerObject>ClassB</OwnerObject></Predecessor><Predecessor><Identifier>3452227813</Identifier><MemberName>B3</MemberName><OwnerObject>ClassB</OwnerObject></Predecessor><Predecessor><Identifier>3467780622</Identifier><MemberName>A4</MemberName><OwnerObject>ClassA</OwnerObject></Predecessor></Predecessors><Successors /></Node></Nodes></UpdateProcess>";
        }

        public static string GetUpdateInfoStringWithUpdateError()
        {
            return "<UpdateProcess><UpdateSuccessful>True</UpdateSuccessful><UpdateStartedAt>14:2:29:848</UpdateStartedAt><UpdateEndedAt>14:2:29:849</UpdateEndedAt><UpdateDuration>00:00:00.0010000</UpdateDuration><UpdateError><FailedNode><Identifier>3451262663</Identifier><MemberName>B2</MemberName><OwnerObject>ClassB</OwnerObject></FailedNode><SourceException>Null reference exception!</SourceException><StackTrace>Test stack trace</StackTrace></UpdateError><UpdateCause><Message>Complete graph update requested!</Message></UpdateCause><Reactor><Identifier>Example_1_6</Identifier><Graph><Identifier>Example_1_6</Identifier><TotalNodeCount>16</TotalNodeCount></Graph></Reactor><Nodes><Node><Identifier>3451459271</Identifier><MemberName>B3</MemberName><OwnerObject>ClassB</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>958745</ObjectIdentifier><CurrentValue>2</CurrentValue><PreviousValue>2</PreviousValue><UpdateOrder>1</UpdateOrder><UpdateLayer>1</UpdateLayer><UpdateStartedAt>14:2:29:848</UpdateStartedAt><UpdateCompletedAt>14:2:29:848</UpdateCompletedAt><UpdateDuration>00:00:00.0000058</UpdateDuration><Predecessors /><Successors><Successor><Identifier>3451393735</Identifier><MemberName>B4</MemberName><OwnerObject>ClassB</OwnerObject></Successor></Successors></Node><Node><Identifier>3451262663</Identifier><MemberName>B2</MemberName><OwnerObject>ClassB</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>958745</ObjectIdentifier><CurrentValue>2</CurrentValue><PreviousValue>2</PreviousValue><UpdateOrder>2</UpdateOrder><UpdateLayer>1</UpdateLayer><UpdateStartedAt>14:2:29:848</UpdateStartedAt><UpdateCompletedAt>14:2:29:848</UpdateCompletedAt><UpdateDuration>00:00:00.0000046</UpdateDuration><Predecessors /><Successors><Successor><Identifier>3451393735</Identifier><MemberName>B4</MemberName><OwnerObject>ClassB</OwnerObject></Successor></Successors></Node><Node><Identifier>3451328199</Identifier><MemberName>B1</MemberName><OwnerObject>ClassB</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>958745</ObjectIdentifier><CurrentValue>2</CurrentValue><PreviousValue>2</PreviousValue><UpdateOrder>3</UpdateOrder><UpdateLayer>1</UpdateLayer><UpdateStartedAt>14:2:29:848</UpdateStartedAt><UpdateCompletedAt>14:2:29:848</UpdateCompletedAt><UpdateDuration>00:00:00.0000300</UpdateDuration><Predecessors /><Successors><Successor><Identifier>3451393735</Identifier><MemberName>B4</MemberName><OwnerObject>ClassB</OwnerObject></Successor></Successors></Node><Node><Identifier>3452227813</Identifier><MemberName>B3</MemberName><OwnerObject>ClassB</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>7563067</ObjectIdentifier><CurrentValue>2</CurrentValue><PreviousValue>2</PreviousValue><UpdateOrder>4</UpdateOrder><UpdateLayer>1</UpdateLayer><UpdateStartedAt>14:2:29:848</UpdateStartedAt><UpdateCompletedAt>14:2:29:848</UpdateCompletedAt><UpdateDuration>00:00:00.0000045</UpdateDuration><Predecessors /><Successors><Successor><Identifier>3452293349</Identifier><MemberName>B4</MemberName><OwnerObject>ClassB</OwnerObject></Successor></Successors></Node><Node><Identifier>3452686565</Identifier><MemberName>B2</MemberName><OwnerObject>ClassB</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>7563067</ObjectIdentifier><CurrentValue>2</CurrentValue><PreviousValue>2</PreviousValue><UpdateOrder>5</UpdateOrder><UpdateLayer>1</UpdateLayer><UpdateStartedAt>14:2:29:848</UpdateStartedAt><UpdateCompletedAt>14:2:29:848</UpdateCompletedAt><UpdateDuration>00:00:00.0000039</UpdateDuration><Predecessors /><Successors><Successor><Identifier>3452293349</Identifier><MemberName>B4</MemberName><OwnerObject>ClassB</OwnerObject></Successor></Successors></Node><Node><Identifier>3452621029</Identifier><MemberName>B1</MemberName><OwnerObject>ClassB</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>7563067</ObjectIdentifier><CurrentValue>2</CurrentValue><PreviousValue>2</PreviousValue><UpdateOrder>6</UpdateOrder><UpdateLayer>1</UpdateLayer><UpdateStartedAt>14:2:29:848</UpdateStartedAt><UpdateCompletedAt>14:2:29:848</UpdateCompletedAt><UpdateDuration>00:00:00.0000179</UpdateDuration><Predecessors /><Successors><Successor><Identifier>3452293349</Identifier><MemberName>B4</MemberName><OwnerObject>ClassB</OwnerObject></Successor></Successors></Node><Node><Identifier>3466523524</Identifier><MemberName>A3</MemberName><OwnerObject>ClassA</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>53036123</ObjectIdentifier><CurrentValue>1</CurrentValue><PreviousValue>1</PreviousValue><UpdateOrder>7</UpdateOrder><UpdateLayer>2</UpdateLayer><UpdateStartedAt>14:2:29:848</UpdateStartedAt><UpdateCompletedAt>14:2:29:848</UpdateCompletedAt><UpdateDuration>00:00:00.0000042</UpdateDuration><Predecessors /><Successors><Successor><Identifier>3466589060</Identifier><MemberName>A4</MemberName><OwnerObject>ClassA</OwnerObject></Successor></Successors></Node><Node><Identifier>3465671556</Identifier><MemberName>A2</MemberName><OwnerObject>ClassA</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>53036123</ObjectIdentifier><CurrentValue>1</CurrentValue><PreviousValue>1</PreviousValue><UpdateOrder>8</UpdateOrder><UpdateLayer>2</UpdateLayer><UpdateStartedAt>14:2:29:848</UpdateStartedAt><UpdateCompletedAt>14:2:29:848</UpdateCompletedAt><UpdateDuration>00:00:00.0000042</UpdateDuration><Predecessors /><Successors><Successor><Identifier>3466589060</Identifier><MemberName>A4</MemberName><OwnerObject>ClassA</OwnerObject></Successor></Successors></Node><Node><Identifier>3465606020</Identifier><MemberName>A1</MemberName><OwnerObject>ClassA</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>53036123</ObjectIdentifier><CurrentValue>1</CurrentValue><PreviousValue>1</PreviousValue><UpdateOrder>9</UpdateOrder><UpdateLayer>2</UpdateLayer><UpdateStartedAt>14:2:29:848</UpdateStartedAt><UpdateCompletedAt>14:2:29:848</UpdateCompletedAt><UpdateDuration>00:00:00.0000046</UpdateDuration><Predecessors /><Successors><Successor><Identifier>3466589060</Identifier><MemberName>A4</MemberName><OwnerObject>ClassA</OwnerObject></Successor></Successors></Node><Node><Identifier>3466589060</Identifier><MemberName>A4</MemberName><OwnerObject>ClassA</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>53036123</ObjectIdentifier><CurrentValue>1</CurrentValue><PreviousValue>1</PreviousValue><UpdateOrder>10</UpdateOrder><UpdateLayer>1</UpdateLayer><UpdateStartedAt>14:2:29:849</UpdateStartedAt><UpdateCompletedAt>14:2:29:849</UpdateCompletedAt><UpdateDuration>00:00:00.0000049</UpdateDuration><Predecessors><Predecessor><Identifier>3465606020</Identifier><MemberName>A1</MemberName><OwnerObject>ClassA</OwnerObject></Predecessor><Predecessor><Identifier>3465671556</Identifier><MemberName>A2</MemberName><OwnerObject>ClassA</OwnerObject></Predecessor><Predecessor><Identifier>3466523524</Identifier><MemberName>A3</MemberName><OwnerObject>ClassA</OwnerObject></Predecessor></Predecessors><Successors><Successor><Identifier>3451393735</Identifier><MemberName>B4</MemberName><OwnerObject>ClassB</OwnerObject></Successor></Successors></Node><Node><Identifier>3451393735</Identifier><MemberName>B4</MemberName><OwnerObject>ClassB</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>958745</ObjectIdentifier><CurrentValue>2</CurrentValue><PreviousValue>2</PreviousValue><UpdateOrder>11</UpdateOrder><UpdateLayer>0</UpdateLayer><UpdateStartedAt>14:2:29:849</UpdateStartedAt><UpdateCompletedAt>14:2:29:849</UpdateCompletedAt><UpdateDuration>00:00:00.0000048</UpdateDuration><Predecessors><Predecessor><Identifier>3451328199</Identifier><MemberName>B1</MemberName><OwnerObject>ClassB</OwnerObject></Predecessor><Predecessor><Identifier>3451262663</Identifier><MemberName>B2</MemberName><OwnerObject>ClassB</OwnerObject></Predecessor><Predecessor><Identifier>3451459271</Identifier><MemberName>B3</MemberName><OwnerObject>ClassB</OwnerObject></Predecessor><Predecessor><Identifier>3466589060</Identifier><MemberName>A4</MemberName><OwnerObject>ClassA</OwnerObject></Predecessor></Predecessors><Successors /></Node><Node><Identifier>3467846158</Identifier><MemberName>A3</MemberName><OwnerObject>ClassA</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>50632145</ObjectIdentifier><CurrentValue>1</CurrentValue><PreviousValue>1</PreviousValue><UpdateOrder>12</UpdateOrder><UpdateLayer>2</UpdateLayer><UpdateStartedAt>14:2:29:849</UpdateStartedAt><UpdateCompletedAt>14:2:29:849</UpdateCompletedAt><UpdateDuration>00:00:00.0000041</UpdateDuration><Predecessors /><Successors><Successor><Identifier>3467780622</Identifier><MemberName>A4</MemberName><OwnerObject>ClassA</OwnerObject></Successor></Successors></Node><Node><Identifier>3468435982</Identifier><MemberName>A2</MemberName><OwnerObject>ClassA</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>50632145</ObjectIdentifier><CurrentValue>1</CurrentValue><PreviousValue>1</PreviousValue><UpdateOrder>13</UpdateOrder><UpdateLayer>2</UpdateLayer><UpdateStartedAt>14:2:29:849</UpdateStartedAt><UpdateCompletedAt>14:2:29:849</UpdateCompletedAt><UpdateDuration>00:00:00.0000035</UpdateDuration><Predecessors /><Successors><Successor><Identifier>3467780622</Identifier><MemberName>A4</MemberName><OwnerObject>ClassA</OwnerObject></Successor></Successors></Node><Node><Identifier>3468501518</Identifier><MemberName>A1</MemberName><OwnerObject>ClassA</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>50632145</ObjectIdentifier><CurrentValue>1</CurrentValue><PreviousValue>1</PreviousValue><UpdateOrder>14</UpdateOrder><UpdateLayer>2</UpdateLayer><UpdateStartedAt>14:2:29:849</UpdateStartedAt><UpdateCompletedAt>14:2:29:849</UpdateCompletedAt><UpdateDuration>00:00:00.0000038</UpdateDuration><Predecessors /><Successors><Successor><Identifier>3467780622</Identifier><MemberName>A4</MemberName><OwnerObject>ClassA</OwnerObject></Successor></Successors></Node><Node><Identifier>3467780622</Identifier><MemberName>A4</MemberName><OwnerObject>ClassA</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>50632145</ObjectIdentifier><CurrentValue>1</CurrentValue><PreviousValue>1</PreviousValue><UpdateOrder>15</UpdateOrder><UpdateLayer>1</UpdateLayer><UpdateStartedAt>14:2:29:849</UpdateStartedAt><UpdateCompletedAt>14:2:29:849</UpdateCompletedAt><UpdateDuration>00:00:00.0000041</UpdateDuration><Predecessors><Predecessor><Identifier>3468501518</Identifier><MemberName>A1</MemberName><OwnerObject>ClassA</OwnerObject></Predecessor><Predecessor><Identifier>3468435982</Identifier><MemberName>A2</MemberName><OwnerObject>ClassA</OwnerObject></Predecessor><Predecessor><Identifier>3467846158</Identifier><MemberName>A3</MemberName><OwnerObject>ClassA</OwnerObject></Predecessor></Predecessors><Successors><Successor><Identifier>3452293349</Identifier><MemberName>B4</MemberName><OwnerObject>ClassB</OwnerObject></Successor></Successors></Node><Node><Identifier>3452293349</Identifier><MemberName>B4</MemberName><OwnerObject>ClassB</OwnerObject><NodeType>PropertyNode</NodeType><ObjectIdentifier>7563067</ObjectIdentifier><CurrentValue>2</CurrentValue><PreviousValue>2</PreviousValue><UpdateOrder>16</UpdateOrder><UpdateLayer>0</UpdateLayer><UpdateStartedAt>14:2:29:849</UpdateStartedAt><UpdateCompletedAt>14:2:29:849</UpdateCompletedAt><UpdateDuration>00:00:00.0000041</UpdateDuration><Predecessors><Predecessor><Identifier>3452621029</Identifier><MemberName>B1</MemberName><OwnerObject>ClassB</OwnerObject></Predecessor><Predecessor><Identifier>3452686565</Identifier><MemberName>B2</MemberName><OwnerObject>ClassB</OwnerObject></Predecessor><Predecessor><Identifier>3452227813</Identifier><MemberName>B3</MemberName><OwnerObject>ClassB</OwnerObject></Predecessor><Predecessor><Identifier>3467780622</Identifier><MemberName>A4</MemberName><OwnerObject>ClassA</OwnerObject></Predecessor></Predecessors><Successors /></Node></Nodes></UpdateProcess>";
        }

        public static string GetUpdateInfoStringWithCause()
        {
            return "<UpdateProcess><UpdateSuccessful>True</UpdateSuccessful><UpdateStartedAt>14:2:29:848</UpdateStartedAt><UpdateEndedAt>14:2:29:849</UpdateEndedAt><UpdateDuration>00:00:00.0010000</UpdateDuration><UpdateCause><Message>Complete graph update requested!</Message><InitialNode><Identifier>3451262663</Identifier><MemberName>B2</MemberName><OwnerObject>ClassB</OwnerObject><CurrentValue>10</CurrentValue><PreviousValue>9</PreviousValue></InitialNode></UpdateCause><Reactor><Identifier>Example_1_6</Identifier><Graph><Identifier>Example_1_6</Identifier><TotalNodeCount>16</TotalNodeCount></Graph></Reactor></UpdateProcess>";
        }

        #endregion

        public static string GetReactorXML()
        {
            return "<Reactor><Identifier>Example_1_6</Identifier><Graph><Identifier>Example_1_6</Identifier><TotalNodeCount>16</TotalNodeCount><Nodes><Node><Identifier>3467780622</Identifier><MemberName>A4</MemberName><NodeType>PropertyNode</NodeType><CurrentValue>1</CurrentValue><PreviousValue>1</PreviousValue><OwnerObject><Identifier>50632145</Identifier><Name>Objekt 1</Name><OwnerClass><Identifier>2134988773</Identifier><Name>ClassA</Name><FullName>WindowsFormsApplication16.Examples._01.ClassA</FullName><OwnerNamespace><Identifier>2891102515</Identifier><Name>WindowsFormsApplication16.Examples._01</Name></OwnerNamespace><OwnerAssembly><Identifier>4032828</Identifier><Name>WindowsFormsApplication16.exe</Name></OwnerAssembly></OwnerClass></OwnerObject><Predecessors><Predecessor><Identifier>3468501518</Identifier><MemberName>A1</MemberName></Predecessor><Predecessor><Identifier>3468435982</Identifier><MemberName>A2</MemberName></Predecessor><Predecessor><Identifier>3467846158</Identifier><MemberName>A3</MemberName></Predecessor></Predecessors><Successors><Successor><Identifier>3452293349</Identifier><MemberName>B4</MemberName></Successor></Successors></Node><Node><Identifier>3468501518</Identifier><MemberName>A1</MemberName><NodeType>PropertyNode</NodeType><CurrentValue>1</CurrentValue><PreviousValue>1</PreviousValue><OwnerObject><Identifier>50632145</Identifier><Name>Objekt 1</Name><OwnerClass><Identifier>2134988773</Identifier><Name>ClassA</Name><FullName>WindowsFormsApplication16.Examples._01.ClassA</FullName><OwnerNamespace><Identifier>2891102515</Identifier><Name>WindowsFormsApplication16.Examples._01</Name></OwnerNamespace><OwnerAssembly><Identifier>4032828</Identifier><Name>WindowsFormsApplication16.exe</Name></OwnerAssembly></OwnerClass></OwnerObject><Predecessors /><Successors><Successor><Identifier>3467780622</Identifier><MemberName>A4</MemberName></Successor></Successors></Node><Node><Identifier>3468435982</Identifier><MemberName>A2</MemberName><NodeType>PropertyNode</NodeType><CurrentValue>1</CurrentValue><PreviousValue>1</PreviousValue><OwnerObject><Identifier>50632145</Identifier><Name>Objekt 1</Name><OwnerClass><Identifier>2134988773</Identifier><Name>ClassA</Name><FullName>WindowsFormsApplication16.Examples._01.ClassA</FullName><OwnerNamespace><Identifier>2891102515</Identifier><Name>WindowsFormsApplication16.Examples._01</Name></OwnerNamespace><OwnerAssembly><Identifier>4032828</Identifier><Name>WindowsFormsApplication16.exe</Name></OwnerAssembly></OwnerClass></OwnerObject><Predecessors /><Successors><Successor><Identifier>3467780622</Identifier><MemberName>A4</MemberName></Successor></Successors></Node><Node><Identifier>3467846158</Identifier><MemberName>A3</MemberName><NodeType>PropertyNode</NodeType><CurrentValue>1</CurrentValue><PreviousValue>1</PreviousValue><OwnerObject><Identifier>50632145</Identifier><Name>Objekt 1</Name><OwnerClass><Identifier>2134988773</Identifier><Name>ClassA</Name><FullName>WindowsFormsApplication16.Examples._01.ClassA</FullName><OwnerNamespace><Identifier>2891102515</Identifier><Name>WindowsFormsApplication16.Examples._01</Name></OwnerNamespace><OwnerAssembly><Identifier>4032828</Identifier><Name>WindowsFormsApplication16.exe</Name></OwnerAssembly></OwnerClass></OwnerObject><Predecessors /><Successors><Successor><Identifier>3467780622</Identifier><MemberName>A4</MemberName></Successor></Successors></Node><Node><Identifier>3466589060</Identifier><MemberName>A4</MemberName><NodeType>PropertyNode</NodeType><CurrentValue>1</CurrentValue><PreviousValue>1</PreviousValue><OwnerObject><Identifier>53036123</Identifier><Name>Objekt 2</Name><OwnerClass><Identifier>2134988773</Identifier><Name>ClassA</Name><FullName>WindowsFormsApplication16.Examples._01.ClassA</FullName><OwnerNamespace><Identifier>2891102515</Identifier><Name>WindowsFormsApplication16.Examples._01</Name></OwnerNamespace><OwnerAssembly><Identifier>4032828</Identifier><Name>WindowsFormsApplication16.exe</Name></OwnerAssembly></OwnerClass></OwnerObject><Predecessors><Predecessor><Identifier>3465606020</Identifier><MemberName>A1</MemberName></Predecessor><Predecessor><Identifier>3465671556</Identifier><MemberName>A2</MemberName></Predecessor><Predecessor><Identifier>3466523524</Identifier><MemberName>A3</MemberName></Predecessor></Predecessors><Successors><Successor><Identifier>3451393735</Identifier><MemberName>B4</MemberName></Successor></Successors></Node><Node><Identifier>3465606020</Identifier><MemberName>A1</MemberName><NodeType>PropertyNode</NodeType><CurrentValue>1</CurrentValue><PreviousValue>1</PreviousValue><OwnerObject><Identifier>53036123</Identifier><Name>Objekt 2</Name><OwnerClass><Identifier>2134988773</Identifier><Name>ClassA</Name><FullName>WindowsFormsApplication16.Examples._01.ClassA</FullName><OwnerNamespace><Identifier>2891102515</Identifier><Name>WindowsFormsApplication16.Examples._01</Name></OwnerNamespace><OwnerAssembly><Identifier>4032828</Identifier><Name>WindowsFormsApplication16.exe</Name></OwnerAssembly></OwnerClass></OwnerObject><Predecessors /><Successors><Successor><Identifier>3466589060</Identifier><MemberName>A4</MemberName></Successor></Successors></Node><Node><Identifier>3465671556</Identifier><MemberName>A2</MemberName><NodeType>PropertyNode</NodeType><CurrentValue>1</CurrentValue><PreviousValue>1</PreviousValue><OwnerObject><Identifier>53036123</Identifier><Name>Objekt 2</Name><OwnerClass><Identifier>2134988773</Identifier><Name>ClassA</Name><FullName>WindowsFormsApplication16.Examples._01.ClassA</FullName><OwnerNamespace><Identifier>2891102515</Identifier><Name>WindowsFormsApplication16.Examples._01</Name></OwnerNamespace><OwnerAssembly><Identifier>4032828</Identifier><Name>WindowsFormsApplication16.exe</Name></OwnerAssembly></OwnerClass></OwnerObject><Predecessors /><Successors><Successor><Identifier>3466589060</Identifier><MemberName>A4</MemberName></Successor></Successors></Node><Node><Identifier>3466523524</Identifier><MemberName>A3</MemberName><NodeType>PropertyNode</NodeType><CurrentValue>1</CurrentValue><PreviousValue>1</PreviousValue><OwnerObject><Identifier>53036123</Identifier><Name>Objekt 2</Name><OwnerClass><Identifier>2134988773</Identifier><Name>ClassA</Name><FullName>WindowsFormsApplication16.Examples._01.ClassA</FullName><OwnerNamespace><Identifier>2891102515</Identifier><Name>WindowsFormsApplication16.Examples._01</Name></OwnerNamespace><OwnerAssembly><Identifier>4032828</Identifier><Name>WindowsFormsApplication16.exe</Name></OwnerAssembly></OwnerClass></OwnerObject><Predecessors /><Successors><Successor><Identifier>3466589060</Identifier><MemberName>A4</MemberName></Successor></Successors></Node><Node><Identifier>3452293349</Identifier><MemberName>B4</MemberName><NodeType>PropertyNode</NodeType><CurrentValue>2</CurrentValue><PreviousValue>2</PreviousValue><OwnerObject><Identifier>7563067</Identifier><Name>WindowsFormsApplication16.Examples._01.ClassB</Name><OwnerClass><Identifier>595075089</Identifier><Name>ClassB</Name><FullName>WindowsFormsApplication16.Examples._01.ClassB</FullName><OwnerNamespace><Identifier>2891102515</Identifier><Name>WindowsFormsApplication16.Examples._01</Name></OwnerNamespace><OwnerAssembly><Identifier>4032828</Identifier><Name>WindowsFormsApplication16.exe</Name></OwnerAssembly></OwnerClass></OwnerObject><Predecessors><Predecessor><Identifier>3452621029</Identifier><MemberName>B1</MemberName></Predecessor><Predecessor><Identifier>3452686565</Identifier><MemberName>B2</MemberName></Predecessor><Predecessor><Identifier>3452227813</Identifier><MemberName>B3</MemberName></Predecessor><Predecessor><Identifier>3467780622</Identifier><MemberName>A4</MemberName></Predecessor></Predecessors><Successors /></Node><Node><Identifier>3452621029</Identifier><MemberName>B1</MemberName><NodeType>PropertyNode</NodeType><CurrentValue>2</CurrentValue><PreviousValue>2</PreviousValue><OwnerObject><Identifier>7563067</Identifier><Name>WindowsFormsApplication16.Examples._01.ClassB</Name><OwnerClass><Identifier>595075089</Identifier><Name>ClassB</Name><FullName>WindowsFormsApplication16.Examples._01.ClassB</FullName><OwnerNamespace><Identifier>2891102515</Identifier><Name>WindowsFormsApplication16.Examples._01</Name></OwnerNamespace><OwnerAssembly><Identifier>4032828</Identifier><Name>WindowsFormsApplication16.exe</Name></OwnerAssembly></OwnerClass></OwnerObject><Predecessors /><Successors><Successor><Identifier>3452293349</Identifier><MemberName>B4</MemberName></Successor></Successors></Node><Node><Identifier>3452686565</Identifier><MemberName>B2</MemberName><NodeType>PropertyNode</NodeType><CurrentValue>2</CurrentValue><PreviousValue>2</PreviousValue><OwnerObject><Identifier>7563067</Identifier><Name>WindowsFormsApplication16.Examples._01.ClassB</Name><OwnerClass><Identifier>595075089</Identifier><Name>ClassB</Name><FullName>WindowsFormsApplication16.Examples._01.ClassB</FullName><OwnerNamespace><Identifier>2891102515</Identifier><Name>WindowsFormsApplication16.Examples._01</Name></OwnerNamespace><OwnerAssembly><Identifier>4032828</Identifier><Name>WindowsFormsApplication16.exe</Name></OwnerAssembly></OwnerClass></OwnerObject><Predecessors /><Successors><Successor><Identifier>3452293349</Identifier><MemberName>B4</MemberName></Successor></Successors></Node><Node><Identifier>3452227813</Identifier><MemberName>B3</MemberName><NodeType>PropertyNode</NodeType><CurrentValue>2</CurrentValue><PreviousValue>2</PreviousValue><OwnerObject><Identifier>7563067</Identifier><Name>WindowsFormsApplication16.Examples._01.ClassB</Name><OwnerClass><Identifier>595075089</Identifier><Name>ClassB</Name><FullName>WindowsFormsApplication16.Examples._01.ClassB</FullName><OwnerNamespace><Identifier>2891102515</Identifier><Name>WindowsFormsApplication16.Examples._01</Name></OwnerNamespace><OwnerAssembly><Identifier>4032828</Identifier><Name>WindowsFormsApplication16.exe</Name></OwnerAssembly></OwnerClass></OwnerObject><Predecessors /><Successors><Successor><Identifier>3452293349</Identifier><MemberName>B4</MemberName></Successor></Successors></Node><Node><Identifier>3451393735</Identifier><MemberName>B4</MemberName><NodeType>PropertyNode</NodeType><CurrentValue>2</CurrentValue><PreviousValue>2</PreviousValue><OwnerObject><Identifier>958745</Identifier><Name>WindowsFormsApplication16.Examples._01.ClassB</Name><OwnerClass><Identifier>595075089</Identifier><Name>ClassB</Name><FullName>WindowsFormsApplication16.Examples._01.ClassB</FullName><OwnerNamespace><Identifier>2891102515</Identifier><Name>WindowsFormsApplication16.Examples._01</Name></OwnerNamespace><OwnerAssembly><Identifier>4032828</Identifier><Name>WindowsFormsApplication16.exe</Name></OwnerAssembly></OwnerClass></OwnerObject><Predecessors><Predecessor><Identifier>3451328199</Identifier><MemberName>B1</MemberName></Predecessor><Predecessor><Identifier>3451262663</Identifier><MemberName>B2</MemberName></Predecessor><Predecessor><Identifier>3451459271</Identifier><MemberName>B3</MemberName></Predecessor><Predecessor><Identifier>3466589060</Identifier><MemberName>A4</MemberName></Predecessor></Predecessors><Successors /></Node><Node><Identifier>3451328199</Identifier><MemberName>B1</MemberName><NodeType>PropertyNode</NodeType><CurrentValue>2</CurrentValue><PreviousValue>2</PreviousValue><OwnerObject><Identifier>958745</Identifier><Name>WindowsFormsApplication16.Examples._01.ClassB</Name><OwnerClass><Identifier>595075089</Identifier><Name>ClassB</Name><FullName>WindowsFormsApplication16.Examples._01.ClassB</FullName><OwnerNamespace><Identifier>2891102515</Identifier><Name>WindowsFormsApplication16.Examples._01</Name></OwnerNamespace><OwnerAssembly><Identifier>4032828</Identifier><Name>WindowsFormsApplication16.exe</Name></OwnerAssembly></OwnerClass></OwnerObject><Predecessors /><Successors><Successor><Identifier>3451393735</Identifier><MemberName>B4</MemberName></Successor></Successors></Node><Node><Identifier>3451262663</Identifier><MemberName>B2</MemberName><NodeType>PropertyNode</NodeType><CurrentValue>2</CurrentValue><PreviousValue>2</PreviousValue><OwnerObject><Identifier>958745</Identifier><Name>WindowsFormsApplication16.Examples._01.ClassB</Name><OwnerClass><Identifier>595075089</Identifier><Name>ClassB</Name><FullName>WindowsFormsApplication16.Examples._01.ClassB</FullName><OwnerNamespace><Identifier>2891102515</Identifier><Name>WindowsFormsApplication16.Examples._01</Name></OwnerNamespace><OwnerAssembly><Identifier>4032828</Identifier><Name>WindowsFormsApplication16.exe</Name></OwnerAssembly></OwnerClass></OwnerObject><Predecessors /><Successors><Successor><Identifier>3451393735</Identifier><MemberName>B4</MemberName></Successor></Successors></Node><Node><Identifier>3451459271</Identifier><MemberName>B3</MemberName><NodeType>PropertyNode</NodeType><CurrentValue>2</CurrentValue><PreviousValue>2</PreviousValue><OwnerObject><Identifier>958745</Identifier><Name>WindowsFormsApplication16.Examples._01.ClassB</Name><OwnerClass><Identifier>595075089</Identifier><Name>ClassB</Name><FullName>WindowsFormsApplication16.Examples._01.ClassB</FullName><OwnerNamespace><Identifier>2891102515</Identifier><Name>WindowsFormsApplication16.Examples._01</Name></OwnerNamespace><OwnerAssembly><Identifier>4032828</Identifier><Name>WindowsFormsApplication16.exe</Name></OwnerAssembly></OwnerClass></OwnerObject><Predecessors /><Successors><Successor><Identifier>3451393735</Identifier><MemberName>B4</MemberName></Successor></Successors></Node></Nodes></Graph></Reactor>";
        }
    }
}
