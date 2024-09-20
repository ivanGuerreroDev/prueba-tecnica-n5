using System;
namespace WebApi.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeLastName { get; set; }
        public string EmployeeLastname { get; internal set; }
        public int PermissionTypeId { get; set; }
        public DateTime PermissionDate { get; set; }
        public PermissionType PermissionType { get; set; }
        public object PermissionTypeDescription { get; internal set; }
        public int UserId { get; internal set; }
        public DateTime DateRequested { get; internal set; }
    }
}
