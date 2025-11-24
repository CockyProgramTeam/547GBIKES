using System;
using dirtbike.api.Models;
using dirtbike.api.Data; // adjust namespace to match your project
using Microsoft.EntityFrameworkCore;
namespace Enterpriseservices;

public class ApiLogHelper
{
    public static void InsertDummyRecord()
    {
        using (var context = new DirtbikeContext())
        {
            var dummyLog = new Apilog
            {
                Apiname = "DummyAPI",
                Apinumber = "API-TEST-001",
                Eptype = "GET",
                Hashid = 9999,
                Parameterlist = "param1=value1;param2=value2",
                Apiresult = "Success",
                Description = "This is a dummy test record inserted at " + DateTime.Now
            };

            context.Apilogs.Add(dummyLog);
            context.SaveChanges();
        }
    }
}
