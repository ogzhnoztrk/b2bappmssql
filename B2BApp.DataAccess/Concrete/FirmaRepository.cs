using B2BApp.DataAccess.Context;
using B2BApp.DataAccess.Repository;
using B2BApp.Entities.Concrete;

namespace B2BApp.DataAccess.Abstract
{
    public class FirmaRepository : SqlRepositoryBase<Firma>, IFirmaRepository
    {
        public FirmaRepository(SqlDbContext db) : base(db)
        {
        }
    }
}
