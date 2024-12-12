using B2BApp.DTOs;
using B2BApp.Entities.Concrete;
using Core.Models.Concrete;
using MongoDB.Bson;

namespace B2BApp.Business.Abstract
{
    public interface ISubeStokService
    {
        void addSubeStok(SubeStok subeStok);
        void addManySubeStok(List<SubeStok> subeStokList);
        void updateSubeStok(SubeStok subeStok, string subeStokId);
        void deleteSubeStok(Guid objectId);
        Result<ICollection<SubeStok>> getAll();
        Result<ICollection<SubeStokDto>> getAllWithSubeAndUrun();
        Result<ICollection<SubeStokDto>> getAllWithSubeAndUrunByTedarikciId
            (
            string tedarikciId,
            string? subeId,
            string? firmaId,
            string? kategoriId

            );
        Result<ICollection<SubeStokDto>> getAllWithDetailsByFilters
            (
            string? subeId,
            string? firmaId,
            string? kategoriId

            );

        Result<SubeStokDto> getWithSubeAndUrun(Guid objectId);
        Result<SubeStok> getSubeStokById(Guid objectId);
    }
}
