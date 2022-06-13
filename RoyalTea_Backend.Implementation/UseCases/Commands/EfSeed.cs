using Microsoft.EntityFrameworkCore;
using RoyalTea_Backend.Application.UseCases;
using RoyalTea_Backend.Application.UseCases.Commands;
using RoyalTea_Backend.DataAccess;
using RoyalTea_Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Commands
{
    public class EfSeed : EfUseCase, ISeed
    {
        public EfSeed(AppDbContext dbContext)
            : base(dbContext)
        {

        }

        public int Id => 1;

        public string Name => "DB Seeder";

        public string Description => "Initial Database Seeder for EntityFramework";

        public void Execute()
        {
            if (this.DbContext.Images.Any())
            {
                return;
            }

            var images = new List<Image> { };
            for (int i = 1; i <= 10; i++)
                images.Add(new Image { Path = $"image{i}.jpg" });

            var categories = new List<Category>
            {
                new Category { Name = "Tea Sachets" },
                new Category { Name = "Loose Leaf Tea" },
                new Category { Name = "Iced Tea Pouches" },
                new Category { Name = "Tea Accessories" }
            };

            var specifications = new List<Specification>
            {
                new Specification { Name = "Caffeine Content" },
                new Specification { Name = "Tasting Notes" },
                new Specification { Name = "Tea Accessory Type" }
            };

            var specificationValues = new List<SpecificationValue>
            {
                new SpecificationValue { Specification = specifications.ElementAt(0), Value = "Caffeine Free" },
                new SpecificationValue { Specification = specifications.ElementAt(0), Value = "Low Caffeine" },
                new SpecificationValue { Specification = specifications.ElementAt(0), Value = "Medium/High Caffeine" },

                new SpecificationValue { Specification = specifications.ElementAt(1), Value = "Refreshing" },
                new SpecificationValue { Specification = specifications.ElementAt(1), Value = "Sharp" },
                new SpecificationValue { Specification = specifications.ElementAt(1), Value = "Sweet" },
                new SpecificationValue { Specification = specifications.ElementAt(1), Value = "Floral" },
                new SpecificationValue { Specification = specifications.ElementAt(1), Value = "Smooth" },
                new SpecificationValue { Specification = specifications.ElementAt(1), Value = "Bright" },

                new SpecificationValue { Specification = specifications.ElementAt(2), Value = "Tea Mugs" },
                new SpecificationValue { Specification = specifications.ElementAt(2), Value = "Teapots" },
                new SpecificationValue { Specification = specifications.ElementAt(2), Value = "Thermos" }
            };

            var categorySpecifications = new List<CategorySpecification>
            {
                new CategorySpecification { Category = categories.ElementAt(0), Specification = specifications.ElementAt(0) },
                new CategorySpecification { Category = categories.ElementAt(0), Specification = specifications.ElementAt(1) },

                new CategorySpecification { Category = categories.ElementAt(1), Specification = specifications.ElementAt(0) },
                new CategorySpecification { Category = categories.ElementAt(1), Specification = specifications.ElementAt(1) },

                new CategorySpecification { Category = categories.ElementAt(2), Specification = specifications.ElementAt(0) },
                new CategorySpecification { Category = categories.ElementAt(2), Specification = specifications.ElementAt(1) },

                new CategorySpecification { Category = categories.ElementAt(3), Specification = specifications.ElementAt(2) }
            };

            var products = new List<Product>
            {
                new Product { Title = "Rose Black Tea Sachets", Category = categories.ElementAt(0),
                    Description = "A delectable medley with an enticing floral aroma. Rose Black combines loose leaf black tea with smooth notes of rose and a bright finish in an eco-friendly, biodegradable teabag. Rose Black is delicious hot or cold brewed and poured over ice. The perfect sip for any time of day.",
                    Image = images.ElementAt(0)
                },
                new Product { Title = "Matcha+ Teabag Sachets", Category = categories.ElementAt(0),
                    Description = "Our Matcha+ tea combines various green tea varietals to brew an ultra-vibrant cup. Organic ceremonial grade matcha is delicately hand blended with organic high-grade Sencha green tea leaves to create this energizing blend.",
                    Image = images.ElementAt(1)
                },
                new Product { Title = "Pacific Coast Mint Teabag Sachets", Category = categories.ElementAt(0),
                    Description = "Brew our smooth and refreshing Pacific Cost Mint blend on the go with our eco-friendly, biodegradable teabags. Sip these sharp and minty spearmint and peppermint sachets at any time of day for a subtle lift.",
                    Image = images.ElementAt(2)
                },


                new Product { Title = "Sencha Cherry Blossom", Category = categories.ElementAt(1),
                    Description = "Inspired by the cherry blossom season in spring this batch of Yabukita varietal of sencha green tea is sourced and blended at origin in Japan with spring harvest cherry blossoms.",
                    Image = images.ElementAt(3)
                },
                new Product { Title = "Passionfruit Jasmine Tea", Category = categories.ElementAt(1),
                    Description = "This award winning blend of select black and green teas, jasmine blossoms, and passionfruit essence has a long lasting, sweet flavor and astounding aroma for a perfect well-rounded cup.",
                    Image = images.ElementAt(4)
                },


                new Product { Title = "Meyer Lemon Iced Tea", Category = categories.ElementAt(2),
                    Description = "Inspired by the cherry blossom season in spring this batch of Yabukita varietal of sencha green tea is sourced and blended at origin in Japan with spring harvest cherry blossoms.",
                    Image = images.ElementAt(5)
                },
                new Product { Title = "Green Pomegranate Iced Tea", Category = categories.ElementAt(2),
                    Description = "We've taken the guesswork out of measuring out the perfect pitcher of iced tea with convenient biodegradable iced tea pouches.",
                    Image = images.ElementAt(6)
                },



                new Product { Title = "Ceramic Mug", Category = categories.ElementAt(3),
                    Description = "Made by W/R/F Lab in Southern California, this handmade Ceramic Mug holds about 12oz of your tea of choice.",
                    Image = images.ElementAt(7)
                },
                new Product { Title = "Kinto Porcelain Leaves-to-Tea Teapot", Category = categories.ElementAt(3),
                    Description = "Stunning porcelain teapot from Kinto. Gloss finish and rich texture with subtle variations in the glaze such as around the cup rim. Steel handle.",
                    Image = images.ElementAt(8)
                }

            };

            var productSpecificationValues = new List<ProductSpecificationValue>
            {
                new ProductSpecificationValue { Product = products.ElementAt(0), SpecificationValue = specificationValues.ElementAt(2) },
                new ProductSpecificationValue { Product = products.ElementAt(0), SpecificationValue = specificationValues.ElementAt(3) },
                new ProductSpecificationValue { Product = products.ElementAt(0), SpecificationValue = specificationValues.ElementAt(6) },

                new ProductSpecificationValue { Product = products.ElementAt(1), SpecificationValue = specificationValues.ElementAt(2) },
                new ProductSpecificationValue { Product = products.ElementAt(1), SpecificationValue = specificationValues.ElementAt(4) },
                new ProductSpecificationValue { Product = products.ElementAt(1), SpecificationValue = specificationValues.ElementAt(6) },

                new ProductSpecificationValue { Product = products.ElementAt(2), SpecificationValue = specificationValues.ElementAt(0) },
                new ProductSpecificationValue { Product = products.ElementAt(2), SpecificationValue = specificationValues.ElementAt(5) },
                new ProductSpecificationValue { Product = products.ElementAt(2), SpecificationValue = specificationValues.ElementAt(8) },

                new ProductSpecificationValue { Product = products.ElementAt(3), SpecificationValue = specificationValues.ElementAt(2) },
                new ProductSpecificationValue { Product = products.ElementAt(3), SpecificationValue = specificationValues.ElementAt(4) },
                new ProductSpecificationValue { Product = products.ElementAt(3), SpecificationValue = specificationValues.ElementAt(7) },

                new ProductSpecificationValue { Product = products.ElementAt(4), SpecificationValue = specificationValues.ElementAt(0) },
                new ProductSpecificationValue { Product = products.ElementAt(4), SpecificationValue = specificationValues.ElementAt(5) },
                new ProductSpecificationValue { Product = products.ElementAt(4), SpecificationValue = specificationValues.ElementAt(7) },

                new ProductSpecificationValue { Product = products.ElementAt(5), SpecificationValue = specificationValues.ElementAt(1) },
                new ProductSpecificationValue { Product = products.ElementAt(5), SpecificationValue = specificationValues.ElementAt(5) },
                new ProductSpecificationValue { Product = products.ElementAt(5), SpecificationValue = specificationValues.ElementAt(6) },

                new ProductSpecificationValue { Product = products.ElementAt(6), SpecificationValue = specificationValues.ElementAt(2) },
                new ProductSpecificationValue { Product = products.ElementAt(6), SpecificationValue = specificationValues.ElementAt(3) },
                new ProductSpecificationValue { Product = products.ElementAt(6), SpecificationValue = specificationValues.ElementAt(7) },

                new ProductSpecificationValue { Product = products.ElementAt(7), SpecificationValue = specificationValues.ElementAt(9) },

                new ProductSpecificationValue { Product = products.ElementAt(8), SpecificationValue = specificationValues.ElementAt(10) }

            };

            var currencies = new List<Currency>
            {
                new Currency {IsoCode = "USD"},
                new Currency {IsoCode = "RSD"},
                new Currency {IsoCode = "EUR"}
            };

            var prices = new List<Price>
            {
                new Price { Product = products.ElementAt(0), Currency = currencies.ElementAt(0), Value = 50 },
                new Price { Product = products.ElementAt(0), Currency = currencies.ElementAt(1), Value = 5480 },
                new Price { Product = products.ElementAt(0), Currency = currencies.ElementAt(2), Value = 47 },

                new Price { Product = products.ElementAt(1), Currency = currencies.ElementAt(0), Value = 42 },
                new Price { Product = products.ElementAt(1), Currency = currencies.ElementAt(1), Value = 4600 },
                new Price { Product = products.ElementAt(1), Currency = currencies.ElementAt(2), Value = 39 },

                new Price { Product = products.ElementAt(2), Currency = currencies.ElementAt(0), Value = 50 },
                new Price { Product = products.ElementAt(2), Currency = currencies.ElementAt(1), Value = 5480 },
                new Price { Product = products.ElementAt(2), Currency = currencies.ElementAt(2), Value = 47 },

                new Price { Product = products.ElementAt(3), Currency = currencies.ElementAt(0), Value = 55 },
                new Price { Product = products.ElementAt(3), Currency = currencies.ElementAt(1), Value = 6030 },
                new Price { Product = products.ElementAt(3), Currency = currencies.ElementAt(2), Value = 51 },

                new Price { Product = products.ElementAt(4), Currency = currencies.ElementAt(0), Value = 55 },
                new Price { Product = products.ElementAt(4), Currency = currencies.ElementAt(1), Value = 6030 },
                new Price { Product = products.ElementAt(4), Currency = currencies.ElementAt(2), Value = 51 },

                new Price { Product = products.ElementAt(5), Currency = currencies.ElementAt(0), Value = 42 },
                new Price { Product = products.ElementAt(5), Currency = currencies.ElementAt(1), Value = 4600 },
                new Price { Product = products.ElementAt(5), Currency = currencies.ElementAt(2), Value = 39 },

                new Price { Product = products.ElementAt(6), Currency = currencies.ElementAt(0), Value = 55 },
                new Price { Product = products.ElementAt(6), Currency = currencies.ElementAt(1), Value = 6030 },
                new Price { Product = products.ElementAt(6), Currency = currencies.ElementAt(2), Value = 51 },

                new Price { Product = products.ElementAt(7), Currency = currencies.ElementAt(0), Value = 42 },
                new Price { Product = products.ElementAt(7), Currency = currencies.ElementAt(1), Value = 4600 },
                new Price { Product = products.ElementAt(7), Currency = currencies.ElementAt(2), Value = 39 },

                new Price { Product = products.ElementAt(8), Currency = currencies.ElementAt(0), Value = 50 },
                new Price { Product = products.ElementAt(8), Currency = currencies.ElementAt(1), Value = 5480 },
                new Price { Product = products.ElementAt(8), Currency = currencies.ElementAt(2), Value = 47 }

            };

            var users = new List<User>
            {
                new User { Username = "admin", Email = "admin@website.net", FullName = "Website Admin", Password = "$2a$12$bk8rq5LIreztQGDk22GqVOFt.kBasnFHKmOJYH3iCNZQHlkZBB00W" },
                new User { Username = "user", Email = "user@yahoo.com", FullName = "Website User", Password = "$2a$12$bk8rq5LIreztQGDk22GqVOFt.kBasnFHKmOJYH3iCNZQHlkZBB00W" },
                new User { Username = "pera", Email = "pera@proton.me", FullName = "Pera Peric", Password = "$2a$12$bk8rq5LIreztQGDk22GqVOFt.kBasnFHKmOJYH3iCNZQHlkZBB00W" }
            };

            var useCases = new List<UseCase>
            {

            };

            for (int i = 1; i < 33; i++)
                useCases.Add(new UseCase { User = users.ElementAt(0), UseCaseId = i });

            for (int i = 1; i < 14; i++)
            {
                useCases.Add(new UseCase { User = users.ElementAt(1), UseCaseId = i });
                useCases.Add(new UseCase { User = users.ElementAt(2), UseCaseId = i });
            }
            foreach (var i in new int[] { 15,18,22,25,26 })
            {
                useCases.Add(new UseCase { User = users.ElementAt(1), UseCaseId = i });
                useCases.Add(new UseCase { User = users.ElementAt(2), UseCaseId = i });
            }



            var countries = new List<Country>
            {
                new Country { Name = "Serbia" },
                new Country { Name = "Germany" },
                new Country { Name = "Thailand" },
                new Country { Name = "United States" },
                new Country { Name = "Russia" },
                new Country { Name = "Romania" }
            };

            var addresses = new List<Address>
            {
                new Address { User = users.ElementAt(1), Country = countries.ElementAt(0), DeliveryLocation = "Jurija Gagarina 215, Belgrade 11070" },
                new Address { User = users.ElementAt(1), Country = countries.ElementAt(0), DeliveryLocation = "Vitanovacka 12, Belgrade 11010" },

                new Address { User = users.ElementAt(2), Country = countries.ElementAt(3), DeliveryLocation = "1684 Tyler Avenue, Miami, FL 33169" }
            };

            var cartItems = new List<CartItem>
            {
                new CartItem { User = users.ElementAt(1), Product = products.ElementAt(1), Quantity = 1}
            };

            var orderStatuses = new List<OrderStatus>
            {
                new OrderStatus { Name = "Pending", IsCancellable = true },
                new OrderStatus { Name = "Preparing To Send", IsCancellable = true },
                new OrderStatus { Name = "Package Sent", IsCancellable = false },
                new OrderStatus { Name = "Package Delivered", IsCancellable = false },
                new OrderStatus { Name = "Cancelled", IsCancellable = false }

            };


            this.DbContext.Images.AddRange(images);
            this.DbContext.Specifications.AddRange(specifications);
            this.DbContext.SpecificationValues.AddRange(specificationValues);
            this.DbContext.CategorySpecifications.AddRange(categorySpecifications);
            this.DbContext.Products.AddRange(products);
            this.DbContext.ProductSpecificationValues.AddRange(productSpecificationValues);
            this.DbContext.Currencies.AddRange(currencies);
            this.DbContext.Prices.AddRange(prices);
            this.DbContext.Users.AddRange(users);
            this.DbContext.UseCases.AddRange(useCases);
            this.DbContext.Countries.AddRange(countries);
            this.DbContext.Addresses.AddRange(addresses);
            this.DbContext.CartItems.AddRange(cartItems);
            this.DbContext.OrderStatuses.AddRange(orderStatuses);

            this.DbContext.SaveChanges();
        }
    }
}
