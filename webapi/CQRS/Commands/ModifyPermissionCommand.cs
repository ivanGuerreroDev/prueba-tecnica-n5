using MediatR;
using System;
public class ModifyPermissionCommand : IRequest
{
    public int Id { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeLastName { get; set; }
    public int PermissionTypeId { get; set; }
    public DateTime PermissionDate { get; set; }
}