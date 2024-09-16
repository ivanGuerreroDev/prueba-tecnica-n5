using System;
namespace webapi.Models
{
    public class PermissionDocument
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeLastName { get; set; }
        public int PermissionTypeId { get; set; }
        public DateTime PermissionDate { get; set; }
        public string PermissionTypeDescription { get; set; }
    }
}