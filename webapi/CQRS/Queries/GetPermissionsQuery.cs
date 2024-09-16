using MediatR;
using System.Collections.Generic;
using webapi.Models;
namespace webapi.CQRS.Queries
{
    public class GetPermissionsQuery : IRequest<IEnumerable<Permission>>
    {
    }
}