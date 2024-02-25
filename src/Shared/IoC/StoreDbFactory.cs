using Shared.Data;

namespace Shared.IoC;

public class StoreDbFactory
{
    private StoreDb? _instance;

    public StoreDb GetInstance()
    {
        if (_instance != null) return _instance;
        _instance = StoreDb.Create();
        return _instance;
    }
}