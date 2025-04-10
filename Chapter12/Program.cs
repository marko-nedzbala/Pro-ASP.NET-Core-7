using Chapter12.Platform;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<MessageOptions>(options =>
{
    options.CityName = "Albany";
});

var app = builder.Build();

app.MapGet("/location", async (HttpContext context, IOptions<MessageOptions> msgOpts) =>
{
    Chapter12.Platform.MessageOptions opts = msgOpts.Value;
    await context.Response.WriteAsync($"{opts.CityName}, " + opts.CountryName);
});

app.Use(async (context, next) =>
{
    await next();
    await context.Response.WriteAsync($"\nStatus Code: {context.Response.StatusCode}");
});

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/short")
    {
        await context.Response.WriteAsync($"Request Short Circuited");
    }
    else
    {
        await next();
    }
});

app.Map("/branch", branch =>
{
    branch.UseMiddleware<Chapter12.Platform.QueryStringMiddleware>();

    branch.Use(async (HttpContext context, Func<Task> next) =>
    {
        await context.Response.WriteAsync($"Branch Middleware");
    });
});

app.Use(async (context, next) =>
{
    if (context.Request.Method == HttpMethods.Get
        && context.Request.Query["custom"] == "true")
    {
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync("Custom Middleware \n");
    }
    await next();
});

app.UseMiddleware<Chapter12.Platform.QueryStringMiddleware>();

app.MapGet("/", () => "Hello World!");

app.Run();
