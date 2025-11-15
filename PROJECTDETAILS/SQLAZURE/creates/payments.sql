CREATE TABLE Payment (
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
    BookingID INT NOT NULL,
    PaymentMethod NVARCHAR(50),
    CardType NVARCHAR(50),
    CardLast4 CHAR(4),
    CardExpDate DATE,
    AmountPaid DECIMAL(10,2),
    PaymentDate DATETIME,
    TransactionID NVARCHAR(100),
    
);


//FOREIGN KEY (BookingID) REFERENCES Bookings(BookingID) - NOT IMPLEMENTED YET
