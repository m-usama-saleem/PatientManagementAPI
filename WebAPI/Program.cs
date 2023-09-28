using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebAPI.Contracts;
using WebAPI.DB.Models;
using WebAPI.Services;
using WebAPI.ViewModels.ViewModels;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddDbContext<PatientManagementContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"),
                   builder =>
                   {
                       builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                   }));

builder.Services.AddControllers();
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IPatient, PatientService>();
builder.Services.AddScoped<IDoctor, DoctorService>();
builder.Services.AddScoped<IInvoice, InvoiceService>();
builder.Services.AddScoped<IProcedure, ProcedureService>();

builder.Services.AddScoped(typeof(ICrud<PatientViewModel>), typeof(PatientService));
builder.Services.AddScoped(typeof(ICrud<DoctorViewModel>), typeof(DoctorService));
builder.Services.AddScoped(typeof(ICrud<ProcedureViewModel>), typeof(ProcedureService));
builder.Services.AddScoped(typeof(ICrud<InvoiceViewModel>), typeof(InvoiceService));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.UseIISIntegration();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseCors(x => x.AllowAnyHeader()
      .AllowAnyMethod()
      .AllowAnyOrigin()
      );

app.UseAuthorization();

app.MapControllers();

app.Run();
