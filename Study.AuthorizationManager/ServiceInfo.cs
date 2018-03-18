using System.Reflection;

namespace Study.AuthorizationManager
{
    internal class ServiceInfo
    {
        private static readonly string _name;
        private static readonly string _version;

        static ServiceInfo()
        {
            AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();
            _name = assemblyName.Name;
            _version = assemblyName.Version.ToString();
        }

        public string Name => _name;

        public string Version => _version;
    }
}