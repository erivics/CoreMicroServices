using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetPreconfigureProducts());
            }
        }

        private static IEnumerable<Product> GetPreconfigureProducts()
        {
            return new List<Product>()
            {
               new Product()
               {
                  Id = "602d2149e773f2a3990b47f5",
                  Name = "Samsung",
                  Category = "Smart Phone",
                  Summary = "This phone is the company's biggest change to its flagship smartphone in years",
                  Description = "White colour, big size",
                  ImageFile = "product-1.png",
                  Price = 1150.10m
               },
               new Product()
               {                 
                 Id = "602d2149e773f2a3990b47f4",
                 Name = "Techno5",
                 Category =  "Smart Phone",
                 Summary =  "This phone is the company's biggest change to its flagship smartphone in years",
                 Description = "black colour, big size",
                 ImageFile = "product-2.png",
                 Price =  2345.20m,
               },
               new Product()
               {
                  Id = "602d2149e773f2a3990b47f3",
                  Name = "Infinix 67A",
                  Category =  "Smart Phone",
                  Summary =  "This phone is the company's biggest change to its flagship smartphone in years",
                  Description = "White colour, big size",
                  ImageFile = "product-4.png",
                  Price =  4150.20m,
               },
               new Product()
               {
                  Id = "602d2149e773f2a3990b47f2",
                  Name = "Dell",
                  Category =  "Computer",
                  Summary =  "This computeris the company's biggest change to its flagship computers in years",
                  Description = "Gray colour, big size",
                  ImageFile = "product-3.png",
                  Price =  9050.20m,
                },
               new Product()
               {
                  Id = "602d2149e773f2a3990b47f1",
                  Name = "Samsung",
                  Category =  "Computer",
                  Summary =  "This phone is the company's biggest change to its flagship smartphone in years",
                  Description = "Black colour, big size",
                  ImageFile = "product-5.png",
                  Price =  8100.70m,
               },
           };
        }
    }
}
