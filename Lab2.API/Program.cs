using Lab2.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices()
                .AddDatabase(builder.Configuration)
                .AddRepositories();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.SeedDataAsync();

app.Run();