namespace WebApi.Services
{
    public class RequestPermissionCommand
    {
        public int UserId { get; set; }
        public int PermissionTypeId { get; set; }

        public DateTime PermissionDate { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeeLastname { get; set; }
    }
}
