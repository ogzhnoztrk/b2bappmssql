﻿using B2BApp.Core.Models.Concrete.DbSettingsModel;
using B2BApp.DataAccess.Abstract;
using B2BApp.DataAccess.Repository;
using B2BApp.Entities.Concrete;
using Microsoft.Extensions.Options;

namespace B2BApp.DataAccess.Concrete
{
    public class SubeStokRepository : MongoRepositoryBase<SubeStok>, ISubeStokRepository
    {
        public SubeStokRepository(IOptions<MongoSettings> settings) : base(settings)
        {
        }
    }
}
