using System.Net;
using System.Threading.RateLimiting;
using Fotografias.Api.Application;
using Fotografias.Api.Infrastructure;
using Microsoft.AspNetCore.RateLimiting;
using Swashbuckle.AspNetCore.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRateLimiter(options => {
    options.AddPolicy("client-policy", HttpContext =>
    {
        var client = HttpContext.User?.Identity?.Name ?? "anon";

        return RateLimitPartition.GetFixedWindowLimiter(client, 
        _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 100,
            Window = TimeSpan.FromMinutes(1)
        });    
    });

    options.AddFixedWindowLimiter("fixed", config =>
    {
        config.PermitLimit = 10;
        config.Window = TimeSpan.FromSeconds(30);
        config.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;     
    });

    options.RejectionStatusCode = (int)HttpStatusCode.TooManyRequests;
});

builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API V1");
    c.DefaultModelsExpandDepth(-1);
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseRateLimiter();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers().RequireRateLimiting("fixed");

app.Run();

public partial class Program{}