using WebApi.Repositories;
using WebApi.Models;

namespace WebApi.Services
{
    public class GetPermissionsHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPermissionsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Permission>> Handle(GetPermissionsQuery query)
        {
            if (query.UserId.HasValue)
            {
                // Filtra los permisos por usuario si se proporciona UserId
                return await _unitOfWork.Permissions.GetAllPermissionsAsync();
            }
            else
            {
                // Devuelve todos los permisos si no se especifica UserId
                return await _unitOfWork.Permissions.GetAllPermissionsAsync();
            }
        }
    }
}
