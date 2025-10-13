CREATE TABLE userlogs (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    username TEXT,
    hashid INTEGER,
    hashedpassword TEXT,
    loginstatus TEXT
);