using System.Collections.Generic;

namespace IngameScript
{
    public interface ITestProvider
    {
        IEnumerable<Test> GetTests();
    }
}