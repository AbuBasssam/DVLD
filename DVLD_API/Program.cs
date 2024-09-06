using DVlD_BusinessLayer;
using DVlD_BusinessLayer.Interfaces;
using DVLD_DataAccessLayer;
using DVLD_DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Hosting.Server;
using System.Configuration;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register the IPersonService with the concrete implementation PersonService
builder.Services.AddScoped<IPeopleDataInterface, clsPeopleData>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new clsPeopleData(connectionString);
}); ; 


builder.Services.AddScoped<IPerson, clsPerson>();

builder.Services.AddScoped<IUserDataInterface, clsUserData>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new clsUserData(connectionString);
}); ; 

builder.Services.AddScoped<IUser, clsUser>();

builder.Services.AddScoped<IDriverData,clsDriverData>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new clsDriverData(connectionString);
});

// Register the business layer
builder.Services.AddScoped<IBLLDriver, clsDriver>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();

;
