using Library.Application.DTOs;
using Library.Domain.Entities;
using Library.Domain.Enumerations;

namespace Library.Infraestructure.Interfaces
{
    public interface IMaterialDAO
    {
        List<MaterialDTO> ViewAvailableMaterials();
        List<MaterialDTO> SearchAllMaterials(string input);
        MaterialDTO? SearchMaterial(string materialId);
        bool IsMaterialAvailable(int materialId);
        void CreateMaterial(MaterialDTO material);
        void UpdateMaterial(MaterialDTO material);
        void UpdateMaterialStatus(int materialId, MaterialStatus status);
        void DeleteMaterial(int materialId);
    }
}
