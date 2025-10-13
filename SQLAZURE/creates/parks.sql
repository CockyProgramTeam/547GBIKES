CREATE TABLE Park (
    ParkID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100),
    Address NVARCHAR(255),
    Phone NVARCHAR(20),
    Region NVARCHAR(50),
    TrailLengthMiles FLOAT,
    Difficulty NVARCHAR(50),
    Latitude FLOAT,
    Longitude FLOAT,
    Description NVARCHAR(1000)
);
