sqlite> CREATE TABLE adminlogs (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    userid TEXT NOT NULL,
    date DATETIME DEFAULT CURRENT_TIMESTAMP,
    description TEXT,
    acknowledged TEXT,
    techid INTEGER,
    managerescid INTEGER,
    threatlevel TEXT
);
sqlite> CREATE TABLE superuserlogs (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    userid TEXT NOT NULL,
    date DATETIME DEFAULT CURRENT_TIMESTAMP,
    description TEXT,
    acknowledged TEXT,
    techid INTEGER,
    managerescid INTEGER,
    threatlevel TEXT
);
sqlite> CREATE TABLE noctechs (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    userid TEXT NOT NULL,
    employeeid TEXT,
    email TEXT,
    phone TEXT,
    sms INTEGER DEFAULT 1 CHECK (sms IN (0, 1)),
    techaddress1 TEXT,
    techaddress2 TEXT,
    techcity TEXT,
    techstate TEXT,
    techzip TEXT