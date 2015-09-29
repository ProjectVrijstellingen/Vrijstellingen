using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using StructureMap;

namespace VTP2015.Infrastructure
{
    public static class ContainerFactory
    {
        private static readonly Lazy<Container> ContainerBuilder =
            new Lazy<Container>(DefaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IContainer Container => ContainerBuilder.Value;

        private static Container DefaultContainer()
        {
            return new Container(x =>
            {
                // default config
            });
        }
    }
}