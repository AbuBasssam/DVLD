using DVlD_BusinessLayer;
using DVlD_BusinessLayer.Interfaces;
using DVLD_DataAccessLayer;
using DVLD_DataAccessLayer.Interfaces;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register the IPersonService with the concrete implementation PersonService
builder.Services.AddScoped<IPeopleDataInterface, clsPeopleData>(); // Register the implementation for IPeopleDataInterface

builder.Services.AddScoped<IPerson, clsPerson>();

builder.Services.AddScoped<IUserDataInterface, clsUserData>(); // Register the implementation for IPeopleDataInterface

builder.Services.AddScoped<IUser, clsUser>();


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
