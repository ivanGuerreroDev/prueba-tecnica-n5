using WebApi.Repositories;
namespace WebApi.Services
{
    public class ModifyPermissionHandler
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ModifyPermissionHandler(IPermissionRepository permissionRepository, IUnitOfWork unitOfWork)
        {
            _permissionRepository = permissionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ModifyPermissionCommand command)
        {
            // Busca el permiso existente
            var permission = await _permissionRepository.GetPermissionByIdAsync(command.PermissionId);

            if (permission == null)
            {
                return Result.Failure("Permission not found.");
            }

            // Modificar las propiedades del permiso
            permission.EmployeeName = command.EmployeeName;
            permission.PermissionTypeId = command.PermissionTypeId;

            // Actualiza el permiso en el repositorio
            _permissionRepository.Update(permission);

            // Guarda los cambios en la base de datos
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }
    }

    public class Result
    {
        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; }

        public static Result Success() => new Result { IsSuccess = true };
        public static Result Failure(string errorMessage) => new Result { IsSuccess = false, ErrorMessage = errorMessage };
    }
}
