using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace VTP2015.Infrastructure.Registries
{
    public class StandardRegistry : Registry
    {
        public StandardRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
                scan.With(new ControllerConvention());
            });
        }
    }
}