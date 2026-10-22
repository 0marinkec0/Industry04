using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common;

public static class Mapper
{
    public static TDestination Map<TSource, TDestination>(
       TSource source)
    {
        ArgumentNullException.ThrowIfNull(source);

        var sourceType = typeof(TSource);
        var destinationType = typeof(TDestination);

        var sourceProperties = sourceType
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead)
            .ToDictionary(p => p.Name);

        var constructors = destinationType.GetConstructors()
            .OrderByDescending(c => c.GetParameters().Length);

        foreach (var constructor in constructors)
        {
            var parameters = constructor.GetParameters();

            var args = new object?[parameters.Length];
            var matched = true;

            for (int i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];

                if (parameter.Name is null ||
                    !sourceProperties.TryGetValue(
                        parameter.Name, out var property) ||
                    !parameter.ParameterType.IsAssignableFrom(
                        property.PropertyType))
                {
                    matched = false;
                    break;
                }

                args[i] = property.GetValue(source);
            }

            if (matched)
                return (TDestination)constructor.Invoke(args);
        }

        throw new InvalidOperationException(
            $"Nije pronađen odgovarajući konstruktor za {destinationType.Name}.");
    }
}
