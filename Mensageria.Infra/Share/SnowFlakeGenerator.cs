using IdGen;
using Mensageria.Domain.Interfaces.Share;

namespace Mensageria.Infra.Share;

public class SnowFlakeGenerator: ISnowFlakeGenerator
{
    private readonly IdGenerator _idGenerator = new IdGenerator(0);
    public string GenerateId()
    {
        return _idGenerator.CreateId().ToString();
    }
}