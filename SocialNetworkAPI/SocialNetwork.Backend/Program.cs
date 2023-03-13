using AutoMapper;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.Migrations;
using SocialNetwork.Backend.Utils;
using SocialNetwork.Backend.Model;
using SocialNetwork.Backend.Services;
using SocialNetwork.Backend;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.EnvironmentName.Equals("Docker"))
{
    builder.Configuration.AddJsonFile($"appsettings.Docker.json");
}

builder.Services.AddControllers();
builder.Services
    .AddLogging(c => c.AddFluentMigratorConsole())
    .AddFluentMigratorCore()
    .ConfigureRunner(c => c
        .AddMySql5()
        .WithGlobalConnectionString(builder.Configuration.GetConnectionString("Database"))
        .ScanIn(typeof(InitialMigration).Assembly).For.All());

builder.Services.AddDbContext<DefaultContext>();
builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter());
});
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
{
    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
}));

#region Services

builder.Services.AddScoped<PostsService>();
builder.Services.AddScoped<CommentsService>();
builder.Services.AddScoped<GroupsService>();
builder.Services.AddScoped<UsersService>();

#endregion

#region Automapper

var mapperConfig = new MapperConfiguration(c =>
{
    c.AddProfile(new AutoMapperProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

mapperConfig.AssertConfigurationIsValid();

#endregion

var app = builder.Build();
// Configure the HTTP request pipeline.

app.UseCors("CorsPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Migrate();
app.Run();