#1 OKAY SO THE TRIGGERS IN THIS SYSTEM ARE ON USER INSERTS(POSTS)....WE CREATE A USERPROFILE, AND A CARTMASTER....
#2 WE ALSO ARE SETTING USERS IDS TO THE NEW RECORD ID... TO SIMPlIFY THE PROCESS... NOT ALLOWING USERID OVERIDES WHICH IS MORE SECURE...

sqlite> SELECT name, tbl_name, sql
FROM sqlite_master
WHERE type = 'trigger';
create_userprofile_after_user_insert|users|CREATE TRIGGER create_userprofile_after_user_insert
AFTER INSERT ON users
FOR EACH ROW
BEGIN
  INSERT INTO userprofiles (userid, activepictureurl, firstname, lastname, fullname)
  VALUES (
    NEW.id,
    NEW.activepictureurl,
    NEW.firstname,
    NEW.lastname,
    NEW.fullname
  );
END
set_userid_after_insert|users|CREATE TRIGGER set_userid_after_insert
AFTER INSERT ON users
FOR EACH ROW
BEGIN
  UPDATE users
  SET userid = NEW.id
  WHERE id = NEW.id;
END
sqlite> 
CREATE TRIGGER create_cartmaster_after_user_insert
AFTER INSERT ON users
FOR EACH ROW
BEGIN
  INSERT INTO cartmaster (
    userId,
    cartsCount,
    cartsCancelled,
    cartsActive,
    cartsActiveList,
    loyaltyid,
    loyaltyvendor
  )
  VALUES (
    NEW.userid,
    0,
    0,
    0,
    '',
    '',
    ''
  );
END;
CREATE TRIGGER set_useridstring_after_insert
AFTER INSERT ON users
FOR EACH ROW
BEGIN
  UPDATE users
  SET useridstring = CAST(NEW.id AS TEXT)
  WHERE id = NEW.id;
END;
