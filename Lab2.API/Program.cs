using Lab2.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices()
                .AddDatabase(builder.Configuration, builder.Environment)
                .AddRepositories();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

app.UseExceptionHandling();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.SeedDataAsync();

app.Run();