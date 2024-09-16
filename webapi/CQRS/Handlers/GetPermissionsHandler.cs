using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using webapi.CQRS.Queries;
using webapi.Models;
using webapi.Repositories;    

namespace webapi.CQRS.Handlers
{
    public class GetPermissionsHandler : IRequestHandler<GetPermissionsQuery, IEnumerable<Permission>>
    {
        private readonly IPermissionRepository _permissionRepository;

        public GetPermissionsHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<IEnumerable<Permission>> handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            return await _permissionRepository.GetAllPermissionsAsync();
        }
    }
}
