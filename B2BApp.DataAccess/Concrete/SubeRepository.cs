using B2BApp.DataAccess.Context;
using B2BApp.DataAccess.Repository;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete.DbSettingsModel;
using Microsoft.Extensions.Options;

namespace B2BApp.DataAccess.Abstract
{
    public class SubeRepository : SqlRepositoryBase<Sube>, ISubeRepository
    {
        public SubeRepository(SqlDbContext db) : base(db)
        {
        }
    }
}
