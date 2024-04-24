using Microsoft.EntityFrameworkCore;
using Brasserie.Data;
using Brasserie.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{

	var connectionString = builder.Configuration.GetConnectionString("ConnectionMariaDB");
	options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});


builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(
		policy =>
		{
			policy.AllowAnyOrigin()
				.AllowAnyHeader()
				.AllowAnyMethod();
		});
});

builder.Services.AddScoped<BeerService>();
builder.Services.AddScoped<BrewerService>();
builder.Services.AddScoped<SaleService>();
builder.Services.AddScoped<StockService>();

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
