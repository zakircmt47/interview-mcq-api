
namespace KowQuestAndAns
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.ConfigureServices();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // CORS origin setup
            builder.Services.AddCors(options => options.AddPolicy(name: "NgOrigins",
                policy =>
                {
                    policy.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
                    policy.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
                    policy.WithOrigins("http://192.168.1.7:9001").AllowAnyMethod().AllowAnyHeader();
                    policy.WithOrigins().AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                }));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("NgOrigins");
            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}