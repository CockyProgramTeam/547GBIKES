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

Supporting the CG UI Requires Modifications to the CartItem table
sqlite> alter table CartItem add column numDays int; //RESERVATIONS HAVE A NUMBER OF DAYS SPECIFICALLY WRITTEN... THIS COULD BE A COMPUTED FIELD.
sqlite> alter table CartItem add column Parkidasstring string; //ParkIDs are GUIDS for Capgemeni.


