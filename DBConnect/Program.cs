using DBConnect.Models;
using Npgsql;
using System;

///////////////////////////////////////////////////////////////
// Change the password here to run it on your local system DB//
///////////////////////////////////////////////////////////////
var connString = "Host=localhost;Username=postgres;Password=admin;Database=postgres";

var dataSourceBuilder = new NpgsqlDataSourceBuilder(connString);
var dataSource = dataSourceBuilder.Build();

var conn = await dataSource.OpenConnectionAsync();
async void DatabaseUpdated()
{

    var EntryID = 0;

    List<List<string>> rigs = new List<List<string>>
    {
        new List<string> { "RIG1", "MUMBAI", "MUMRIG1" },
        new List<string> { "RIG2", "MUMBAI", "MUMRIG2" },
        new List<string> { "RIG3", "MUMBAI", "MUMRIG3" },
        new List<string> { "RIG4", "SURAT", "SURRIG1" },
        new List<string> { "RIG5", "SURAT", "SURRIG2" },
        new List<string> { "RIG6", "KOLKATA", "KOLRIG1" },
        new List<string> { "RIG7", "KOLKATA", "KOLRIG2" }
    };

    List<string> status = new List<string>
    {
        "OK",
        "NOT OK"
    };

    while (true)
    {
        Random r = new Random();
        int rInt = r.Next(0, 7);
        double rTemp= r.NextDouble() * 100;
        double rPressure = r.NextDouble() * 20;
        double rProd = r.NextDouble() * 10000;
        int rProb = r.Next(1, 11);
        await using (var cmd = new NpgsqlCommand("INSERT INTO public.\"OilRig\"(\r\n\t\"EntryId\", " +
            "\"RigName\", \"Temperature\", \"Pressure\", \"ProductionRate\", \"Status\", " +
            "\"RigLocation\", \"RigId\")\r\n\t" +
            "VALUES (@EntryID, @RigName, @Temp, @Pres, @Prod, @Sta, @Loc, @RigID);", conn))
        {
            
            cmd.Parameters.AddWithValue("EntryID", EntryID.ToString());
            cmd.Parameters.AddWithValue("RigName", rigs[rInt][2]);
            cmd.Parameters.AddWithValue("RigID", rigs[rInt][0]);
            cmd.Parameters.AddWithValue("Loc", rigs[rInt][1]);
            cmd.Parameters.AddWithValue("Temp", rTemp);
            cmd.Parameters.AddWithValue("Pres", rPressure);
            cmd.Parameters.AddWithValue("Sta", rProb > 9 ? status[1] : status[0]);
            cmd.Parameters.AddWithValue("Prod", rProd.ToString() + "barrels/s");
            await cmd.ExecuteNonQueryAsync();
            EntryID++;
            Task.Delay(5000).Wait();
        }
    }


}



// Insert some data

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PostgresContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
DatabaseUpdated();
app.Run();
await using (var cmd = new NpgsqlCommand("DELETE FROM public.\"OilRig\";", conn))
{
    await cmd.ExecuteNonQueryAsync();
}
