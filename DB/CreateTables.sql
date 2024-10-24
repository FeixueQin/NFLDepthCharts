-- SQLite

Players table
CREATE TABLE Players (
    playerNumber INT PRIMARY KEY,  -- Use number as the primary key
    name VARCHAR(100) NOT NULL,
    positionId INTEGER,
    FOREIGN KEY (positionId) REFERENCES Positions(positionId)
);

-- Positions table
CREATE TABLE Positions (
    positionId INTEGER PRIMARY KEY AUTOINCREMENT,
    name VARCHAR(50) NOT NULL,
    abbreviation VARCHAR(10) NOT NULL
);

-- Depth Chart table
CREATE TABLE DepthChart (
    depthChartId INTEGER PRIMARY KEY AUTOINCREMENT,
    playerNumber INTEGER,
    positionId INTEGER,
    positionDepth INTEGER NOT NULL,
    FOREIGN KEY (playerNumber) REFERENCES Players(playerNumber),
    FOREIGN KEY (positionId) REFERENCES Positions(positionId)
);