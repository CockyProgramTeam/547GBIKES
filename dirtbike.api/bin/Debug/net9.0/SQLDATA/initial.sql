/*INSERT OR IGNORE INTO Users
(Id, Uidstring, Displayname, Fullname, Username, Email, Activepictureurl)
VALUES
(1, '17fa0861-d120-4cd8-a24b-f9a579ecbf17', 'Moto R. Bike', 'James Sheldon', 'motorbike', 'motorbike@example.com', ''),
(2, 'a1', 'Trail Blazer', 'Sarah Johnson', 'trailblazer', 'a1@example.com', ''),
(3, 'b2', 'Speed Seeker', 'Carlos Martinez', 'speedseeker', 'b2@example.com', ''),
(4, 'c3', 'Moto Queen', 'Emily Davis', 'motoqueen', 'c3@example.com', ''),
(5, 'd4', 'Ride Master', 'David Lee', 'ridemaster', 'd4@example.com', ''),
(6, 'e5', 'Adventure Addict', 'Olivia Brown', 'adventureaddict', 'e5@example.com', ''),
(7, 'f6', 'Hill Thrill', 'Michael Thompson', 'hillthrill', 'f6@example.com', ''),
(8, 'g7', 'Slope Rider', 'Anna Wilson', 'sloperider', 'g7@example.com', ''),
(9, 'h8', 'Thrill Rider', 'Jason Clark', 'thrillrider', 'h8@example.com', ''),
(10, 'i9', 'Drop Lover', 'Sophia Taylor', 'droplover', 'i9@example.com', '');
*/
INSERT OR IGNORE INTO Parks
(ParkId, Id, Name, Address, Region, Description, AdultPrice, ChildPrice,
 Latitude, Longitude, Pic1url)
VALUES
(998, '92ed4740-12d9-4573-a8f1-c883ca216a00', 'Motobike Mayhem', 'Springwood, CO', 'Springwood, CO',
 'Lorem ipsum dolor sit amet, consectetur adipiscing elit...', 25, 15, 0.0, 0.0,
 'https://placehold.co/600x400/334155/FFF?text=Motobike+Mayhem');

INSERT OR IGNORE INTO Parks
(ParkId, Id, Name, Address, Region, Description, AdultPrice, ChildPrice,
 Latitude, Longitude, Pic1url)
VALUES
(999, 'fc099512-96d4-497a-a42f-d7b3967abc03', 'Crossbar Parkway', 'Springwood, CO', 'Springwood, CO',
 'This park boasts extreme hills and fun drops for all thrill-seekers.', 25, 15, 0.0, 0.0,
 'https://placehold.co/600x400/3321a5/FFF?text=Crossbar+Parkway');
/*
INSERT INTO ParkReviews VALUES
(999, 'fc099512-96d4-497a-a42f-d7b3967abc03', 1, 'Phenomenal park!', 0,
 '2025-09-12T12:30:32.594Z', '17fa0861-d120-4cd8-a24b-f9a579ecbf17', 1, 'Moto R. Bike', 'James Sheldon', 'Crossbar Parkway', '999', 'UI-MOCK');

INSERT INTO ParkReview VALUES
(999, 'fc099512-96d4-497a-a42f-d7b3967abc03', 7, 'The drops were exhilarating! Perfect for thrill-seekers.', 5,
 '2025-09-20T13:00:00.000Z', 'f6', 1, 'Hill Thrill', 'Michael Thompson', 'Crossbar Parkway', '999', 'UI-MOCK');

INSERT INTO ParkReview VALUES
(999, 'fc099512-96d4-497a-a42f-d7b3967abc03', 8, 'Loved the hills, though the food options could be better.', 4,
 '2025-09-19T15:30:00.000Z', 'g7', 1, 'Slope Rider', 'Anna Wilson', 'Crossbar Parkway', '999', 'UI-MOCK');

INSERT INTO ParkReview VALUES
(999, 'fc099512-96d4-497a-a42f-d7b3967abc03', 9, 'Crossbar Parkway exceeded my expectations. Smooth rides and breathtaking views.', 5,
 '2025-09-18T12:45:00.000Z', 'h8', 1, 'Thrill Rider', 'Jason Clark', 'Crossbar Parkway', '999', 'UI-MOCK');

INSERT INTO ParkReview VALUES
(999, 'fc099512-96d4-497a-a42f-d7b3967abc03', 10, 'Exciting rides, though a bit too intense for younger kids.', 4,
 '2025-09-17T17:10:00.000Z', 'i9', 1, 'Drop Lover', 'Sophia Taylor', 'Crossbar Parkway', '999', 'UI-MOCK');

INSERT INTO ParkReview
(ParkId, ParkGuid, UserId, Description, Stars, DatePosted, Useridasstring,
 Active, Displayname, Fullname, ParkName, ParkIdAsString, Possource)
VALUES
(998, '92ed4740-12d9-4573-a8f1-c883ca216a00', 1, 'Phenomenal park!', 5, '2025-09-15T12:30:32.594Z',
 '17fa0861-d120-4cd8-a24b-f9a579ecbf17', 1, 'Moto R. Bike', 'James Sheldon', 'Motobike Mayhem', '998', 'UI-MOCK');

INSERT INTO ParkReview VALUES
(998, '92ed4740-12d9-4573-a8f1-c883ca216a00', 2, 'The rides were thrilling and the staff was super friendly. Highly recommend!', 5,
 '2025-09-20T10:15:00.000Z', 'a1', 1, 'Trail Blazer', 'Sarah Johnson', 'Motobike Mayhem', '998', 'UI-MOCK');

INSERT INTO ParkReview VALUES
(998, '92ed4740-12d9-4573-a8f1-c883ca216a00', 3, 'Great park overall, though I wish there were more shaded rest areas.', 4,
 '2025-09-19T14:45:00.000Z', 'b2', 1, 'Speed Seeker', 'Carlos Martinez', 'Motobike Mayhem', '998', 'UI-MOCK');

INSERT INTO ParkReview VALUES
(998, '92ed4740-12d9-4573-a8f1-c883ca216a00', 4, 'Absolutely loved the adrenaline rush. The hills were perfectly designed!', 5,
 '2025-09-18T09:00:00.000Z', 'c3', 1, 'Moto Queen', 'Emily Davis', 'Motobike Mayhem', '998', 'UI-MOCK');

INSERT INTO ParkReview VALUES
(998, '92ed4740-12d9-4573-a8f1-c883ca216a00', 5, 'Fun experience! Some rides were a bit crowded but still worth the wait.', 4,
 '2025-09-17T16:20:00.000Z', 'd4', 1, 'Ride Master', 'David Lee', 'Motobike Mayhem', '998', 'UI-MOCK');

INSERT INTO ParkReview VALUES
(998, '92ed4740-12d9-4573-a8f1-c883ca216a00', 6, 'Best park I’ve visited in years. Smooth rides and great atmosphere!', 5,
 '2025-09-16T11:10:00.000Z', 'e5', 1, 'Adventure Addict', 'Olivia Brown', 'Motobike Mayhem', '998', 'UI-MOCK');
*/