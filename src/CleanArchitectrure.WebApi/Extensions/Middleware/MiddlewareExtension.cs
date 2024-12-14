namespace CleanArchitectrure.WebApi.Extensions.Middleware
{
    /// <summary>
    /// Middleware Extension
    /// </summary>
    public static class MiddlewareExtension
    {
        /// <summary>
        /// Add Middleware
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder AddMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ValidationMiddleware>();
        }
    }
}
