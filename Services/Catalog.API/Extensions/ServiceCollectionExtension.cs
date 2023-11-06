﻿using Catalog.API.Interfaces.Repositories;
using Catalog.API.Repositories;

namespace Catalog.API.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<IProductRepository, ProductRepository>();
    }
}
