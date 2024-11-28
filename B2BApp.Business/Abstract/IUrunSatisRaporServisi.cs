using B2BApp.Core.Models.Concrete;
using B2BApp.DTOs;

namespace B2BApp.Business.Abstract
{
    public interface IUrunSatisRaporServisi
    {
        Result<UrunlerVeAylikSatislarDto> getUrunlerVeAylikSatislarByTedarikciId(string tedarikciId);
    }

}
