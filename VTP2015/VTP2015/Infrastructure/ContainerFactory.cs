using StructureMap;
using VTP2015.Infrastructure.Registries;

namespace VTP2015.Infrastructure
{
    public static class ContainerFactory
    {
        static ContainerFactory()
        {
            Container = new Container(cfg =>
            {
                cfg.AddRegistry(new StandardRegistry());
                cfg.AddRegistry(new ControllerRegistry());
                cfg.AddRegistry(new ServiceLayerRegistry());
                //cfg.AddRegistry(new MockServiceLayerRegistry());
                cfg.AddRegistry(new TaskRegistry());
                cfg.AddRegistry(new RepostitoryRegistry());
                //cfg.AddRegistry(new MockRepositoryRegistry());
            }); 
        }  

        public static IContainer Container{ get; set; }
    }
}