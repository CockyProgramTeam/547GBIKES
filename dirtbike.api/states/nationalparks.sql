INSERT INTO Parks (
    name, address, phone, region, trailLengthMiles, difficulty, description,
    dayPassPriceUsd, longitude, latitude, trailmapurl, parklogourl,
    maxvisitors, currentvisitors, currentvisitorschildren, currentvisitorsadults,
    maxcampsites, columns, state,
    pic1url, pic2url, pic3url, pic4url, pic5url, pic6url, pic7url, pic8url, pic9url,
    isnationalpark, isstatepark
) VALUES
-- 1. Canyonlands National Park
('Canyonlands National Park', '2282 SW Resource Blvd, Moab, UT 84532', '435-719-2313', 'Southwest',
20, 'Advanced', 'Latitude: 38.3269\nLongitude: -109.8783',
30, -109.8783, 38.3269, NULL, NULL,
500, 0, 0, 0,
100, NULL, 'UT',
'https://www.nps.gov/common/uploads/structured_data/3C7E2E3F-1DD8-B71B-0B9F7C3A9A1E9F1D.jpg', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
1, 0),

-- 2. Hot Springs National Park
('Hot Springs National Park', '101 Reserve St, Hot Springs, AR 71901', '501-620-6715', 'South',
26, 'Intermediate', 'Latitude: 34.5133\nLongitude: -93.0540',
0, -93.0540, 34.5133, NULL, NULL,
300, 0, 0, 0,
50, NULL, 'AR',
'https://www.nps.gov/common/uploads/structured_data/3C7E2E3F-1DD8-B71B-0B9F7C3A9A1E9F1D.jpg', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
1, 0),

-- 3. Mammoth Cave National Park
('Mammoth Cave National Park', '1 Mammoth Cave Pkwy, Mammoth Cave, KY 42259', '270-758-2180', 'Southeast',
10, 'Intermediate', 'Latitude: 37.1860\nLongitude: -86.1015',
20, -86.1015, 37.1860, NULL, NULL,
400, 0, 0, 0,
80, NULL, 'KY',
NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
1, 0),

-- 4. Big Bend National Park
('Big Bend National Park', '1 Panther Junction, Big Bend National Park, TX 79834', '432-477-2251', 'Southwest',
15, 'Advanced', 'Latitude: 29.1275\nLongitude: -103.2425',
30, -103.2425, 29.1275, NULL, NULL,
600, 0, 0, 0,
120, NULL, 'TX',
NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
1, 0),

-- 5. Voyageurs National Park
('Voyageurs National Park', '360 Hwy 11 East, International Falls, MN 56649', '218-283-6600', 'Midwest',
8, 'Intermediate', 'Latitude: 48.4849\nLongitude: -92.8380',
0, -92.8380, 48.4849, NULL, NULL,
250, 0, 0, 0,
40, NULL, 'MN',
NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
1, 0),

-- 6. Everglades National Park
('Everglades National Park', '40001 State Road 9336, Homestead, FL 33034', '305-242-7700', 'Southeast',
12, 'Easy to Intermediate', 'Latitude: 25.2866\nLongitude: -80.8987',
30, -80.8987, 25.2866, NULL, NULL,
700, 0, 0, 0,
150, NULL, 'FL',
NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
1, 0),

-- 7. Great Smoky Mountains National Park
('Great Smoky Mountains National Park', '107 Park Headquarters Rd, Gatlinburg, TN 37738', '865-436-1200', 'Southeast',
18, 'Intermediate to Advanced', 'Latitude: 35.6532\nLongitude: -83.5070',
0, -83.5070, 35.6532, NULL, NULL,
1000, 0, 0, 0,
200, NULL, 'TN',
NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
1, 0),

-- 8. Rocky Mountain National Park
('Rocky Mountain National Park', '1000 US Hwy 36, Estes Park, CO 80517', '970-586-1206', 'Mountain West',
22, 'Advanced', 'Latitude: 40.3428\nLongitude: -105.6836',
30, -105.6836, 40.3428, NULL, NULL,
800, 0, 0, 0,
160, NULL, 'CO',
NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
1, 0),

-- 9. Shenandoah National Park
('Shenandoah National Park', '3655 US Hwy 211 East, Luray, VA 22835', '540-999-3500', 'Mid-Atlantic',
14, 'Intermediate', 'Latitude: 38.2928\nLongitude: -78.6796',
30, -78.6796, 38.2928, NULL, NULL,
500, 0, 0, 0,
100, NULL, 'VA',
NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
1, 0),

-- 10. Crater Lake National Park
('Crater Lake National Park', '1 Rim Drive, Crater Lake, OR 97604', '541-594-3000', 'Pacific Northwest',
10, 'Intermediate', 'Latitude: 42.9446\nLongitude: -122.1090',
30, -122.1090, 42.9446, NULL, NULL,
350, 0, 0, 0,
70, NULL, 'OR',
NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
1, 0);
