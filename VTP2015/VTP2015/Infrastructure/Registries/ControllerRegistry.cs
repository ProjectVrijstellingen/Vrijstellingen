using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace VTP2015.Infrastructure.Registries
{
    public class ControllerRegistry : Registry
    {
        public ControllerRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.With(new ControllerConvention());
            });
        }
    }
}