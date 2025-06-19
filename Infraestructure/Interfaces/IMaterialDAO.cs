using Library.Application.DTOs;
using Library.Domain.Entities;
using Library.Domain.Enumerations;

namespace Library.Infraestructure.Interfaces
{
    public interface IMaterialDAO
    {
        List<MaterialDTO> GetAvailableMaterials();
        List<MaterialDTO> GetAllMaterials(string input);
        MaterialDTO? GetMaterial(string materialId);
        bool IsMaterialAvailable(int materialId);
        void InsertMaterial(MaterialDTO material);
        void UpdateMaterial(MaterialDTO material);
        void UpdateMaterialStatus(int materialId, MaterialStatus status);
        void DeleteMaterial(int materialId);
    }
}
