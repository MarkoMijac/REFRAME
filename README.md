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

_Below is an example of how you can instruct your audience on installing and setting up your app. This template doesn't rely on any external dependencies or services._

1. Get a free API Key at [https://example.com](https://example.com)
2. Clone the repo
   ```sh
   git clone https://github.com/your_username_/Project-Name.git
   ```
3. Install NPM packages
   ```sh
   npm install
   ```
4. Enter your API in `config.js`
   ```js
   const API_KEY = 'ENTER YOUR API';
   ```

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- USAGE EXAMPLES -->
## Usage

Use this space to show useful examples of how a project can be used. Additional screenshots, code examples and demos work well in this space. You may also link to more resources.

_For more examples, please refer to the [Documentation](https://example.com)_

<p align="right">(<a href="#readme-top">back to top</a>)</p>

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
