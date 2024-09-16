using MediatR;
using System;
public class RequestPermissionCommand : IRequest
{
    public string EmployeeName { get; set; }
    public string EmployeeLastName { get; set; }
    public int PermissionTypeId { get; set; }
    public DateTime PermissionDate { get; set; }
}