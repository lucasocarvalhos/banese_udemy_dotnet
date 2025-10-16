using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NZWalksDbContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionStrings"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
