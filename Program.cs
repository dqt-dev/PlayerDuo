using PlayerDuo.Database;
using PlayerDuo.Repositories.Authen;
using PlayerDuo.Repositories.Orders;
using PlayerDuo.Repositories.Users;
// using PlayerDuo.Services.Email;
using PlayerDuo.Services.Storage;
using PlayerDuo.Services.Token;
// using PlayerDuo.Services.XLSX;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using PlayerDuo.Repositories.Messages;
using PlayerDuo.Repositories.Skills;
using PlayerDuo.Repositories.Categories;
using PlayerDuo.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// add Db context
builder.Services.AddDbContext<MyDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


builder.Services.AddEndpointsApiExplorer();

// add Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "PlayerDuoApi",
        Description = "An ASP.NET Core Back-end API for PlayerDuo",
        Contact = new OpenApiContact
        {
            Name = "Contact via Facebook",
            Url = new Uri("https://www.facebook.com/quocdat.ngoluu/")
        },
    });
    // add authentication header for Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                          Enter 'Bearer' [space] and then your token in the text input below.
                          \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddSignalR();

// add Authentication service
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });


// services cors
builder.Services.AddCors(p => p.AddPolicy("MyCorsPolicy", builder =>
{
    builder.WithOrigins("http://localhost:3000", "http://localhost:3006", "https://curious-pavlova-30d110.netlify.app", "https://joyful-toffee-562315.netlify.app").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
    //builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

// add my services and repositories
builder.Services.AddScoped<IStorageService, StorageService>();
// builder.Services.AddScoped<IXLSXService, XLSXService>();
builder.Services.AddScoped<ITokenService, TokenService>();
// builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<IAuthenRepository, AuthenRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
// builder.Services.AddScoped<ITourRepository, TourRepository>();
// builder.Services.AddScoped<IProviderRepository, ProviderRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseCors("MyCorsPolicy");

// add this to enable access static files in wwwroot via url
app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chat");

app.Run();
