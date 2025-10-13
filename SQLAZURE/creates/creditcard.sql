CREATE TABLE Card (
    CardID INT PRIMARY KEY IDENTITY(1,1),
    UID UNIQUEIDENTIFIER NOT NULL,
    CardType NVARCHAR(50),           -- e.g., 'Visa', 'MasterCard', 'Amex'
    CardVendor NVARCHAR(100),        -- e.g., 'Bank of America', 'Chase', 'Capital One'
    CardLast4 CHAR(4),
    CardExpDate DATE,
    BillingZip NVARCHAR(10),
    IsActive BIT DEFAULT 1,
    UNIQUE (CardLast4, CardExpDate)
    -- FOREIGN KEY (UID) REFERENCES Customers(UID)
);
