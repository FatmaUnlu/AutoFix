using System.Diagnostics;

namespace AutoFix.Inject
{
    public class MyDependency : IMyDependency
    {
        public void Log(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
