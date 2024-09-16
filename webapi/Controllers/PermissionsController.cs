using Elasticsearch.Net;
using Microsoft.AspNetCore.Mvc;
using Nest;
using webapi.Models;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionsController : ControllerBase
    {
        private readonly IElasticClient _elasticClient;
        private readonly IRequestPermissionService _requestPermissionService;

        public PermissionsController(IElasticClient elasticClient, IRequestPermissionService requestPermissionService)
        {
            _elasticClient = elasticClient;
            _requestPermissionService = requestPermissionService;
        }

        [HttpPost]
        public IActionResult RequestPermission(Permission permission)
        {
            // Almacenar el permiso en Elasticsearch
            var indexResponse = _elasticClient.IndexDocument(permission);

            if (!indexResponse.IsValid)
            {
                return StatusCode(500, "Error al almacenar el permiso en Elasticsearch");
            }

            _requestPermissionService.RequestPermission(permission);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetPermissions()
        {
            // Recuperar todos los permisos de Elasticsearch
            var searchResponse = _elasticClient.Search<Permission>(s => s
                .Index("permissions")
                .Query(q => q.MatchAll())
            );

            var permissions = searchResponse.Documents;

            return Ok(permissions);
        }

        [HttpGet("{id}")]
        public IActionResult GetPermission(string id)
        {
            // Recuperar un permiso específico de Elasticsearch
            var getResponse = _elasticClient.Get<Permission>(id);

            if (!getResponse.Found)
            {
                return NotFound();
            }

            var permission = getResponse.Source;

            return Ok(permission);
        }
    }
}