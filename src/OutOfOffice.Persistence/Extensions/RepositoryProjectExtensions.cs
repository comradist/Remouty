using System.Linq.Dynamic.Core;
using OutOfOffice.Domain.Models.Entities;
using OutOfOffice.Persistence.Extensions.Utility;

namespace OutOfOffice.Persistence.Extensions;

public static class RepositoryProjectExtensions
{
    public static IQueryable<Project> FilterAndSearch(this IQueryable<Project> project, string filterQueryString)
    {
        if (string.IsNullOrWhiteSpace(filterQueryString))
        {
            return project;
        }

        var (filterQuery, parameters) = FilterQueryBuilder.CreateFilterQueryWithParameters<Project>(filterQueryString);
        var filteredProject = project.Where(filterQuery);

        return filteredProject;
    }

    public static IQueryable<Project> Sort(this IQueryable<Project> projects, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
        {
            return projects.OrderBy(x => x.StartDate);
        }

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Project>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
        {
            return projects.OrderBy(x => x.StartDate);
        }

        //Projects.OrderBy(item => item.Name).ThenByDescending(item => item.Age);
        return projects.OrderBy(orderQuery);
    }
}