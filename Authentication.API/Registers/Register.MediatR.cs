using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using Authentication.Common.Mediatr.Commands.Abstractions;
using Authentication.Common.Mediatr.Query.Abstractions;

namespace Authentication.API.Registers;

public static partial class Register
{
    public static IServiceCollection RegisterMediatR(this IServiceCollection services)
    {
        // 1️⃣ Register dispatchers
        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        services.AddScoped<IQueryDispatcher, QueryDispatcher>();

        // 2️⃣ Scan assemblies manually
        var assemblies = new[]
        {
            typeof(Core.Commands.CommandsAssemblyMarker).Assembly,
            typeof(Core.Queries.QueriesAssemblyMarker).Assembly
        };

        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes()
                                .Where(t => !t.IsAbstract && !t.IsInterface);

            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces()
                                     .Where(i => i.IsGenericType);

                foreach (var iface in interfaces)
                {
                    var genericDef = iface.GetGenericTypeDefinition();

                    if (genericDef == typeof(ICommandHandler<,>) ||
                        genericDef == typeof(IQueryHandler<,>))
                    {
                        services.AddScoped(iface, type);
                    }
                }
            }
        }

        return services;
    }
}
