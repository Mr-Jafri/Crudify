
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationContext(builder.Configuration);

builder.Services.AddDependencyInjections(builder.Configuration);

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(TokenAuthentication));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("default", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

builder.Services.AddOpenApi();
builder.Services.AddSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crudify-API");
    });
}

app.SeedData();

app.UseMiddleware<ActivityLoggingMiddleware>();
app.UseMiddleware<ExceptionLoggingMiddleware>();
app.UseCors("default");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
