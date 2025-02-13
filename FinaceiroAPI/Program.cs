using FinaceiroAPI.Data;
using FinaceiroAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using FinaceiroAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<FinanceiroAPIContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("FinanceiroAPI"))
    );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var chave = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("Chave").Get<string>());

builder.Services.AddAuthentication(
    obj =>
    {
        obj.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        obj.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }   
    
).AddJwtBearer(
    obj =>
    {
        obj.RequireHttpsMetadata = false;
        obj.SaveToken = true;
        obj.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(chave),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    }
);


builder.Services.AddSingleton<TokenService>();


builder.Services.AddScoped<IRepository, Repository>();


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

app.Run();
