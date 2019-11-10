using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.Utilities;

namespace Web.Extensions.Filters
{
    public class ValidateMultipleFormDataAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!MultipartRequestHelper.IsMultipartContentType(context.HttpContext.Request.ContentType))
            {
                context.Result = new BadRequestObjectResult("Content Type is not mime-multipart.");
            }
        }
    }
}
