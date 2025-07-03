using MediatR;
using ReporteService.Application.Commands;
using ReporteService.Dominio.Repositorios;
using ReporteService.Infrastructure.Repositorios;
using Microsoft.EntityFrameworkCore;
using UsuarioServicio.Infrastructure.Persistencia;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MassTransit;
using ReporteService.Application.Sagas;
using ReporteService.Infrastructure.Mongo;
using ReporteService.Infrastructure.MongoDB;
using ReporteService.Infrastructure.Consumers;
using ReporteService.Dominio.Interfaces;
using ReporteService.Infrastructure.EventPublishers;
using System.Reflection;
using ReporteService.Infraestructura.Repositorios;


var builder = WebApplication.CreateBuilder(args);

//Swagger
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IReporteRepository, ReporteRepository>();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CrearReporteCommand).Assembly));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var keycloak = builder.Configuration.GetSection("Keycloak");
        options.Authority = "http://localhost:8081/realms/microservicio-usuarios";
        options.Audience = "account";
        options.RequireHttpsMetadata = false; // solo si estás en desarrollo local
    });

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "ReporteService.Api",
        Version = "v1"
    });

    // Configuración de seguridad JWT
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Ingresa el token JWT como: Bearer {token}"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


//Mongo 

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoSettings"));
builder.Services.AddSingleton<MongoDbContext>();


// MassTransit
builder.Services.AddMassTransit(x =>
{
    // 1. Registrar consumidores
    x.AddConsumer<ReporteCreadoConsumer>();
    x.AddConsumer<ReporteStateChangedConsumer>(); // 👈 Nuevo consumer agregado

    // 2. Registrar la saga
    x.AddSagaStateMachine<ReporteStateMachine, ReporteState>()
        .MongoDbRepository(r =>
        {
            r.Connection = builder.Configuration["MongoSettings:ConnectionString"];
            r.DatabaseName = builder.Configuration["MongoSettings:DatabaseName"];
            r.CollectionName = "reporte_sagas"; // opcional
        });


    // 3. Configurar RabbitMQ
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h => { });

        // Consumer para evento SubastaCreada
        cfg.ReceiveEndpoint("reporte-creado-event", e =>
        {
            e.ConfigureConsumer<ReporteCreadoConsumer>(context);
        });

        // ✅ Nuevo endpoint para el cambio de estado
        cfg.ReceiveEndpoint("reporte-state-changed-event", e =>
        {
            e.ConfigureConsumer<ReporteStateChangedConsumer>(context);
        });

        // Endpoint para la saga
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddScoped<IPublicadorReporteEventos, PublicadorReporteEventos>();
builder.Services.AddSingleton<IReporteMongoContext, MongoDbContext>();
builder.Services.AddScoped<IMongoReporteRepository, MongoAuctionRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run()
;
