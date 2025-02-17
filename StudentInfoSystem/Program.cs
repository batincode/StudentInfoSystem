using Microsoft.EntityFrameworkCore;
using StudentInfoSystem.Infrastructure.Data;
using StudentInfoSystem.Core.Interfaces;
using StudentInfoSystem.Infrastructure.Repositories;
using StudentInfoSystem.Infrastructure;
using StudentInfoSystem.Application.Services;
using StudentInfoSystem.Core.Services;
using StudentInfoSystem.Application.Mapping;
using AutoMapper;
using System.Reflection;
using StudentInfoSystem.Application.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add scoped services
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<IGradeRepository, GradeRepository>();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddAutoMapper(typeof(TeacherProfile));


// Add controllers
builder.Services.AddControllers();

// Add Razor Pages support
builder.Services.AddRazorPages(); // Razor Pages deste�i eklendi

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware pipeline
//app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages(); // Razor Pages route'lar�n� tan�mla

app.Run();
