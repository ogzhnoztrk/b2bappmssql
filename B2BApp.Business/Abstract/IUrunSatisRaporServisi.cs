using B2BApp.DTOs;
using Core.Models.Concrete;

namespace B2BApp.Business.Abstract
{
    public interface IUrunSatisRaporServisi
    {
        Result<UrunlerVeAylikSatislarDto> getUrunlerVeAylikSatislarByTedarikciId(string tedarikciId);
    }

}
