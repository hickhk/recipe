using System.Globalization;

namespace RecipeBook.API.Middleware
{
    public class MiddlewareCulture
    {
        private readonly RequestDelegate _next;
        public MiddlewareCulture(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var suppertedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures);
            var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();
            var cultureInfo = CultureInfo.GetCultureInfo("en-US");

            if (!string.IsNullOrWhiteSpace(requestedCulture) && suppertedLanguages.Any(c => c.Name == requestedCulture))
            {
                cultureInfo = CultureInfo.GetCultureInfo(requestedCulture);
            }

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            await _next(context);
        }
    }
}
