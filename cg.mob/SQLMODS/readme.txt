Supporting the CG UI Requires modifications to the User Tables.

sqlite> alter table users add column displayname text;
sqlite> alter table users add column dateOfBirth DateOnly;
