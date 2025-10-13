CREATE TABLE SalesSession (
    SalesSessionID INTEGER PRIMARY KEY AUTOINCREMENT,
    UID TEXT NOT NULL,
    SessionStart TEXT NOT NULL,  -- Store DATETIME as ISO 8601 string (e.g., 'YYYY-MM-DD HH:MM:SS')
    SessionEnd TEXT,
    CartID1 INTEGER,
    CartID2 INTEGER,
    CartID3 INTEGER,
    CartID4 INTEGER,
    CartID5 INTEGER
    -- FOREIGN KEY (UID) REFERENCES Customers(UID),
    -- FOREIGN KEY (CartID1) REFERENCES Cart(CartID),
    -- FOREIGN KEY (CartID2) REFERENCES Cart(CartID),
    -- FOREIGN KEY (CartID3) REFERENCES Cart(CartID),
    -- FOREIGN KEY (CartID4) REFERENCES Cart(CartID),
    -- FOREIGN KEY (CartID5) REFERENCES Cart(CartID)
);
CREATE TABLE SalesCatalogue (
    SalesCatalogueID INTEGER PRIMARY KEY AUTOINCREMENT,
    ParkID INTEGER NOT NULL,
    ServiceType TEXT,
    ServiceName TEXT,
    Description TEXT,
    Price REAL NOT NULL,
    StartDate TEXT,       -- Store DATE as ISO 8601 string (e.g., 'YYYY-MM-DD')
    EndDate TEXT,         -- Same format as StartDate
    IsActive INTEGER DEFAULT 1
    -- FOREIGN KEY (ParkID) REFERENCES MountainBikeParks(ParkID)
);
CREATE TABLE Payment (
    PaymentID INTEGER PRIMARY KEY AUTOINCREMENT,
    BookingID INTEGER NOT NULL,
    PaymentMethod TEXT,
    CardType TEXT,
    CardLast4 TEXT CHECK (LENGTH(CardLast4) = 4),
    CardExpDate TEXT, -- Store DATE as ISO 8601 string (e.g., 'YYYY-MM-DD')
    AmountPaid REAL,
    PaymentDate TEXT, -- Store DATETIME as ISO 8601 string (e.g., 'YYYY-MM-DD HH:MM:SS')
    TransactionID TEXT
);
CREATE TABLE Park (
    ParkID INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT,
    Address TEXT,
    Phone TEXT,
    Region TEXT,
    TrailLengthMiles REAL,
    Difficulty TEXT,
    Latitude REAL,
    Longitude REAL,
    Description TEXT
);
CREATE TABLE Customer (
    CustomerID INTEGER PRIMARY KEY AUTOINCREMENT,
    UID TEXT NOT NULL,
    FullName TEXT,
    Email TEXT,
    Phone TEXT,
    BillingAddress TEXT,
    Login TEXT NOT NULL,
    PasswordHash TEXT NOT NULL,
    UserStatus TEXT DEFAULT 'active'
);
CREATE TABLE Card (
    CardID INTEGER PRIMARY KEY AUTOINCREMENT,
    UID TEXT NOT NULL,
    CardType TEXT,
    CardVendor TEXT,
    CardLast4 TEXT CHECK (LENGTH(CardLast4) = 4),
    CardExpDate TEXT, -- Store DATE as ISO 8601 string (e.g., 'YYYY-MM-DD')
    BillingZip TEXT,
    IsActive INTEGER DEFAULT 1,
    UNIQUE (CardLast4, CardExpDate)
    -- FOREIGN KEY (UID) REFERENCES Customers(UID)
);
CREATE TABLE Cart (
    CartID INTEGER PRIMARY KEY AUTOINCREMENT,
    UID TEXT NOT NULL,
    ParkID INTEGER NOT NULL,
    ItemType TEXT,
    ItemDescription TEXT,
    Quantity INTEGER NOT NULL,
    UnitPrice REAL NOT NULL,
    TotalPrice REAL GENERATED ALWAYS AS (Quantity * UnitPrice) STORED,
    DateAdded TEXT DEFAULT (datetime('now')),
    IsCheckedOut INTEGER DEFAULT 0
    -- FOREIGN KEY (UID) REFERENCES Customers(UID),
    -- FOREIGN KEY (ParkID) REFERENCES MountainBikeParks(ParkID)
);
CREATE TABLE Booking (
    BookingID INTEGER PRIMARY KEY AUTOINCREMENT,
    UID TEXT NOT NULL,
    BillingTelephoneNumber TEXT,
    CreditCardType TEXT,
    CreditCardLast4 TEXT CHECK (LENGTH(CreditCardLast4) = 4),
    CreditCardExpDate TEXT, -- Use TEXT for DATE in SQLite
    QuantityAdults INTEGER,
    QuantityChildren INTEGER,
    CustomerBillingName TEXT,
    TotalAmount REAL, -- SQLite uses REAL for decimal values
    TransactionID TEXT,
    ParkID INTEGER,
    ParkName TEXT
);
CREATE TABLE apilog (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    apiname TEXT,
    apinumber TEXT,
    eptype TEXT,
    hashid INTEGER,
    parameterlist TEXT,
    apiresult TEXT
);
CREATE TABLE sessionlogs (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    username TEXT,
    hashid INTEGER,
    sessionstart DATETIME,
    sessionend DATETIME,
    moduleid TEXT,
    description TEXT
);
