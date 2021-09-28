using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using cqrs.template.domain.Exceptions;

namespace cqrs.template.api.v1.Controllers.V1
{
    [ApiVersion("1")]
    [ApiController]
    public class InitialController : BaseController
    {
        public InitialController(INotificationHandler<ExceptionNotification> notifications) : base(notifications)
        {
        }
        
        [HttpGet]
        [ProducesResponseTypeAttribute(typeof(IEnumerable<string>), 200)]
        public async Task<IActionResult> Sample()
        {
            var ipsum = new List<string> {"Nothing", "Here", "Just", "Hello"};

            return Response(Ok(new {ipsum}));
        }
    }
}