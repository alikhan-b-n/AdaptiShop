using System.Text;
using AdaptiShop.DAL;
using AdaptiShop.DAL.Providers.Abstract;
using AdaptiShop.DAL.Providers.EntityProviders;
using AdatiShop.BLL.Services;
using AdatiShop.BLL.Services.Abstract;
using Contracts.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RetAil;


var builder = WebApplication.CreateBuilder(args);

// Add services to dependency injection
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.Configure<SecretOptions>(builder.Configuration.GetSection("SecretOptions"));
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IHistoryService, HistoryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryProvider, CategoryProvider>();
builder.Services.AddScoped<IUserProvider, UserProvider>();
builder.Services.AddScoped<IProductProvider, ProductProvider>();
builder.Services.AddScoped<IHistoryProvider, HistoryProvider>();


#region Jwt Configuration

var secrets = builder.Configuration.GetSection("SecretOptions");

var key = Encoding.ASCII.GetBytes(secrets.GetValue<string>("JWTSecret")!);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

#endregion

builder.Services.AddDbContext<ApplicationContext>(x =>
    x.UseInMemoryDatabase(builder.Configuration.GetConnectionString("InMemory")!));

ConfigureServicesSwagger.ConfigureServices(builder.Services);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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