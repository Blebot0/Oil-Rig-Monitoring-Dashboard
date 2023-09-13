using OilRigWebApi.Models;

namespace OilRigWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var apiCorsPolicy = "ApiCorsPolicy";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: apiCorsPolicy,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:3000")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod()
                                        .AllowCredentials();
                                      //.WithMethods("OPTIONS", "GET");
                                  });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddDbContext<PostgresContext>();


            var app = builder.Build();
            app.UseHttpsRedirection();

            app.UseCors(apiCorsPolicy);

            app.UseAuthorization();
            app.MapControllers();
            app.Run();

        }
    }
}