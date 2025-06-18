using Library.Application.Interfaces;
using Library.Application.DTOs;
using Library.Domain.Enumerations;
using Library.Infraestructure.Interfaces;
using Library.Presentation.UI.Printers;
using Library.Application.Exceptions;
using Library.Domain.Structures;
using Library.Presentation.UI.Inputs;

namespace Library.Application.Services
{
    internal class MaterialService : IMaterialService
    {
        private readonly IMaterialDAO _materialDao;

        public MaterialService(IMaterialDAO materialDao) => _materialDao = materialDao;

        public void ViewAvailableMaterials()
        {
            Console.Clear();
            var list = _materialDao.ViewAvailableMaterials();
            if (list.Count == 0)
            {
                throw new MaterialException.MaterialListNotFoundException();
            }

            Console.WriteLine("\nLista de materiales disponibles:");

            foreach (var m in list)
            {
                MaterialPrinter.Print(m);
            }
            Console.WriteLine("\nPresiona una tecla para regresar al menú...");
            Console.ReadKey();
        }

        public void SearchAllMaterials()
        {
            var filter = MaterialInput.GetMaterialFilter();
            var list = _materialDao.SearchAllMaterials(filter);
            if (list.Count == 0)
            {
                throw new MaterialException.MaterialFilterNotFoundException();
            }
            else
            {
                foreach (var m in list)
                {
                    MaterialPrinter.Print(m);
                    Console.WriteLine();
                }
            }
            Console.WriteLine("\nPresiona una tecla para regresar al menú...");
            Console.ReadKey();
        }

        public void SearchMaterial()
        {
            var id = MaterialInput.GetMaterialId();
            var material = _materialDao.SearchMaterial(id.ToString());
            if (material == null)
            {
                throw new MaterialException.MaterialNotFoundException(id);
            }
            else
            {
                MaterialPrinter.Print(material);
            }
            Console.WriteLine("\nPresiona una tecla para continuar...");
            Console.ReadKey();
        }

        public void CheckAvailableMaterial()
        {
            var id = MaterialInput.GetMaterialId();
            bool ok = _materialDao.IsMaterialAvailable(id);
            if (!ok) throw new MaterialException.MaterialUnavailableException(id);
            Console.WriteLine("Material disponible.");
        }

        public void CreateMaterial()
        {
            int input = MaterialInput.GetMaterialFromInput();
            Console.Clear();

            switch (input)
            {
                case 1:
                    var bookDto = MaterialInput.GetBookFromInput();
                    ValidateMaterial(bookDto);
                    _materialDao.CreateMaterial(bookDto);
                    Console.WriteLine("Libro creado exitosamente.");
                    break;

                case 2:
                    var audiovisualDto = MaterialInput.GetAudioVisualFromInput();
                    ValidateMaterial(audiovisualDto);
                    _materialDao.CreateMaterial(audiovisualDto);
                    Console.WriteLine("Material audiovisual creado exitosamente.");
                    break;

                default:
                    Console.WriteLine("Opción inválida. Intente de nuevo.");
                    return;
            }

            Console.WriteLine("Presiona una tecla para continuar...");
            Console.ReadKey();
        }

        public void UpdateMaterial()
        {
            int materialId = MaterialInput.GetMaterialId();
            var material = _materialDao.SearchMaterial(materialId.ToString());

            if (material is null)
            {
                throw new MaterialException.MaterialNotFoundException(materialId.ToString());
            }

            if (material is BookDTO book)
            {
                var updatedBook = MaterialInput.GetBookUpdateInput(book);
                _materialDao.UpdateMaterial(updatedBook);
            }
            if (material is AudioVisualDTO av)
            {
                var updatedAV = MaterialInput.GetAudioVisualUpdateInput(av);
                _materialDao.UpdateMaterial(updatedAV);
            }

            Console.WriteLine("Material actualizado correctamente.");
            Console.WriteLine("Presiona una tecla para continuar...");
            Console.ReadKey();
        }


        public void UpdateMaterialStatus(int id, MaterialStatus status)
        {
            _materialDao.UpdateMaterialStatus(id, status);
        }

        public void DeleteMaterial()
        {
            var id = MaterialInput.GetMaterialId();
            var material = _materialDao.SearchMaterial(id.ToString());

            if (material is null)
            {
                throw new UserException.UserNotFoundException(id.ToString());
            }
            else if (MaterialInput.ConfirmAction(material.Title)) {
                _materialDao.DeleteMaterial(id);
                Console.WriteLine("Material eliminado satisfactoriamente.");
            }
            else
            {
                Console.WriteLine("\nOperación cancelada.");
            }
            Console.WriteLine("Presiona una tecla para continuar...");
            Console.ReadKey();
        }

        private void ValidateMaterial(MaterialDTO m)
        {
            if (string.IsNullOrWhiteSpace(m.Title))
                throw new MaterialException.InvalidMaterialDataException("Título vacío");
            if (!MaterialTopic.TryParse(m.MaterialTopic.ToString(), out _))
                throw new MaterialException.InvalidMaterialTopicException(m.MaterialTopic.ToString());
        }
    }
}
