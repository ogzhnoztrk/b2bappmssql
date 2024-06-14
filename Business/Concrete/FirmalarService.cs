using B2BApp.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BApp.Business.Abstract
{
    public class FirmalarService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FirmalarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



    }
}
