﻿using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreConfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContexName}", typeof(OrderContext).Name);
            }

        }

        private static IEnumerable<Order> GetPreConfiguredOrders()
        {
            return new List<Order>()
            {
                new Order() {UserName = "Kola", FirstName = "Kolawole", LastName = "Adesina", EmailAddress = "meloga41@gmail.com", AddressLine = "Agodo", Country = "Nigeria", TotalPrice = 1550 },
                new Order() {UserName = "Bayo", FirstName = "Adesina", LastName = "Adesina", EmailAddress = "erivicstech@gmail.com", AddressLine = "Ejigbo", Country = "Nigeria", TotalPrice = 1050 }

            };
        }
        
    }
}
