using Chapter13;
using Chapter13.Platform;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.UseMiddleware<Population>();
//app.UseMiddleware<Capital>();

//app.UseRouting();

//#pragma warning disable ASP0014
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapGet("routing", async context =>
//    {
//        await context.Response.WriteAsync("Request Was Routed");
//    });
//    endpoints.MapGet("capital/uk", new Capital().Invoke);
//    endpoints.MapGet("population/paris", new Population().Invoke);
//});



app.MapGet("{first:alpha:length(3)}/{second:bool}/{*catchall}", async context =>
{
    await context.Response.WriteAsync("Request Was Routed\n");
	foreach (var kvp in context.Request.RouteValues)
	{
		await context.Response.WriteAsync($"{kvp.Key}: {kvp.Value}\n");
	}
});

app.MapGet("capital/{country=France}", Capital.Endpoint);
//app.MapGet("size/{city}", Population.Endpoint).WithMetadata(new RouteNameMetadata("population"));
app.MapGet("size/{city?}", Population.Endpoint).WithMetadata(new RouteNameMetadata("population")); ;

app.Use(async (context, next) =>
{
	Endpoint? end = context.GetEndpoint();
	if (end != null)
	{
		await context.Response.WriteAsync($"{end.DisplayName} Selected \n");
	}
	else
	{
		await context.Response.WriteAsync("No Endpoint Selected \n");
	}
	await next();
});

app.Map("{number:int}", async context =>
{
	await context.Response.WriteAsync("Routed to the int endpoint");
}).WithDisplayName("Int Endpoint").Add(b => ((RouteEndpointBuilder)b).Order = 1);

app.Map("{number:double}", async context =>
{
    await context.Response.WriteAsync("Routed to the double endpoint");
}).WithDisplayName("Double Endpoint").Add(b => ((RouteEndpointBuilder)b).Order = 2);

//app.MapGet("routing", async context =>
//{
//    await context.Response.WriteAsync("Request was routed");
//});
//app.MapGet("capital/uk", new Capital().Invoke);
//app.MapGet("population/pairs", new Population().Invoke);



//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("Terminal Middleware Reached");
//});

app.MapFallback(async context =>
{
	await context.Response.WriteAsync("Routed to fallback endpoint");
});

app.Run();











