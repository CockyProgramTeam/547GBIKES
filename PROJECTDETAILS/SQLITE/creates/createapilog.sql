CREATE TABLE apilog (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    apiname TEXT,
    apinumber TEXT,
    eptype TEXT,
    hashid INTEGER,
    parameterlist TEXT,
    apiresult TEXT
);