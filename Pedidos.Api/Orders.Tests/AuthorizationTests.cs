using FluentAssertions;
using FluentAssertions.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orders.Api.Controllers;
using System.Reflection;
using Xunit;

namespace Orders.Tests
{
    public class AuthorizationTests
    {

        [Fact]
        public void ApiControllers_RequiresAuthorization()
        {
            var unauthorizedControllers = new[]
            {
                typeof(HealthCheckController)
            };

            var assembly = Assembly.GetAssembly(typeof(HealthCheckController));
            var controllers = AllTypes.From(assembly).ThatDeriveFrom<ControllerBase>().Except(unauthorizedControllers);
            var controllersMissingAuth = controllers.Where(t => !t.IsDefined(typeof(AuthorizeAttribute), false)).ToList();
            var controllersMissingAuthNames = string.Join(" and ", controllersMissingAuth.Select(x => x.Name));
            controllersMissingAuth.Count.Should().Be(0, "because {0} should have the Authorize attribute", controllersMissingAuthNames);
        }
    }
}
