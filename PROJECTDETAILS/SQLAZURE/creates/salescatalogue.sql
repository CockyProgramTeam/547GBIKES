CREATE TABLE SalesCatalogue (
    SalesCatalogueID INT PRIMARY KEY IDENTITY(1,1),
    ParkID INT NOT NULL,
    ServiceType NVARCHAR(50),           -- e.g., 'Day Pass', 'Half Day Pass', 'Helmet Rental'
    ServiceName NVARCHAR(100),          -- e.g., 'Adult Day Pass', 'Premium Bike Rental'
    Description NVARCHAR(255),
    Price DECIMAL(10,2) NOT NULL,
    StartDate DATE,                     -- When the service becomes available
    EndDate DATE,                       -- When the service ends or expires
    IsActive BIT DEFAULT 1
    -- FOREIGN KEY (ParkID) REFERENCES MountainBikeParks(ParkID)
);
