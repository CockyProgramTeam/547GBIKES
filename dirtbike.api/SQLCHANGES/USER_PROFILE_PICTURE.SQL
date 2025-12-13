BEGIN TRANSACTION;

INSERT INTO Userprofiles (Id, Userid, Useridstring)
SELECT u.Id, u.Userid, u.Uidstring
FROM Users u
WHERE NOT EXISTS (
    SELECT 1 FROM Userprofiles up WHERE up.Id = u.Id
);

INSERT INTO UserPicture (Id, Userid, Useridstring)
SELECT u.Id, u.Userid, u.Uidstring
FROM Users u
WHERE NOT EXISTS (
    SELECT 1 FROM UserPicture pic WHERE pic.Id = u.Id
);

COMMIT;