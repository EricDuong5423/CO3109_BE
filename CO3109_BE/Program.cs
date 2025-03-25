using CO3109_BE.Controllers.CalcHist;
using CO3109_BE.Entities;
using CO3109_BE.Repository;
using CO3109_BE.Repository.Account;
using CO3109_BE.Repository.CalcHist;
using CO3109_BE.Repository.CalcHist.Chapter2;
using CO3109_BE.Repository.CalcHist.InputData;
using CO3109_BE.Repository.Component.Axis;
using CO3109_BE.Repository.Component.BallBearing;
using CO3109_BE.Repository.Component.ElectricEngine;
using CO3109_BE.Repository.Component.Gear;
using CO3109_BE.Repository.Component.RollerChain;
using CO3109_BE.Settings;
using CO3109_BE.Services;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System.Reflection;
using CO3109_BE.Repository.CalcHist.Chapter3;

try
{
    var builder = WebApplication.CreateBuilder(args);


    //Bind mongoDB to application
    builder.Services.Configure<MongoDbSettings>(
        builder.Configuration.GetSection(nameof(MongoDbSettings)));
    // Add services to the container.
    builder.Services.AddControllers();
    // Add Repository
    builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    // Add dong co dien k repository
    builder.Services.AddScoped<Idong_co_4aRepository, dong_co_4aRepository>();
    // Add dong co dien dk repository
    builder.Services.AddScoped<Idong_co_dkRepository, dong_co_dkRepository>();
    // Add dong co dien 4a repository
    builder.Services.AddScoped<Idong_co_kRepository, dong_co_kRepository>();
    // Add xich con lan repository
    builder.Services.AddScoped<Ixich_con_lanRepository, xich_con_lanRepository>();
    // Add tai khoan khach repository
    builder.Services.AddScoped<Itai_khoan_khachRepository, tai_khoan_khachRepository>();
    // Add tai khoan quan li repository
    builder.Services.AddScoped<Itai_khoan_quan_liRepository, tai_khoan_quan_liRepository>();
    // Add banh rang repository
    builder.Services.AddScoped<Ibanh_rangRepository, banh_rangRepository>();
    // Add truc vong dan hoi repository
    builder.Services.AddScoped<Itruc_vong_dan_hoiRepository, truc_vong_dan_hoiRepository>();
    // Add vong dan hoi repository
    builder.Services.AddScoped<Ivong_dan_hoiRepository, vong_dan_hoiRepository>();
    // Add lich su tinh toan repository
    builder.Services.AddScoped<Ilich_su_tinh_toanRepository, lich_su_tinh_toanRepository>();
    // Add data dau vao repository
    builder.Services.AddScoped<Idata_dau_vaoRepository, data_dau_vaoRepository>();
    // Add chuong 2 repository
    builder.Services.AddScoped<Ichuong_2Repository, chuong_2Repository>();
    // Add o bi repository
    builder.Services.AddScoped<Io_biRepository, o_biRepository>();
    // Add calculating method
    builder.Services.AddScoped<calculatingMethod>();
    // Create a singleton RestClient
    builder.Services.AddSingleton<RestClient>(new RestClient("https://ai-api-f95d.onrender.com/api-ai"));
    builder.Services.AddScoped<AiApiService>();
    // Add chuong 3 repository
    builder.Services.AddScoped<Ichuong_3Repository, chuong_3Repository>();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

    //JSON serializer
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "CO 3109 API", Version = "v1" });
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
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

    app.UseStaticFiles();

    app.MapControllers();

    app.Run();
}
catch(Exception e)
{
    Console.WriteLine($"Startup error: {e.Message}");
    Console.WriteLine($"Trace: {e.StackTrace}");
}
