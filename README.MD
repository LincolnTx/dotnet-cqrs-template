# Dotnet CQRS Template

This is a project made using dotnet template creating, you could read about it 
here [Microsoft Documentation!](https://docs.microsoft.com/pt-br/dotnet/core/tutorials/cli-templates-create-project-template)

To use it in your machine, you need to clone this repository, open a terminal and navigate to the following folder:
`dotnet-cqrs-template`.

Then you run the command: 
`dotnet new -i .\ `

The command bellow will add this template in your dotnet-cli under 
the name of `cqrstemplate`

Now to start a new project with this template you could use bellow  commands.

1. `dotnet new cqrstemplate --name name.of.your.project` 
2. `dotnet new cqrstemplate -o name.of.your.project` 

The first command will create the solution and all the files 
inside your currently folder. 
The second will use the parameter you'll pass to create a folder with that name.
In both cases the value you'll pass as a parameter will be used as the 
name of your project folders and resources


This template was made by > [Lincoln Texeira](https://github.com/LincolnTx)


