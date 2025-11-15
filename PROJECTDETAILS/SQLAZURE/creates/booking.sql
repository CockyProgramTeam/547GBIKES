CREATE TABLE Booking (
    BookingID INT PRIMARY KEY IDENTITY(1,1),
    UID UNIQUEIDENTIFIER NOT NULL,
    BillingTelephoneNumber NVARCHAR(20),
    CreditCardType NVARCHAR(50),
    CreditCardLast4 CHAR(4),
    CreditCardExpDate DATE,
    QuantityAdults INT,
    QuantityChildren INT,
    CustomerBillingName NVARCHAR(100),
    TotalAmount DECIMAL(10,2),
    TransactionID NVARCHAR(100),
    ParkID INT,
    ParkName NVARCHAR(100)
);

