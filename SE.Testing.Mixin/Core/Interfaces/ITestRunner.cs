using System.Collections.Generic;

namespace IngameScript
{
    public interface ITestRunner
    {
        void AddProvider(ITestProvider provider);
        IEnumerator<bool> RunAll();
    }
}