using System.Data.SqlClient;
using WebApp.BL.Services;
using WebApp.Core.Interfaces;
using WebApp.Core.Interfaces.Services;
using WebApp.Data;

internal class Program
{
    private const string CONNECTION_STRING_FOR_CREATE_DATABASE = "Server=localhost;Integrated Security=True;";
    private const string DATABASE_NAME = "Test_Andrii_Yamelynets";

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        builder.Services.AddAutoMapper(typeof(Program));

        // Add services to the container.
        builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IEmployeeService, EmployeeService>();

        builder.Services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        CreateDatabase();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void CreateDatabase()
    {

        string createDbQuery = $"IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = '{DATABASE_NAME}')"
                               + "BEGIN"
                               + $" CREATE DATABASE [{DATABASE_NAME}]"
                               + " END";

        string createEmpoyeeTable = $"USE {DATABASE_NAME};"
                                    + $" IF (OBJECT_ID('Employee', 'U') IS NULL)"
                                    + @" BEGIN
                                            CREATE TABLE Employee (
                                                ID INT IDENTITY(1,1) PRIMARY KEY,
                                                Name VARCHAR(50) NOT NULL,
                                                ManagerID INT NULL,
                                                Enable BIT NOT NULL
                                            )

                                            INSERT INTO Employee (Name, ManagerID, Enable) VALUES
                                                ('Andrey', NULL, 1),
                                                ('Alexey', 1, 1),
                                                ('Roman', 2, 1),
                                                ('Dima', 2, 1),
                                                ('Roma', 2, 1),
                                                ('Diana', 4, 1)
                                        END";

        try
        {
            using (var connection = new SqlConnection(CONNECTION_STRING_FOR_CREATE_DATABASE))
            {
                connection.Open();

                using (var command = new SqlCommand(createDbQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                using (var command = new SqlCommand(createEmpoyeeTable, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            // NOTE: log exception
        }
    }
}