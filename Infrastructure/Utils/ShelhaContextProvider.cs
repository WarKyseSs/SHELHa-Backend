using Infrastructure.Ef;

namespace Infrastructure.Utils;

public class ShelhaContextProvider
{
    private readonly IConnectionStringProvider _connectionStringProvider;

    public ShelhaContextProvider(IConnectionStringProvider connectionStringProvider)
    {
        _connectionStringProvider = connectionStringProvider;
    }

    public ShelhaContext NewContext()
    {
        return new ShelhaContext(_connectionStringProvider);
    }
}
