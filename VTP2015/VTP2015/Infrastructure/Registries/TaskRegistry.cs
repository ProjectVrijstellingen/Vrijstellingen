using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using VTP2015.Infrastructure.Tasks;

namespace VTP2015.Infrastructure.Registries
{
    public class TaskRegistry : Registry
    {
        public TaskRegistry()
        {
            Scan(scan =>
            {
                scan.AssembliesFromApplicationBaseDirectory(
                    a => a.FullName.StartsWith("VTP2015"));
                scan.AddAllTypesOf<IRunAtInit>();
                scan.AddAllTypesOf<IRunAtStartup>();
                scan.AddAllTypesOf<IRunOnEachRequest>();
                scan.AddAllTypesOf<IRunOnError>();
                scan.AddAllTypesOf<IRunOnEachRequest>();
            });
        }
    }
}