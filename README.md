# NFLDepthCharts
Code challenge from FanDuel
Key Usecases
- addPlayerToDepthChart (position, player, position_depth)
- removePlayerFromDepthChart(position, player)
- getBackups (position, player)
- getFullDepthChart()

## IDE & Tools

- VisualStudioCode, SQLite db
- C#, .Net8, Swagger
- EntityFramework
- Xunit, Moq

## Project Reference
Using a hybrid Clean Architecture
- Web.proj references to Application.proj
- Infrastructure.proj references to Application.proj
- Application.proj reference to Infrastructure.proj

## How it works
- Pull down the code from Github
- Run command to install packages 
    ```bash
    dotnet restore
    ```
- Make sure the SQLite has been installed correctly
    1. Run the DB to see if there is any data in the tables.

- If the DB is empty, run the following SQL script
    1. Run the CreateTable.sql file in the DB folder to create 3 tables: Players, Positions and DepthChart
    2. Run the SeedData.sql file in the DB folder to create some raw data in the two tale: Players and Positions 
- Run the Web application
    1. Direct to the Web project root and run the command:
    ```bash
    dotnet watch run
    ```
    2. Listen to the port http://localhost:5182/ or the swagger UI http://localhost:5182/swagger/index.html
- Test
    ```bash
    dotnet test
    ```