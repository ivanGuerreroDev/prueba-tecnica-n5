using Microsoft.AspNetCore.Mvc;
using webapi.Models;

public class PermissionsController : ControllerBase
{
    private readonly IRequestPermissionService _requestPermissionService;

    public PermissionsController(IRequestPermissionService requestPermissionService)
    {
        _requestPermissionService = requestPermissionService;
    }

    [HttpPost]
    public IActionResult RequestPermission(Permission permission)
    {
        _requestPermissionService.RequestPermission(permission);
        return Ok();
    }
}