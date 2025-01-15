using Autofac;
using B2BApp.Business.Abstract;
using B2BApp.Business.Concrete;
using B2BApp.DataAccess.Abstract;
using B2BApp.DataAccess.Concrete;

namespace B2BApp.Business.DependecyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerDependency();
            builder.RegisterType<FirmaService>().As<IFirmaService>().InstancePerDependency();
            builder.RegisterType<KategoriService>().As<IKategoriService>().InstancePerDependency();
            builder.RegisterType<SatisService>().As<ISatisService>().InstancePerDependency();
            builder.RegisterType<SubeService>().As<ISubeService>().InstancePerDependency();
            builder.RegisterType<UrunService>().As<IUrunService>().InstancePerDependency();
            builder.RegisterType<SubeStokService>().As<ISubeStokService>().InstancePerDependency();
            builder.RegisterType<TedarikciService>().As<ITedarikciService>().InstancePerDependency();
            builder.RegisterType<UrunSatisRaporServisi>().As<IUrunSatisRaporServisi>().InstancePerDependency();
            builder.RegisterType<FilterService>().As<IFilterService>().InstancePerDependency();
            builder.RegisterType<SiparisService>().As<ISiparisService>().InstancePerDependency();
            builder.RegisterType<KullaniciService>().As<IKullaniciService>().InstancePerDependency();
            builder.RegisterType<AuthService>().As<IAuthService>().InstancePerDependency();





        }
    }
}
