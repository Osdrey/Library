namespace Library.Application.Interfaces
{
    internal interface IMaterialService
    {
        void SearchMaterial();
        void CreateMaterial();
        void UpdateMaterial();
        void DeleteMaterial();
        void ViewAvaraibleMaterials();
    }
}