using System.Reflection;
using System.Text;

namespace OutOfOffice.Persistence.Extensions.Utility;

public static class FilterQueryBuilder
{
    public static (string query, object[] parameters) CreateFilterQueryWithParameters<T>(string filterQueryString)
    {
        var filterParams = filterQueryString.Trim().Split('&');
        var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var filterQueryBuilder = new StringBuilder();
        var parameters = new List<object>();

        foreach (var param in filterParams)
        {
            if (string.IsNullOrWhiteSpace(param))
            {
                continue;
            }

            var parts = param.Split(new[] { '=' }, 2);
            if (parts.Length != 2)
            {
                continue;
            }

            var propertyFromQueryName = parts[0].Trim();
            var propertyValue = parts[1].Trim();
            var objectProperty = propertyInfos.FirstOrDefault(pi =>
                pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

            if (objectProperty == null)
            {
                continue;
            }

            if (objectProperty.PropertyType == typeof(string))
            {
                filterQueryBuilder.Append($"{objectProperty.Name}.Contains(\"{propertyValue.ToLower()}\") && ");
                parameters.Add(propertyValue);
            }
            else if (objectProperty.PropertyType.IsPrimitive || objectProperty.PropertyType.IsValueType || objectProperty.PropertyType.IsEnum)
            {
                filterQueryBuilder.Append($"{objectProperty.Name} == {propertyValue} && ");
                parameters.Add(Convert.ChangeType(propertyValue, objectProperty.PropertyType));
            }
            else if (objectProperty.PropertyType.IsClass && objectProperty.PropertyType != typeof(byte[]))
            {
                filterQueryBuilder.Append($"{objectProperty.Name}.ToString().Contains({propertyValue}) && ");
                parameters.Add(propertyValue);
            }
        }

        var filterQuery = filterQueryBuilder.ToString().TrimEnd('&', ' ');

        return (filterQuery, parameters.ToArray());
    }
}
