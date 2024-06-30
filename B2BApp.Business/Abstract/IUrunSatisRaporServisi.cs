using B2BApp.DTOs;
using Core.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.Business.Abstract
{
	public interface IUrunSatisRaporServisi
	{
		Result<UrunlerVeAylikSatislarDto> getUrunlerVeAylikSatislarByTedarikciId(string tedarikciId);
	}

}
