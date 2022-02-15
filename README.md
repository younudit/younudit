
<br />
<h1 align="center" style="padding-bottom: 20px">NUD Scanned Images</h1>
<br />

## Documentation

<br />

   - [`README.md`](README.md): 👀 General Information  ⭐ You are here! ⭐
   - RUNBOOK.md: 🤖 DevOps Information - TODO
   - DEVBOOK.md: 👾 Dev Environment information - TODO
   - TESTBOOK.md: 🧪 Test Environment information - TODO
   - CONTRIBUTING.md: 😺 Contributing information - TODO
   - RELEASE.md: 💥 Release information - TODO
 
## Visualization 

   - [`Database Diagram`](https://dbdiagram.io/d/) 📑 Database Diagram - TODO
   - [`API Docs`](/docs): 📝 Generated API Docs, OpenAPI/Swagger, and PostMan Collections - TODO

<br />

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system. This is a template I use for every project-this may be incomplete as there hasn't been time afforded/the organization may not have current ci/cd systems in place. The readme reflects my knowledge at the time of writing and may not be updated over time to reflect the current state of the code unless good principles are maintained.

<br />

### Prerequisites

Project Framework (MS) Requirements:

 - Visual Studio
 - VS Code
 - Asp.net prereqs

Software Requirements:

 - node 15+
 - npm


Note: The initial setup of the software will require a SQL instance to connect to-it can be customized per deployment in the AppSettings file. 

<br />

### Installing

**No Ci/Cd (No Docker)**

 1. Install dependencies (Visual Studio, Asp.net dev kit, Node)
 1. Clone repo into folder
 1. Update appsettings in server folder to point to test environment
 1. cd into cloned client directory
 1. Enter desired configuration parameters/keys in .config files
 1. Install front end dependencies (npm install)
 1. Build front end by running npm run build
 1. Open "server" project in visual studio
 1. Run with IIS Express
 1. Point your client to the local instance of the debug server

You're going to want to point the client at your local instance of the back end server after you click "run".

<br />

## Deployment

You will need to manually deploy to the hosting IIS server. Currently, the code separation allows you to host the client completely separately from the back end if you desire (like on a cdn.)

<br />

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](#).

<br />

## Authors

* **Benn McGuire** - *Initial work* - [email](mailto:benn.mcguire@gmail.com)

<br />

## License
None, not licensed for resale or external use. Contact benn.mcguire@gmail.com or NUD