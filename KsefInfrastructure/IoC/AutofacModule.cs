using Autofac;
using KsefClient.ClientHttp;
using KsefInfrastructure.EF;

namespace KsefInfrastructure.IoC
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppDbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterType<KsefApiHttp>()
                .As<IAuthChallenge>()
                .InstancePerLifetimeScope();

            
        }
    }
}
