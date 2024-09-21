namespace WebApi.Services
{
    public class ModifyPermissionCommand
    {
        public int PermissionId { get; set; }
        public string EmployeeName { get; set; }

        public string EmployeeLastname { get; set; }

        public DateTime PermissionDate { get; set; }
        public int PermissionTypeId { get; set; }
    }
}
