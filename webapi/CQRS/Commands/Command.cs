public class RequestPermissionCommand : ICommand
{
    private readonly IUnitOfWork _unitOfWork;

    public RequestPermissionCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void Execute()
    {
        // Perform permission request logic
        _unitOfWork.Save();
    }
}