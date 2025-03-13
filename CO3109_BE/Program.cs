using CO3109_BE.Entities;
using CO3109_BE.Repository;
using CO3109_BE.Repository.dong_co_dien;
using CO3109_BE.Repository.XichConLan;
using CO3109_BE.Settings;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


//Bind mongoDB to application
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection(nameof(MongoDbSettings)));
// Add services to the container.
builder.Services.AddControllers();
// Add Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
// Add dong co dien k repository
builder.Services.AddScoped<Idong_co_dien_kRepository, dong_co_dien_kRepository>();
// Add dong co dien dk repository
builder.Services.AddScoped<Idong_co_dien_dkRepository, dong_co_dien_dkRepository>();
// Add dong co dien 4a repository
builder.Services.AddScoped<Idong_co_dien_4aRepository, dong_co_dien_4aRepository>();
// Add xich con lan repository
builder.Services.AddScoped<IXichConLanRepository, XichConLanRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//JSON serializer
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CO 3109 API", Version = "v1" });
});
builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
builder.Services.AddCors();

var app = builder.Build();

//CORS
app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
    c.RoutePrefix = "swagger";
});

app.UseAuthorization();

app.MapControllers();

app.Run();
