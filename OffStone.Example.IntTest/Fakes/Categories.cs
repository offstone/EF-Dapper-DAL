using OffStone.Example.Dal.Entities;
using System.Collections.Generic;

namespace OffStone.Example.IntTest.Fakes
{

    public static class Categories
    {
        public static IList<Category> categories = new List<Category>{
            new Category{
                CategoryId = 1,
                CategoryName = "Beverages",
                Description = "Soft drinks, coffees, teas, beers, and ales"
            },
            new Category{
                CategoryId = 2,
                CategoryName = "Condiments",
                Description = "Sweet and savory sauces, relishes, spreads, and seasonings"
            },
            new Category{
                CategoryId = 3,
                CategoryName = "Confections",
                Description = "Desserts, candies, and sweet breads"
            },
            new Category{
                CategoryId = 4,
                CategoryName = "Dairy Products",
                Description = "Cheeses"
            },
            new Category{
                CategoryId = 5,
                CategoryName = "Grains/Cereals",
                Description = "Breads, crackers, pasta, and cereal"
            },
            new Category{
                CategoryId = 6,
                CategoryName = "Meat/Poultry",
                Description = "Prepared meats"
            },
            new Category{
                CategoryId = 7,
                CategoryName = "Produce",
                Description = "Dried fruit and bean curd"
            },
            new Category{
                CategoryId = 8,
                CategoryName = "Seafood",
                Description = "Seaweed and fish"
            }
        };

        public static Category NewCategory_01 = new Category
        {
            CategoryName = "TestCategory",
            Description = "Test Category 01"
        };
    }
}
