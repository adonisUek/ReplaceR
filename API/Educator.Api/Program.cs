using Educator.Api.Database;
using Educator.Api.Logic;
using Educator.Api.Logic.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EducatorDbContext>();

builder.Services.AddScoped<IUserLogic, UserLogic>();
builder.Services.AddScoped<IReplacementLogic, ReplacementLogic>();
builder.Services.AddScoped<IEnrollmentLogic, EnrollmentLogic>();
builder.Services.AddScoped<ISubjectLogic, SubjectLogic>();

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