
# Umbraco MyProject Sample Code

this is a Umbraco MyProject Sample Code using .net core v6.0 and Umbraco v10.0.0

## Table of Contents

- [Installation](#installation)
- [Usage](#usage)
- [Examples](#examples)
- [Documentation](#documentation)
- [Contributing](#contributing)


## Installation

You can install this library using NuGet. Run the following command in the Package Manager Console:

URL : https://our.umbraco.com/Documentation/Fundamentals/Setup/Install/

1. Open your command line
2. Install the Umbraco templates with dotnet new -i Umbraco.Templates
```
dotnet new -i Umbraco.Templates
```
3. Run dotnet new umbraco --name MyProject to create a new project
```
dotnet new umbraco --name MyProject
```
4. Enter the project folder, it will be the folder containing the .csproj file
5. Run and build your project using dotnet run

```
dotnet run
```
6. The console will output a message similar to: [10:57:39 INF] Now listening on: https://localhost:44388
7. Open your browser and navigate to that url
8. Follow instructions on the installer

	> Create .sln project file 
	dotnet new sln --name solutionname 
	> Congigure .sln file with .csproj file
	dotnet sln add "path\projectname.csproj"
```
dotnet run
dotnet sln add "path\projectname.csproj"
```
	
## Usage

## Examples

## Documentation
This should cover list of API/operations supported by the component
```
public ActionResult GetUser(Guid userid);
public ActionResult GetAllUserList();
public IActionResult DeleteUser(Guid userid);
public IActionResult ManageUser(UserViewModel model);

```
## Contribution