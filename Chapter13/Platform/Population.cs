﻿namespace Chapter13.Platform
{
    public class Population
    {
        public static async Task Endpoint(HttpContext context)
        {
            string city = context.Request.RouteValues["city"] as string ?? "london";
            int? pop = null;

            //switch ((city ?? "").ToLower())
            switch (city.ToLower())
            {
                case "london":
                    pop = 8_136_000;
                    break;
                case "paris":
                    pop = 2_141_000;
                    break;
                case "monaco":
                    pop = 39_000;
                    break;
            }
            if (pop.HasValue)
            {
                    await context.Response.WriteAsync($"City: {city}, Population: {pop}");
                    return;
            } else
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
        }
    }
}
