using Lab2.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices()
                .AddDatabase(builder.Configuration, builder.Environment)
                .AddIdentity()
                .AddRepositories()
                .AddApplicationAuthentication(builder.Configuration)
                .AddApplicationAuthorization()
                .AddSwagger();

builder.Services.AddControllers();

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

app.UseExceptionHandling();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.SeedDataAsync();

app.Run();