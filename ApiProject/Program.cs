using ApiProject.Context;
using ApiProject.Core.Interface;
using ApiProject.Core.Servicers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);





// ����� CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // ���� ����� �������� �������
              .AllowAnyHeader()  // ���� ��� ���
              .AllowAnyMethod(); // ���� ��� ����� (GET, POST, PUT, DELETE, ...)
    });
});




// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<ICriminalSrvice, CriminalSrvice>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// ����� ������� ������� �� wwwroot
app.UseStaticFiles();

// ����� index.html ����� ��������
app.UseDefaultFiles();
app.UseStaticFiles();


// ����� CORS
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
