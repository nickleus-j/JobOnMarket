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

### Examples of valid passwords:

- P@ssw0rd123
 -  Abcd!efgh
-  MyStr0ngP@ssw0rd
-   S0und$ecure

## Setup For non Windows Machines
- Download source file
- connect to a sql server remotely or via containers
- place configuration values for connection string and default password of seeded users.
- Run JobsOnMarket api via dotnet or Visual Studio code
- Browse [http://localhost:5142/index.html](http://localhost:5142/index.html)

## Notes for Running the enpoints on Swagger
- Need to Authenticte to Authorize.
- Feel free to authenticate as a Customer or contractor.
- Different endpoints will available depending on Endpoints.
- Use the default password for all IdentityUsers stated in the .docker compose file.
- The default password can be set in user secrets of  the API project and applied upon reseed.
- Reseed can be done by deleting the database & rerun the API project.

# Authorize in Swagger
- try enpoint /api/Auth/login
- Use the registration Endpoint and put Customer or Contrctor as the role 
- Copy the JWT token and paste it to the textbox that appears after clicking the authorize button on swagger.
- Logout by clicking authorize button again.

## Other notes
This can also be run via Visual Studio but needs Configuration of Values. Encouraged to use manage usersecrets of the JobsOnMarket project.
Connection string will require a working sql server instace with the right credentials.

Running Please make sure that ports 1433,5142,7180 are free to be used. The setup requires those ports.