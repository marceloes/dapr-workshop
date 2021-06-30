# Assignment 0 - Install tools and pre-requisites

## Assignment goals

In this assignment, you'll configure and make sure you have all the pre-requisites installed on your machine.

## Step 1. Install pre-requisites

1. Install all the pre-requisites listed above and make sure they're working fine

- Git ([download](https://git-scm.com/))
- .NET 5 SDK ([download](https://dotnet.microsoft.com/download/dotnet/5.0))
- Visual Studio Code ([download](https://code.visualstudio.com/download)) with at least the following extensions installed:
  - [C#](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
  - [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client)
- Docker for desktop ([download](https://www.docker.com/products/docker-desktop))
- Dapr CLI and Dapr runtime ([instructions](https://docs.dapr.io/getting-started/install-dapr-selfhost/))

All scripts in the instructions are Powershell scripts. If you're working on a Mac, it is recommended to install Powershell for Mac:

- Powershell for Mac ([instructions](https://docs.microsoft.com/nl-nl/powershell/scripting/install/installing-powershell-core-on-macos?view=powershell-7.1))

Make sure you have at least the following versions installed. This workshop has been tested with the following versions:

| Attribute            | Details |
| -------------------- | ------- |
| Dapr runtime version | v1.0.0  |
| Dapr.NET SDK version | v1.0.0  |
| Dapr CLI version     | v1.0.0  |
| Platform             | .NET 5  |

2. Clone the Github repository to a local folder on your machine:

   ```console
   git clone https://github.com/robvet/dapr-workshop.git
   ```

3. Review  the source code of the different services. You can open the `src` folder in this repo in VS Code. All folders used in the assignments are specified relative to the root of the folder where you have cloned the dapr-workshop repository.

4. Go to [assignment 1](../Assignment01/README.md).

