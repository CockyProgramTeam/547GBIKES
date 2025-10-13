CREATE TABLE Cart (
    CartID INT PRIMARY KEY IDENTITY(1,1),
    UID UNIQUEIDENTIFIER NOT NULL,
    ParkID INT NOT NULL,
    ItemType NVARCHAR(50),           -- e.g., 'Day Pass', 'Bike Rental', 'Helmet Rental'
    ItemDescription NVARCHAR(255),   -- e.g., 'Adult Day Pass for Paris Mountain'
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10,2) NOT NULL,
    TotalPrice AS (Quantity * UnitPrice) PERSISTED,
    DateAdded DATETIME DEFAULT GETDATE(),
    IsCheckedOut BIT DEFAULT 0
    -- FOREIGN KEY (UID) REFERENCES Customers(UID)
    -- FOREIGN KEY (ParkID) REFERENCES MountainBikeParks(ParkID)
);
