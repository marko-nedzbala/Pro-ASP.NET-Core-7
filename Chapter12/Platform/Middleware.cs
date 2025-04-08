﻿namespace Chapter12.Platform
{
    public class QueryStringMiddleware
    {
        private RequestDelegate next;

        public QueryStringMiddleware(RequestDelegate nextDelegate)
        {
            next = nextDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            if(context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
            {
                if (!context.Response.HasStarted)
                {
                    context.Response.ContentType = "text/plain";
                }
                await context.Response.WriteAsync("Class Middleware \n");
            }
            await next(context);
        }
    }
}
