using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data;
using Shared.IoC;

namespace Shared;

public abstract class BaseTestController(IServiceProvider serviceProvider)
    : ControllerBase
{
    private StoreDb? _db;
    protected StoreDb StoreDb
    {
        get
        {
            if (_db is not null) return _db;
            var factory = serviceProvider.GetRequiredService<StoreDbFactory>();
            return _db = factory.GetInstance();
        }
    }
}