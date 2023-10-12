using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Core.Utilities.Security.Encyption;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Business.DependencyResolvers.Autofac;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.DependencyResolvers;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));


builder.Services.AddControllers();

//-------------JWT---------------
builder.Services.AddCors(options =>
{
    //Localhost must be change
    options.AddPolicy(name: "AllowOrigin", configurePolicy: builder => builder.WithOrigins("http://localhost:3000").AllowAnyHeader());
});

var tokenOptions = new TokenOptions
{
    Audience = builder.Configuration.GetSection("TokenOptions:Audience").Value!,
    Issuer = builder.Configuration.GetSection("TokenOptions:Issuer").Value!,
    AccessTokenExpiration = int.Parse(builder.Configuration.GetSection("TokenOptions:AccessTokenExpiration").Value!),
    SecurityKey = builder.Configuration.GetSection("TokenOptions:SecurityKey").Value!
};

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
    };
});
//------------------------------
builder.Services.AddDependencyResolvers(new ICoreModule[]
{
    new CoreModule(),
});
//------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowOrigin");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
