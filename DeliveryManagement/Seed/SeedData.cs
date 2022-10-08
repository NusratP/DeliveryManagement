using DeliveryManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeliveryManagement.Seed
{
    public class SeedData
    {
        public SeedData()
        {

        }

        public IList<Product> GetProducts()
        {
            var productsList = new List<Product>();
            var product = new Product
            {
                productId = 1,
                name = "Milk",
                deliveryDays = DayOfWeeks()
                            .Where(x=>x!= DayOfWeek.Monday)
                            .ToList(),
                productType = ProductType.normal,
                daysInAdvance= 4
            };
            productsList.Add(product);

            product = new Product
            {
                productId = 2,
                name = "Sugar",
                deliveryDays = DayOfWeeks()
                            .Where(x => x != DayOfWeek.Tuesday)
                            .ToList(),
                productType = ProductType.temporary,
                daysInAdvance = 3
            };
            productsList.Add(product);
            
            product = new Product
            {
                productId = 3,
                name = "Potato",
                deliveryDays = DayOfWeeks()
                            .ToList(),
                productType = ProductType.external,
                daysInAdvance = 5
            };
            productsList.Add(product);
            
            product = new Product
            {
                productId = 4,
                name = "Onion",
                deliveryDays = DayOfWeeks()
                            .Where(x => x != DayOfWeek.Tuesday)
                            .ToList(),
                productType = ProductType.normal,
                daysInAdvance = 2
            };
            productsList.Add(product);
            
            product = new Product
            {
                productId = 5,
                name = "Coffee",
                deliveryDays = DayOfWeeks()
                            .Where(x => x != DayOfWeek.Friday)
                            .ToList(),
                productType = ProductType.temporary,
                daysInAdvance = 3
            };
            productsList.Add(product);
            
            product = new Product
            {
                productId = 6,
                name = "Bread",
                deliveryDays = DayOfWeeks()
                            .Where(x => x != DayOfWeek.Friday)
                            .ToList(),
                productType = ProductType.external,
                daysInAdvance = 5
            };
            productsList.Add(product);
            
            product = new Product
            {
                productId = 7,
                name = "Chicken",
                deliveryDays = DayOfWeeks()
                            .Where(x => x != DayOfWeek.Friday)
                            .ToList(),
                productType = ProductType.normal,
                daysInAdvance = 1
            };
            productsList.Add(product);
            
            product = new Product
            {
                productId = 8,
                name = "Fish",
                deliveryDays = DayOfWeeks()
                            .ToList(),
                productType = ProductType.temporary,
                daysInAdvance = 5
            };
            productsList.Add(product);

            product = new Product
            {
                productId = 9,
                name = "Pasta",
                deliveryDays = DayOfWeeks()
                            .Where(x => x != DayOfWeek.Friday)
                            .ToList(),
                productType = ProductType.external,
                daysInAdvance = 3
            };
            productsList.Add(product);
            
            product = new Product
            {
                productId = 10,
                name = "Apple",
                deliveryDays = DayOfWeeks()
                            .Where(x => x != DayOfWeek.Friday)
                            .ToList(),
                productType = ProductType.normal,
                daysInAdvance = 2
            };
            productsList.Add(product);

            return productsList;
        }

        private IList<DayOfWeek> DayOfWeeks()
        {
           return Enum.GetValues(typeof(DayOfWeek))
                           .Cast<DayOfWeek>().ToList();

        }


    }
}
