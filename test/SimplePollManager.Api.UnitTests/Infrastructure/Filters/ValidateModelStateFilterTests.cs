namespace SimplePollManager.Api.UnitTests.Infrastructure.Filters
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Routing;
    using Moq;
    using SimplePollManager.Api.Infrastructure.Filters;
    using Xunit;

    public class ValidateModelStateFilterTests
    {
        private readonly ValidateModelStateFilter filter = new ValidateModelStateFilter();

        [Fact]
        public void When_ModelState_is_valid_Then_result_is_empty()
        {
            // When
            var context = this.GetMockedContext(null, null);
            this.filter.OnActionExecuting(context);

            // Then
            context.Result.Should().BeNull();
        }

        [Fact]
        public void When_ModelState_is_not_valid_Then_bad_request_returned()
        {
            // When
            var context = this.GetMockedContext("some_field", "Some error message");
            filter.OnActionExecuting(context);

            // Then
            context.Result.Should().NotBeNull();
            context.Result.Should().BeOfType<BadRequestObjectResult>()
                .Which.Value.Should().BeOfType<ErrorResponse>()
                .Which.Messages.Should().ContainMatch("*error message*");
        }

        private ActionExecutingContext GetMockedContext(string key, string errorMessage)
        {
            var modelState = new ModelStateDictionary();

            if (key != null)
            {
                modelState.AddModelError(key, errorMessage);
            }

            var actionContext = new ActionContext(
                Mock.Of<HttpContext>(),
                Mock.Of<RouteData>(),
                Mock.Of<ActionDescriptor>(),
                modelState
            );

            var context = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                Mock.Of<Controller>()
            );

            return context;
        }
    }
}
