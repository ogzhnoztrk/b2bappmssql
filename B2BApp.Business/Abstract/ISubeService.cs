using B2BApp.Core.Models.Concrete;
using B2BApp.DTOs;
using B2BApp.Entities.Concrete;
using MongoDB.Bson;

namespace B2BApp.Business.Abstract
{
    public interface ISubeService
    {
        void addSube(Sube sube);
        void updateSube(Sube sube, string subeId);
        void deleteSube(ObjectId objectId);
        Result<ICollection<Sube>> getAll(string? firmaId);
        Result<Sube> getSubeById(ObjectId objectId);
        Result<ICollection<Sube>> getSubeByFirmaId(string subeId);
        Result<ICollection<SubeDto>> getSubelerWithFirma();
        Result<SubeDto> getSubeWithFirma(ObjectId objectId);
    }
}
