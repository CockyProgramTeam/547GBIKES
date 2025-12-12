using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace T4SQLITEINSTALLER
{
    public static class SQLITEINSTALLER
    {
        // Creates/opens ./SQLDATA/parks.db and ensures tables exist
        public static void createsqlitedb(string dbFileName = "parks.db")
        {
            // Ensure ./SQLDATA directory exists
            Directory.CreateDirectory("SQLDATA");
            var fullPath = Path.GetFullPath(Path.Combine("SQLDATA", dbFileName));
            Console.WriteLine($"SQLite database file path: {fullPath}");

            var connectionString = $"Data Source={fullPath};";
            using var connection = new SqliteConnection(connectionString);

            try
            {
                connection.Open();

                // Enable foreign keys
                using (var pragma = connection.CreateCommand())
                {
                    pragma.CommandText = "PRAGMA foreign_keys = ON;";
                    pragma.ExecuteNonQuery();
                }

                // Parks table
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Parks (
                        ParkID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT,
                        Address TEXT,
                        Phone TEXT,
                        Region TEXT,
                        TrailLengthMiles REAL,
                        Difficulty TEXT,
                        Description TEXT,
                        DayPassPriceUsd REAL,
                        Longitude REAL,
                        Latitude REAL,
                        Trailmapurl TEXT,
                        Parklogourl TEXT,
                        Maxvisitors INTEGER DEFAULT 200,
                        Currentvisitors INTEGER DEFAULT 0,
                        Currentvisitorschildren INTEGER DEFAULT 0,
                        Currentvisitorsadults INTEGER DEFAULT 0,
                        Maxcampsites INTEGER DEFAULT 100,
                        State TEXT,
                        Pic1url TEXT,
                        Pic2url TEXT,
                        Pic3url TEXT,
                        Pic4url TEXT,
                        Pic5url TEXT,
                        Pic6url TEXT,
                        Pic7url TEXT,
                        Pic8url TEXT,
                        Pic9url TEXT,
                        Isnationalpark TEXT,
                        Isstatepark TEXT,
                        Hqbranchid TEXT,
                        Mountainbikes INTEGER,
                        Camping INTEGER,
                        Rafting INTEGER,
                        Canoeing INTEGER,
                        Frisbee INTEGER,
                        Iscanadian INTEGER,
                        Ismexican INTEGER,
                        Motocross INTEGER,
                        Cabins INTEGER,
                        Tents INTEGER,
                        Skiing INTEGER,
                        AverageRating REAL,
                        Id TEXT,
                        Reviews TEXT,
                        Currentcampsites INTEGER DEFAULT 0
                    );";
                    cmd.ExecuteNonQuery();
                }
                Console.WriteLine("Table 'Parks' ensured.");

                // ParkReviews table
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS ParkReviews (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        ParkId INTEGER NOT NULL,
                        UserId INTEGER NOT NULL,
                        Description TEXT NOT NULL,
                        Stars INTEGER,
                        DatePosted TEXT NOT NULL,
                        DateApproved TEXT,
                        DateDenied TEXT,
                        ReasonDescription TEXT,
                        ReviewManagerId INTEGER,
                        Useridasstring TEXT,
                        Active INTEGER,
                        Displayname TEXT,
                        Fullname TEXT,
                        ParkName TEXT,
                        ParkIdAsString TEXT,
                        Possource TEXT,
                        ParkGuid TEXT,
                        FOREIGN KEY (ParkId) REFERENCES Parks(ParkID) ON DELETE CASCADE,
                        FOREIGN KEY (UserId) REFERENCES Users(id) ON DELETE CASCADE
                    );";
                    cmd.ExecuteNonQuery();
                }
                Console.WriteLine("Table 'ParkReviews' ensured.");

                // Cart table
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Cart (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        CartId INTEGER,
                        Uid TEXT,
                        ParkId INTEGER,
                        ItemType TEXT,
                        ItemDescription TEXT,
                        Quantity INTEGER,
                        UnitPrice REAL,
                        TotalPrice REAL,
                        DateAdded TEXT,
                        IsCheckedOut INTEGER,
                        Paymentid TEXT,
                        Bookinginfo TEXT,
                        Totalcartitems INTEGER,
                        Multipleitems INTEGER,
                        Johnstotals REAL,
                        Transactiontotal REAL,
                        Parkname TEXT,
                        ResStart TEXT,
                        ResEnd TEXT,
                        Adults INTEGER,
                        Children INTEGER,
                        Tentsites INTEGER,
                        ParkGuid TEXT,
                        Possource TEXT,
                        FOREIGN KEY (ParkId) REFERENCES Parks(ParkID) ON DELETE CASCADE
                    );";
                    cmd.ExecuteNonQuery();
                }
                Console.WriteLine("Table 'Cart' ensured.");

                // CartItem table
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS CartItem (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Cartid INTEGER,
                        Cartitemdate TEXT,
                        Itemvendor TEXT,
                        Itemdescription TEXT,
                        Itemextendedprice REAL,
                        Itemqty INTEGER,
                        Itemtotals REAL,
                        Salescatid INTEGER,
                        Productid TEXT,
                        Shopid TEXT,
                        Parkid TEXT,
                        Subtotal REAL,
                        CreatedDate TEXT,
                        ResStart TEXT,
                        ResEnd TEXT,
                        Qrcodeurl TEXT,
                        Reservationcode TEXT,
                        Memberid TEXT,
                        Rewardsprovider TEXT,
                        Adults INTEGER,
                        Children INTEGER,
                        Statetaxpercent REAL,
                        Statetaxauth TEXT,
                        Ustaxpercent REAL,
                        Ustaxtotal REAL,
                        Statetaxtotal REAL,
                        Itemsubtotal REAL,
                        Parkname TEXT,
                        Userid INTEGER,
                        NumDays INTEGER,
                        Parkidasstring TEXT,
                        ParkGuid TEXT,
                        Possource TEXT,
                        FOREIGN KEY (Cartid) REFERENCES Cart(Id) ON DELETE CASCADE
                    );";
                    cmd.ExecuteNonQuery();
                }
                Console.WriteLine("Table 'CartItem' ensured.");

                // Users table
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        id INTEGER NOT NULL PRIMARY KEY,
                        firstname TEXT,
                        lastname TEXT,
                        username TEXT,
                        email TEXT,
                        employee INTEGER,
                        employeeid TEXT,
                        microsoftid TEXT,
                        ncrid TEXT,
                        oracleid TEXT,
                        azureid TEXT,
                        plainpassword TEXT,
                        hashedpassword TEXT,
                        passwordtype INTEGER,
                        jid INTEGER,
                        profileurl TEXT,
                        role TEXT,
                        fullname TEXT,
                        companyid INTEGER,
                        resettoken TEXT,
                        resettokenexpiration TEXT,
                        userid INTEGER,
                        BTN TEXT,
                        iscertified INTEGER,
                        groupid1 TEXT,
                        groupid2 TEXT,
                        groupid3 TEXT,
                        groupid4 TEXT,
                        groupid5 TEXT,
                        accountstatus TEXT,
                        accountactiondate TEXT,
                        accountactiondescription TEXT,
                        usertwofactorenabled INTEGER,
                        usertwofactortype TEXT,
                        usertwofactorkeysmsdestination TEXT,
                        twofactorkeyemaildestination TEXT,
                        twofactorprovider TEXT,
                        twofactorprovidertoken TEXT,
                        twofactorproviderauthstring TEXT
                    );";
                    cmd.ExecuteNonQuery();
                }
                Console.WriteLine("Table 'Users' ensured.");

                Console.WriteLine("SQLite install completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQLite install error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}


