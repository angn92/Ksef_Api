using Autofac;
using KsefClient.ClientHttp;
using KsefClient.Helpers;
using KsefInfrastructure.Command;
using KsefInfrastructure.CQRS;
using KsefInfrastructure.EF;
using KsefInfrastructure.Helper;

namespace KsefInfrastructure.IoC
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof(IRequestHandler<>))
                .AsImplementedInterfaces();

            builder.RegisterType<RequestDispatcher>()
                .As<IRequestDispatcher>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => x.IsAssignableTo<IRequest>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<AppDbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterType<KsefApiHttp>()
                .As<IAuthChallenge>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UriHelper>()
                .As<IUriHelper>()
                .InstancePerLifetimeScope();

            builder.RegisterType<XmlHelper>()
                .As<IXmlHelper>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CertificateHelper>()
                .As<ICertificateHelper>()
                .InstancePerLifetimeScope();
        }
    }
}
