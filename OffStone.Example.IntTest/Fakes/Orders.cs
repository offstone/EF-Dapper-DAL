using OffStone.Example.Dal.Entities;
using System;
using System.Collections.Generic;

namespace OffStone.Example.IntTest.Fakes
{

    public static class Orders
    {
        public static Order ExistingOrder_01 =  new Order{
                OrderId = 10315,
                CustomerId = "ISLAT",
                EmployeeId = 4,
                OrderDate = new DateTime(1996, 09, 26),
                RequiredDate = new DateTime(1996, 10, 24),
                ShippedDate = new DateTime(1996, 10, 03),
                ShipVia = 2,
                Freight = 41.76M,
                ShipName = "Island Trading",
                ShipAddress = "Garden House Crowther Way",
                ShipCity = "Cowes",
                ShipRegion = "Isle of Wight",
                ShipPostalCode = "PO31 7PJ",
                ShipCountry = "UK"
        };

        public static Order NewOrder_01 = new Order
        {
            CustomerId = "ISLAT",
            EmployeeId = 4,
            OrderDate = new DateTime(2021, 08, 25),
            RequiredDate = new DateTime(2021, 09, 23),
            ShippedDate = new DateTime(2021, 09, 02),
            ShipVia = 2,
            Freight = 41.76M,
            ShipName = "Test Ship",
            ShipAddress = "Testing Rd",
            ShipCity = "Test City",
            ShipRegion = "Testshire",
            ShipPostalCode = "TS21 1TS",
            ShipCountry = "UK"
        };

        public static Order SearchOrder_01 = new Order
        {
            CustomerId = "ISLAT",
            ShipCity = "Cowes",
            OrderDate = new DateTime(1997, 08, 05),
            Customer = new Customer { CompanyName = "Island Trading" }
        };

        public static IDictionary<string, string> SearchStrings_01 
            = new Dictionary<string, string> { 
                { "CustomerId", "ISLAT" },
                { "ShipCity", "Cowes" }
        };
    }
}
