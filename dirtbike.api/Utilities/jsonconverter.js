const fs = require('fs');
const path = require('path');

const csvData = `
Park Name,Address1,Address2,City,State,Zip,Latitude,Longitude,Bicycling,Camping,Day Pass Cost,Week Pass Cost
Acadia National Park,20 McFarland Hill Dr,,Bar Harbor,ME,04609,44.338974,-68.273430,Yes,Yes,$35 vehicle,$35 vehicle (7-day)
American Samoa National Park,,,Pago Pago,AS,96799,-14.258889,-170.684444,Limited,No,Free,Free
Paris Mountain State Park,2401 State Park Rd,,Greenville,SC,29609,34.94158,-82.41128,Yes,Yes,$2,$2
`.trim(); // Add more rows as needed

function parseCSV(csv) {
  const lines = csv.split('\n');
  const headers = lines[0].split(',').map(h => h.trim());
  const parks = [];

  for (let i = 1; i < lines.length; i++) {
    const values = lines[i].split(',').map(v => v.trim());
    const park = {};

    headers.forEach((header, index) => {
      park[header] = values[index] || '';
    });

    parks.push({
      name: park["Park Name"],
      address: park["Address1"],
      city: park["City"],
      state: park["State"],
      zip: park["Zip"],
      latitude: parseFloat(park["Latitude"]),
      longitude: parseFloat(park["Longitude"]),
      bicycling: park["Bicycling"],
      camping: park["Camping"],
      dayPassCost: park["Day Pass Cost"],
      weekPassCost: park["Week Pass Cost"],
      isNationalPark: park["Park Name"].includes("National Park"),
      isStatePark: park["Park Name"].includes("State Park")
    });
  }

  return parks;
}

const parksJSON = parseCSV(csvData);

// Write to parks.json
fs.writeFileSync(path.join(__dirname, 'parks.json'), JSON.stringify(parksJSON, null, 2), 'utf8');

console.log('parks.json has been created!');
