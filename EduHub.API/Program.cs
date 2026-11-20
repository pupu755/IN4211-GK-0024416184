using EduHub.Application.Interfaces;
using EduHub.Application.Services;
using EduHub.Domain.Interfaces;
using EduHub.Infrastructure.Repositories;
using EduHub.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Đăng ký Dependency Injection cho các tầng Clean Architecture
builder.Services.AddSingleton<IEventRepository, InMemoryEventRepository>();
builder.Services.AddSingleton<INotificationServiceFactory, NotificationServiceFactory>();
builder.Services.AddScoped<EventService>(sp =>
    new EventService(
        sp.GetRequiredService<IEventRepository>(),
        sp.GetRequiredService<INotificationServiceFactory>()
    )
);

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
