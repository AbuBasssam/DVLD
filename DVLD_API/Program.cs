using DVlD_BusinessLayer;
using DVlD_BusinessLayer.Interfaces;
using DVLD_DataAccessLayer;
using DVLD_DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Hosting.Server;
using System.Configuration;
using static DVlD_BusinessLayer.clsApplicationType;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//ApplicationTypes configuratoin
//-----------------------------------------------

builder.Services.AddScoped<IBLLApplicationTypes, clsApplicationType>();

builder.Services.AddScoped<IApplicationTypesDAL, clsApplicationTypesData>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new clsApplicationTypesData(connectionString);
});

//-----------------------------------------------

//People configuratoin
builder.Services.AddScoped<IPeopleDataInterface, clsPeopleData>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new clsPeopleData(connectionString);
});  

builder.Services.AddScoped<IPerson, clsPerson>();

//-----------------------------------------------

//User configuratoin
builder.Services.AddScoped<IUserDataInterface, clsUserData>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new clsUserData(connectionString);
}); 

builder.Services.AddScoped<IUser, clsUser>();
//-----------------------------------------------

//Driver configuratoin

builder.Services.AddScoped<IDriverData,clsDriverData>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new clsDriverData(connectionString);
});

builder.Services.AddScoped<IBLLDriver, clsDriver>();

//-----------------------------------------------

//Test Types configuratoin
builder.Services.AddScoped<IDALTestTypes, clsTestTypesData>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new clsTestTypesData(connectionString);
});

builder.Services.AddScoped<IBLLTestTypes, clsTestTypes>();

//-----------------------------------------------

//License Classes configuratoin
builder.Services.AddScoped<IDALLicenseClasses, clsLicenseClassesData>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new clsLicenseClassesData(connectionString);
});

builder.Services.AddScoped<IBLLLicenseClass, clsLicenseClasses>();

//-----------------------------------------------


//Test configuratoin
builder.Services.AddScoped<IDALTest, clsTestData>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new clsTestData(connectionString);
});

builder.Services.AddScoped<IBLLTest, clsTest>();

//-----------------------------------------------

//License configuratoin
builder.Services.AddScoped<IDALLicense, clsLicensesData>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new clsLicensesData(connectionString);
});

builder.Services.AddScoped<IBLLLicnese, clsLicense>();

//-----------------------------------------------

//Test Appointment configuratoin
builder.Services.AddScoped<IDALTestAppointment, clsTestAppointmentData>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new clsTestAppointmentData(connectionString);
});

builder.Services.AddScoped<IBLLTestAppointment, clsTestAppointment>();

//-----------------------------------------------

//International License configuratoin
builder.Services.AddScoped<IDALInternationalLicense, clsInternationalInternationalLicenseData>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new clsInternationalInternationalLicenseData(connectionString);
});

builder.Services.AddScoped<IBLLInternationalLicnense, clsInternationalLicense>();

//-----------------------------------------------
//Local Driving License configuratoin
builder.Services.AddScoped<IDALLocalDrivingLicenseApplication, clsLocalDrivingLicenseApplicationData>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new clsLocalDrivingLicenseApplicationData(connectionString);
});

builder.Services.AddScoped<IBLLLocalDrivingLicenseApp, clsLocalDrivingLicenseApplication>();
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
