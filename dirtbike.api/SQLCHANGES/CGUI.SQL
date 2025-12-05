ALTER TABLE STATEMENT
alter parks add column adultPrice text;
alter parks add column childPrice text;
alter parks add column reviews text;
alter parks add column id text;

UPDATE Parks
SET Id = (
    substr(hex(randomblob(16)),1,8) || '-' ||
    substr(hex(randomblob(16)),9,4) || '-' ||
    substr(hex(randomblob(16)),13,4) || '-' ||
    substr(hex(randomblob(16)),17,4) || '-' ||
    substr(hex(randomblob(16)),21,12)
)
WHERE ParkId > 1;

UPDATE PARKS set childPrice = 8.00 where ParkId >0;
UPDATE PARKS set adultPrice = 23.00 where ParkId >0;



"adultPrice": 23, 
"childPrice": 13, 
"reviews": null, 
"id": "165e1b3f-8105-c95a-32b5-23cac48d9aca"