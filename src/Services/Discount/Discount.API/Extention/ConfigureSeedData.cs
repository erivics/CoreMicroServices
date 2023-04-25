using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Runtime.CompilerServices;

namespace Discount.API.Extention
{
    public static class ConfigureSeedData
    {
        public static IApplicationBuilder MigrateDatabase<TContext> (this IApplicationBuilder applicationBuilder, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            using (var scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();  
                var logger = services.GetRequiredService<ILogger<TContext>>();
                try
                {
                    logger.LogInformation("...Migrating postgresql database");
                    using var connection = new NpgsqlConnection
                   (configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                   connection.Open();

                    using var command = new NpgsqlCommand
                    {
                        Connection = connection,
                    };

                    command.CommandText = "DROP TABLE IF EXISTS Coupon";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY,
                                                                ProductName VARCHAR(24) NOT NULL,
                                                                Description TEXT,
                                                                Amount INT)";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Coupon(ProductName, Description Amount) VALUES('Iphone x', 'IPhone discount', 150)";
                    command.ExecuteNonQuery();


                    command.CommandText = "INSERT INTO Coupon(ProductName, Description Amount) VALUES('Samsung 10', 'Samsung discount', 200)";
                    command.ExecuteNonQuery();

                    logger.LogInformation("Migrated postgresql database");
                }
                catch (NpgsqlException ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the postgresql database");
                    if ( retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(applicationBuilder, retryForAvailability);
                    }
                }
                return applicationBuilder;
            }
        }
    }
}
