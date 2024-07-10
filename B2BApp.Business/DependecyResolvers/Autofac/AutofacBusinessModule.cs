using Autofac;
using B2BApp.Business.Abstract;
using B2BApp.Business.Concrete;
using B2BApp.DataAccess.Abstract;
using B2BApp.DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.Business.DependecyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().SingleInstance();
            builder.RegisterType<FirmaService>().As<IFirmaService>().SingleInstance();
            builder.RegisterType<KategoriService>().As<IKategoriService>().SingleInstance();
            builder.RegisterType<SatisService>().As<ISatisService>().SingleInstance();
            builder.RegisterType<SubeService>().As<ISubeService>().SingleInstance();
            builder.RegisterType<UrunService>().As<IUrunService>().SingleInstance();
            builder.RegisterType<SubeStokService>().As<ISubeStokService>().SingleInstance();
            builder.RegisterType<TedarikciService>().As<ITedarikciService>().SingleInstance();
            builder.RegisterType<UrunSatisRaporServisi>().As<IUrunSatisRaporServisi>().SingleInstance();
            builder.RegisterType<FilterService>().As<IFilterService>().SingleInstance();
            builder.RegisterType<SiparisService>().As<ISiparisService>().SingleInstance();
            builder.RegisterType<KullaniciService>().As<IKullaniciService>().SingleInstance();
            builder.RegisterType<AuthService>().As<IAuthService>().SingleInstance();





        }
    }
}
