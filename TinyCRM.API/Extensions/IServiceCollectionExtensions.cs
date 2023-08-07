using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using TinyCRM.API.Common.ExceptionHandlers;
using TinyCRM.API.Services;
using TinyCRM.Application.Common.Identity;
using TinyCRM.Application.Common.Seeders;
using TinyCRM.Application.Common.UnitOfWorks;
using TinyCRM.Application.Dtos.Shared;
using TinyCRM.Application.Mapper;
using TinyCRM.Application.Repositories;
using TinyCRM.Application.Repositories.Base;
using TinyCRM.Application.Services;
using TinyCRM.Application.Services.Abstracts;
using TinyCRM.Infrastructure.Identity;
using TinyCRM.Infrastructure.Identity.Mapper;
using TinyCRM.Infrastructure.Identity.Services;
using TinyCRM.Infrastructure.Persistent;
using TinyCRM.Infrastructure.Persistent.Repositories;
using TinyCRM.Infrastructure.Persistent.Repositories.Base;
using TinyCRM.Infrastructure.Persistent.UnitOfWorks;

namespace TinyCRM.API.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.EnableSensitiveDataLogging(env.IsDevelopment());
            options.UseSqlServer(configuration.GetConnectionString("Default"));
        });

        services.AddScoped<Func<AppDbContext>>((provider) => () => provider.GetRequiredService<AppDbContext>());
        services.AddScoped<DbContextFactory>();
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IExceptionHandler, ExceptionHandler>();

        services.AddScoped<IAccountService, AccountService>()
                .AddScoped<IContactService, ContactService>()
                .AddScoped<ILeadService, LeadService>()
                .AddScoped<IProductService, ProductService>()
                .AddScoped<IDealService, DealService>()
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<ITokenProvider, JwtTokenProvider>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IDataSeeder, DataSeeder>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped(typeof(IReadOnlyRepository<,>), typeof(ReadOnlyRepository<,>))
                .AddScoped(typeof(ISpecificationRepository<,>), typeof(SpecificationRepository<,>))
                .AddScoped(typeof(IRepository<,>), typeof(Repository<,>))
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<IAccountRepository, AccountRepository>()
                .AddScoped<IContactRepository, ContactRepository>()
                .AddScoped<ILeadRepository, LeadRepository>()
                .AddScoped<IDealRepository, DealRepository>()
                .AddScoped<IDealLineRepository, DealLineRepository>();

        return services;
    }

    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        services.AddScoped<IDataSeeder, IdentityDataSeeder>();

        services.AddScoped<IApplicationUserManager, IdentityUserManager>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<ISignInManager, IdentitySignInManager>();

        //services.AddDbContext<ApplicationIdentityDbContext>(options =>
        //{
        //    options.EnableSensitiveDataLogging(env.IsDevelopment());
        //    options.UseSqlServer(configuration.GetConnectionString("Identity"));
        //});

        services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;

            // Signin settings.
            options.SignIn.RequireConfirmedEmail = false;
        });

        return services;
    }

    public static IServiceCollection AddApplicationAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSetting>(configuration.GetSection("Jwt"));

        services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,

                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    };
                });

        return services;
    }

    public static IServiceCollection AddApplicationAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization();
        services.AddScoped<ICurrentUser, CurrentUser>();

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Tiny CRM",
                Version = "v1"
            });

            c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                BearerFormat = "JWT",
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }

    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(TinyCrmMapper)));
        services.AddAutoMapper(Assembly.GetAssembly(typeof(IdentityMapper)));

        return services;
    }
}