using StorageCRUD.Repositories;
using StorageCRUD.StorageInterface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<BlobInterface, BlobRepository>();
builder.Services.AddScoped<FileShareInterface, FileShareRepository>();
builder.Services.AddScoped<QueueInterface, QueueRepository>();
builder.Services.AddScoped<TableInterface, TableRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
