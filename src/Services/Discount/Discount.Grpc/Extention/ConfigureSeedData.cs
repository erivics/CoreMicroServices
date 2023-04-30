using Npgsql;


namespace Discount.Grpc.Extention
{
    public static class ConfigureSeedData
    {
        public static void MigrateDatabase<TContext>(this IApplicationBuilder app, int? retry = 0)
        {
            //int? retry = 0;
            int retryForAvailability = retry.Value;

            using (var scope = app.ApplicationServices.CreateScope())
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

                    command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Techno5', 'Techno5 discount', 150)";
                    command.ExecuteNonQuery();


                    command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung', 'Samsung discount', 200)";
                    command.ExecuteNonQuery();

                    logger.LogInformation("Migrated postgresql database");
                }
                catch (NpgsqlException ex)
                {
                    //To perform retry operation
                    logger.LogError(ex, "An error occurred while migrating the postgresql database");
                    if ( retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(app, retryForAvailability);
                    }
                }            
            }
        }
    }
}
