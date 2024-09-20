using WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionsController : ControllerBase
    {
        private readonly RequestPermissionHandler _requestPermissionHandler;
        private readonly GetPermissionsHandler _getPermissionsHandler;

        public PermissionsController(RequestPermissionHandler requestPermissionHandler, GetPermissionsHandler getPermissionsHandler)
        {
            _requestPermissionHandler = requestPermissionHandler;
            _getPermissionsHandler = getPermissionsHandler;
        }

        [HttpPost("request")]
        public async Task<IActionResult> RequestPermission([FromBody] RequestPermissionCommand command)
        {
            await _requestPermissionHandler.Handle(command);
            return Ok();
        }

        [HttpGet("permissions")]
        public async Task<IActionResult> GetPermissions([FromQuery] GetPermissionsQuery query)
        {
            var permissions = await _getPermissionsHandler.Handle(query);
            return Ok(permissions);
        }
    }
}