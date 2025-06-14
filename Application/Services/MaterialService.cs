using Library.Application.Interfaces;
using Library.Domain.Entities;
using Library.Domain.Enumerations;
namespace Library.Application.Services
{
    internal class MaterialService : IMaterialService
    {
        public void SearchMaterial()
        {
            Console.WriteLine("Buscando material...");
        }
        public void CreateMaterial()
        {
            Console.WriteLine("Creando material...");
        }
        public void UpdateMaterial()
        {
            Console.WriteLine("Actualizando material...");
        }
        public void DeleteMaterial()
        {
            Console.WriteLine("Eliminando material...");
        }
        public void ViewAvaraibleMaterials()
        {
            Console.WriteLine("Viendo materiales disponibles...");
        }
    }
}
