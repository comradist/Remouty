
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using OutOfOffice.Domain.Models.Entities;
using OutOfOffice.Persistence.Extensions.Utility;

namespace OutOfOffice.Persistence.Extensions;

public static class RepositoryEmployeeExtensions
{
    public static IQueryable<Employee> FilterAndSearch(this IQueryable<Employee> employees, string filterQueryString)
    {
        if (string.IsNullOrWhiteSpace(filterQueryString))
        {
            return employees;
        }

        var (filterQuery, parameters) = FilterQueryBuilder.CreateFilterQueryWithParameters<Employee>(filterQueryString);
        var filteredEmployees = employees.Where(filterQuery);

        return filteredEmployees;
    }

    public static IQueryable<Employee> Sort(this IQueryable<Employee> employees, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
        {
            return employees.OrderBy(x => x.FullName);
        }

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Employee>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
        {
            return employees.OrderBy(x => x.FullName);
        }

        //employees.OrderBy(item => item.Name).ThenByDescending(item => item.Age);
        return employees.OrderBy(orderQuery);
    }

}