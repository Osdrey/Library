using Library.Domain.Enumerations;

namespace Library.Application.Interfaces
{
    public interface IMaterialService
    {
        void ViewAvailableMaterials();
        void SearchAllMaterials();
        void SearchMaterial();
        void CheckAvailableMaterial();
        void CreateMaterial();
        void UpdateMaterial();
        void UpdateMaterialStatus(int id, MaterialStatus status);
        void DeleteMaterial();
    }
}