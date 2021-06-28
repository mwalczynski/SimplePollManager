namespace SimplePollManager.Api.Filters
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            var validationErrorMessages = context.ModelState
                .Keys
                .SelectMany(key => context.ModelState[key].Errors)
                .Select(error => error.ErrorMessage);

            context.Result = new BadRequestObjectResult(new ErrorResponse
            {
                Messages = validationErrorMessages
            });
        }
    }
}
