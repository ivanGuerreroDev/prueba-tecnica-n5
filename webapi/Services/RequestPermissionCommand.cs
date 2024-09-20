namespace WebApi.Services
{
    public class RequestPermissionCommand
    {
        public int UserId { get; set; }
        public int PermissionTypeId { get; set; }
    }
}
