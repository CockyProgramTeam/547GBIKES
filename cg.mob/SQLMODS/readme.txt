Supporting the CG UI Requires modifications to the User Tables.

sqlite> alter table users add column displayname text;
sqlite> alter table users add column dateOfBirth DateOnly;

Supporting the CG UI Requires modifications to the ParkReviews Tables.
sqlite> alter table ParkReviews add column active Boolean;

Supporting the CG UI Requires modifications to the Park Table
adultPrice;
childPrice;
id (ParkID AS STRING-> CG USES GUI ID FORM).

All these fields are MAPPED ON THE PARK SERVICE WHICH REQUIRES METADATA STRUCTURES FROM THE API...
