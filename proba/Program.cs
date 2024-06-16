using System.Data.SqlClient;
using Dapper;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
void CreateDatabaseTables(IConfiguration configuration)
{
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    var createTables = configuration.GetValue<bool>("DatabaseSettings:CreateTablesOnStartup");

    if (createTables)
    {
        var sqlScript = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "CreateTables.sql"));

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            connection.Execute(sqlScript);
        }
    }
}