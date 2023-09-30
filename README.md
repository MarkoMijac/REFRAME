<!-- PROJECT LOGO -->
<br />
<div align="center">
  <h3 align="center">REFRAME</h3>

  <p align="center">
    A software framework for managing reactive dependencies in object-oriented (OO) applications
    <br 
  </p>
</div>
<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->
## About The Project

![image](https://github.com/MarkoMijac/REFRAME/assets/5802626/5428eed2-4617-4ec6-b1d4-f49247176550)

REFRAME is a software framework which can be used by application developers to manage reactive dependencies in object-oriented (OO) applications. In object-oriented (OO) applications, objects collaborate by invoking each other's behaviour and data, thus forming dependencies. Sometimes there is a need (or benefit) to express these dependencies as reactive, i.e. in a way that when one object changes its data or invokes a metode, its dependent objects automatically react and update their state or invoke their own methods. In practice, such dependencies are often found in event-driven systems, graphical user interfaces, animation, spreadsheet systems, embedded systems, etc. 

When need arises to treat dependencies as reactive dependences in OO applications, developers usually end up developing their own ad-hoc solutions inspired by Observer (or similar) design pattern. Such solutions may be challenging to develop form scratch. However, more importantly, such solutions often do not live up to the challenge of handling large and complex dependency graphs that may form when combining sufficient number of reactive dependencies. Some of the issues that frequently arise: not performing updates that are necessary, performing updates that are not necessary, performing updates redundantly, causing glitches (temporary inconsistencies) due to incorrect update order, creating infinite loops due to circular dependences. These issues may heavily distort results we get from the underlying software, as well as its performance. In order to avoid creating their own solutions from scratch, developers can use REFRAME to: specify individual reactive dependencies, combine reactive dependencies into a dependency graph, perform update process (sequentially or in parallel), analyse constructed dependency graph, visualize constructed dependency graph.

### Built With

* C# .NET
* Visual Studio 2019/2022 with DGML Editor installed
* .NET Framework

<!-- GETTING STARTED -->
## Getting Started

This is an example of how you may give instructions on setting up your project locally.
To get a local copy up and running follow these simple example steps.

### Prerequisites

* C# .NET
* Visual Studio 2019/2022 with DGML Editor installed
* .NET Framework

### Installation

In order to use Reframe framework you need to obtain framework binary files (.dll and .exe files). This can be done either by downloading ready to use binaries from [GitHub Release](https://github.com/MarkoMijac/REFRAME/releases/tag/v1.0.0) page (ReframeCore.zip and ReframeTools.zip files), or by cloning the project and building these files by yourself.

### Use scenarios

Reframe framework consists of 12 .dll files and one .exe file. However, which of these files will be necessary to use depends on the use scenario. 

| Component  | Description |
| ------------- | ------------- |
| ReframeCore  | The core component of the framework which enables end-user application to construct dependency graphs and perform update process. It contains abstractions representing members of reactive node hierarchy, dependency graph, update, scheduler, reactor and other essential parts of the framework.  |
| ReframeBaseExceptions  | Contains the definition of the root REFRAME exception which is inherited by specific exceptions in other components.  |
| ReframeFluentAPI | Contains classes with extension methods which allow us to use reactor in a declarative style. |
| IPCServer | Contains interfaces and abstract classes with reusable part of the server side of inter-process communication. |
| ReframeServer | Contains concrete classes with REFRAME-specific implementation of the server side of inter-process communication. |
| ReframeExporter |  Contains classes responsible for exporting dependency graph data and update process data in a form of XML content. |
| ReframeToolsGUI | Contains graphical user interface classes and coordinates the rest of the components. |
| IPCClient | Contains interfaces and abstract classes with reusable part of the client side of inter-process communication. |
| ReframeClient | Contains concrete classes with REFRAME-specific implementation of the client side of inter-process communication. |
| ReframeAnalyzer | Contains abstractions representing members of analysis graph and analysis node hierarchies, factories for creating graph and node objects, filter specifications, analysis and metrics implementations, and other parts related to graph and update analysis. |
| ReframeImporter | Contains utility classes which interpret XML content fetched from end-user application. |
| ReframeVisualizer | Contains interfaces and abstract classes with reusable and technology-independent part of the visualizer implementation. |
| VisualizerDGML | Contains concrete classes implementing visualizer using DGML technology. |

From the perspective of end-user (host) application, there are three main scenarios.

1. Scenario - Core features

The first, and the most basic use scenario assumes using only two components from REFRAME, namely: ReframeCore.dll and ReframeBaseExceptions.dll. These are mandatory components which allow end-user application to access core features of REFRAME, i.e. to construct dependency graphs and perform update process.

![image](https://github.com/MarkoMijac/REFRAME/assets/5802626/b3a2656d-c6d3-4080-ab3d-9b3373456ee4)

In order to use the core features of REFRAME it is necessary to reference ReframeCore.dll and ReframeBaseExceptions.dll libraries in the project we want to use them in. In order to do that, we first need to obtain dll libraries. This can be done either by downloading ready-to-use binaries from [GitHub Release](https://github.com/MarkoMijac/REFRAME/releases/tag/v1.0.0) page (ReframeCore.zip and ReframeTools.zip files), or by cloning the project and building these files by yourself. When we have required dll files, we need to reference them in our project. Dependening on our IDE of choice (e.g. Visual Studio, Visual Studio Code, Resharper, etc.) this process might be different. In case of Visual Studio we would need to right-click at our project in Solution explorer, choose Add Reference option, and then find the dll files we want to reference.

![image](https://github.com/MarkoMijac/REFRAME/assets/5802626/2c7e273e-9390-43fc-b7f9-7297e69cb3ed)


2. Scenario - Core features + Fluent API

The second use scenario introduces optional ReframeFluentAPI.dll component, which (as its name imply) contains implementation of Fluent interface. This allows us to, in addition to traditional imperative style, also use alternative, more declarative approach when specifying individual reactive dependencies between nodes.

![image](https://github.com/MarkoMijac/REFRAME/assets/5802626/d31b1569-d4f7-493d-884f-21f96a562fce)

Extending the base use scenario with fluent API requires referencing one additional component - ReframeFluentAPI.dll. This is done exactly the same as with the first two dll files.

3. Scenario - Core features + Fluent API + Reframe Tools

The third use scenario is only relevant if we want to use REFRAME tools. It allows us to setup inter-process communication between end-user application and REFRAME tools. By introducing IPCServer.dll, ReframeServer.dll and ReframeExporter.dll components, we can start the server in end-user application which will respond to requests from the Analyzer tool (client) and send dependency graph data. Since integration of server components into end-user application and starting the server is a trivial task, application developers can switch to and from this use scenario easily.

![image](https://github.com/MarkoMijac/REFRAME/assets/5802626/ed1884e9-2510-4d73-9f10-d3c48e5e9a35)

<!-- USAGE EXAMPLES -->
## Usage

### Example 1 - Creating and registering new reactor object
Creating new reactor object with identifier "default" and registering it in ReactorRegistry to be available for further use.

```
var reactor = ReactorRegistry.Instance.CreateReactor("default");

```

### Example 2 - Geting existing "default" reactor object from registry

```
var reactor = ReactorRegistry.Instance.GetReactor("default");

```

### Example 3 - Specifying reactive dependencies

![image](https://github.com/MarkoMijac/REFRAME/assets/5802626/0011c15c-a548-4514-b3a0-4db8acc5f9ee)

### Example 4 - Performing update process of entire graph

In order to perform update process of entire graph we need to fetch reactor object from registry and invoke PerformUpdate method.

```
var reactor = ReactorRegistry.Instance.CreateReactor("default");
reactor.PerformUpdate();
```

### Example 5 - Performing update process as a result of triggering change in object's state

```
public int A
{
  get {return _a; }
  set
  {
    _a = value;
    reactor.Update(this);
  }
}
```

### Example 6 - Performing update process asynchronously

```
var reactor = ReactorRegistry.Instance.GetReactor("default");
(reactor.Updater as Updater).Strategy = UpdateStrategy.Asynchronous;
await reactor.PerformUpdate();
```

### Example 7 - Performing update process in parallel

```
var reactor = ReactorRegistry.Instance.GetReactor("default");
(reactor.Updater as Updater).Strategy = UpdateStrategy.Parallel;
await reactor.PerformUpdate();
```

### Example 8 - Reporting the progress and status of the ongoing update process

Reactor exposes three events that can be used to report on the progress and status of update process. These events are: UpdateStarted, UpdateCompleted and UpdateFailed.

```
var reactor = ReactorRegistry.Instance.GetReactor("default");
reactor.UpdateStarted += Reactor_UpdateStarted;
reactor.UpdateCompleted += Reactor_UpdateCompleted;
reactor.UpdateFailed += Reactor_UpdateFailed;

...

private void Reactor_UpdateStarted(object sender, EventArgs e)
{
  var graph = (sender as IUpdater).Graph;
  MessageBox.Show($"Update process for graph {graph.Identifier} has started!");
}

private void Reactor_UpdateFailed(object sender, EventArgs e)
{
  var error = sender as UpdateError;
  var graph = error.Graph;
  var failedNode = error.FailedNode;
  MessageBox.Show($"There was and error in node {failedNode.Identifier} during update process for graph {graph.Identifier}!");
}

private void Reactor_UpdateCompleted(object sender, EventArgs e)
{
  RefreshGui();
}
```

### Example 9 - Displaying list of reactors

After starting REFRAME Tools a graphical user interface appeares showing the list of registered reactors in end-user application. For each registered
reactor a detailed data about its structure can be displayed.

![image](https://github.com/MarkoMijac/REFRAME/assets/5802626/6138893e-9a4b-40e4-af34-07b9b6aa8618)


### Example 10 - Displaying the list of reactive nodes on a different levels of abstraction

After selecting registered reactor, available menu offers six levels at which reactive nodes can be shown, e.g. object-member level. List of nodes are then displayed in a table with data available at chosen level of abstraction.

![image](https://github.com/MarkoMijac/REFRAME/assets/5802626/aae6ca3d-b9af-4a33-8e7e-968abf5e3774)

### Example 11 - Displaying the list of reactive nodes with respect to their role in dependency graph

After selecting registered reactor and a level of abstraction, it is possible to show only reactive nodes with particular role (filtering by role) in a dependency graph, e.g. source nodes.

![image](https://github.com/MarkoMijac/REFRAME/assets/5802626/078f1863-76a1-4e2e-86e4-85b900250ea4)

### Example 12 - Displaying the list of reactive nodes with associated with selected node

After any list of reactive nodes is displayed, and any reactive node is selected, it is possible to display reactive nodes which are in various ways associated with selected node (filtering by association).

![image](https://github.com/MarkoMijac/REFRAME/assets/5802626/51456069-89a9-4d9e-afd0-96b8e6a67600)

### Example 13 - Displaying information about last performed update process
After selecting registered reactor we can show information about the latest update process. Here we can see information about the overall update process, such as update status, cause, duration, error, etc. Also, we can see the list of all reactive nodes which participated in the update process. In addition to usual information about reactive nodes, for each node we can see the duration of update, as well as whether the update resulted in a value change.

![image](https://github.com/MarkoMijac/REFRAME/assets/5802626/d4011d51-90ac-4516-811e-1a389e61d537)

### Example 14 - Visualizing the list of reactive nodes

After displaying any list of reactive nodes we can visualize it in a form of DGML graph shown in Visual Studio’s DGML Viewer.

![image](https://github.com/MarkoMijac/REFRAME/assets/5802626/606d3aae-0a5f-4a83-8867-7e1a4c17629f)

![image](https://github.com/MarkoMijac/REFRAME/assets/5802626/99a0b1fb-2a6c-42ec-93b9-b21ef62d81e7)


<!-- CONTRIBUTING -->
## Contributing

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<!-- CONTACT -->
## Contact

Marko Mijač, email: mmijac [at] foi.hr
<br>
Project Link: [https://github.com/MarkoMijac/REFRAME](https://github.com/MarkoMijac/REFRAME)

<p align="right">(<a href="#readme-top">back to top</a>)</p>
