using Replacer.Api.Database;
using Replacer.Api.Logic;
using Replacer.Api.Logic.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ReplacerDbContext>();

builder.Services.AddScoped<IUserLogic, UserLogic>();
builder.Services.AddScoped<IActivityLogic, ActivityLogic>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseCors(builder => builder
	 .AllowAnyOrigin()
	 .AllowAnyMethod()
	 .AllowAnyHeader());
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
