using Application.Services;
using Application.UnitOfWork;
using Domain.IRepositories.IBankAccountRepositorie;
using Domain.IRepositories.IBankSafeDocumentRepositorie;
using Domain.IRepositories.IBankSafeRepositorie;
using Domain.IRepositories.IBankSafeTransactionsRepositorie;
using Domain.IRepositories.IChatRoomRepositorie;
using Domain.IRepositories.ILoanRepositorie;
using Domain.IRepositories.ISmsSevice;
using Domain.IRepositories.IUserAndNumberOfShareRepositorie;
using Domain.IRepositories.IUserRepositorie;
using HealthChecks.UI.Client;
using Infrastructure.Repositories.BankAccountRepositorie;
using Infrastructure.Repositories.BankSafeDocumentRepositorie;
using Infrastructure.Repositories.BankSafeRepositorie;
using Infrastructure.Repositories.BankSafeTransactionsRepositorie;
using Infrastructure.Repositories.ChatRoomRepositorie;
using Infrastructure.Repositories.LoanRepositorie;
using Infrastructure.Repositories.SmsServiceRepositorie;
using Infrastructure.Repositories.UserAndNumberOfShareRepositorie;
using Infrastructure.Repositories.UserRepositorie;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;
using Prometheus;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;
using WebSite.Controllers;
using WebSite.Hubs;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();
//Database
string Connection = builder.Configuration.GetConnectionString("sqlServer");
builder.Services.AddSqlServer<DbContextEF>(Connection);

builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString("sqlServer"));

builder.Services.AddSignalR();


builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{

    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JWTConfig:issuer"],
        ValidAudience = builder.Configuration["JWTConfig:audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTConfig:Key"]))
    };
});


builder.Services.AddScoped<TokenService>();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUnitOfWork, DbContextEF>();
builder.Services.AddScoped<IUserRepositorieCommand, UserRepositorieCommand>();
builder.Services.AddScoped<IUserRepositorieQuery, UserRepositorieQuery>();
builder.Services.AddScoped<IBankAccountRepositorieCommand, BankAccountRepositorieCommand>();
builder.Services.AddScoped<IBankAccountRepositorieQuery, BankAccountRepositorieQuery>();
builder.Services.AddScoped<IBankSafeRepositorieCommand, BankSafeRepositorieCommand>();
builder.Services.AddScoped<IBankSafeRepositorieQuery, BankSafeRepositorieQuery>();
builder.Services.AddScoped<IUserAndNumberOfShareRepositorieCommand, UserAndNumberOfShareRepositorieCommand>();
builder.Services.AddScoped<IUserAndNumberOfShareRepositorieQuery, UserAndNumberOfShareRepositorieQuery>();
builder.Services.AddScoped<IBankSafeTransactionsRepositorieCommand, BankSafeTransactionsRepositorieCommand>();
builder.Services.AddScoped<IBankSafeTransactionsRepositorieQuery, BankSafeTransactionsRepositorieQuery>();
builder.Services.AddScoped<IBankSafeDocumentRepositorieCommand, BankSafeDocumentRepositorieCommand>();
builder.Services.AddScoped<IBankSafeDocumentRepositorieQuery, BankSafeDocumentRepositorieQuery>();
builder.Services.AddScoped<ILoanRepositorieCommand, LoanRepositorieCommand>();
builder.Services.AddScoped<ILoanRepositorieQuery, LoanRepositorieQuery>();
builder.Services.AddScoped<IChatRoomRepositorieCommand, ChatRoomRepositorieCommand>();
builder.Services.AddScoped<IChatRoomRepositorieQuery, ChatRoomRepositorieQuery>();
builder.Services.AddScoped<ISmsServiceRepositorieQuery, SmsServiceRepositorieQuery>();
builder.Services.AddHttpClient();


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = "Enter JWT Bearer token **_only_**",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            new string[] { }
        }
    });
});




builder.Services.AddControllers();


builder.Services.RegisterApplication();

var app = builder.Build();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization(); 

app.MapControllers();

// تنظیمات Swagger برای محیط توسعه
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
    });
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapHealthChecks("/health", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        endpoints.MapHub<SiteChatHub>("/chathub");
        endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
    });
}

// اضافه کردن سایر تنظیمات
app.UseMetricServer();
app.UseHttpMetrics();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.Run();
