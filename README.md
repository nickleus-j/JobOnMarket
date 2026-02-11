# JobsOnMarket
##  About
This is a practice app that serves as marketplace for outsourcing jobs to contractors.
The information can be accessed and modifed with the API that is connected to a SQL server.
The database is MS-SQL. This has been tested on a Windows machine.

## Requirement to Run installation script
- Windows
- Docker
- Powershell

## Setup to run API's swagger on Windows
- Clone Repository or download source code
- Go to root directory in powershell
- run setup.ps1
- Enter Database Name
- Enter Password for SA account of Sql server
- Wait for docker containers to be deployed
- Browse [http://localhost:5142/index.html](http://localhost:5142/index.html)

## SQL SA password policy
The SA account follows a strong password policy. Otherwise, the setup will fail
- Length: Must be at least 8 characters long
- The password must contain characters from at least three of the following four categories:

  * Latin uppercase letters (A through Z)
  * Latin lowercase letters (a through z)
  * Base 10 digits (0 through 9)
  * Nonalphanumeric characters (symbols such as !, $, #, %, @, ^, &, *, (, ), etc.). 

Examples of valid passwords:

- P@ssw0rd123
 -  Abcd!efgh
-  MyStr0ngP@ssw0rd
-   S0und$ecure

## Other notes
This can also be run via Visual Studio but needs Configuration of Values. Encouraged to use manage usersecrets of the JobsOnMarket project.
Connection string will require a working sql server instace with the right credentials.

## For non Windows Machines
- Download source file
- connect to a sql server remotely or via containers
- place configuration values for connection string and default password for seeded users.
- Run JobsOnMarket api via dotnet or Visual Studio code