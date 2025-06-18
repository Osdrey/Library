using Library.Application.Interfaces;
using Library.Presentation.Helpers;

namespace Library.Presentation.UI.Menus
{
    internal class MaterialUI
    {
        private readonly IMaterialService _materialService;

        public MaterialUI(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        public void ShowMenu()
        {
            bool flagMenu = true;
            while (flagMenu)
            {
                Console.Clear();
                Console.WriteLine(  "╔════════════════════════════════╗\n" +
                                    "║      GESTOR DE MATERIALES      ║\n" +
                                    "╚════════════════════════════════╝\n" +
                "\n¿Qué acción vas a realizar? Ingresa una de las opciones disponibles:\n");
                Console.WriteLine(
                    "1. Crear material\n" +
                    "2. Buscar material\n" +
                    "3. Actualizar material\n" +
                    "4. Eliminar material\n" +
                    "5. Ver materiales disponibles\n" +
                    "6. Filtrar materiales (Titulo, Autor, Año)\n" +
                    "9. Volver al menú anterior\n" +
                    "0. Salir\n");

                var input = Console.ReadLine();
                Console.Clear();

                try
                {
                    switch (input)
                    {
                        case "1":
                            _materialService.CreateMaterial();
                            break;
                        case "2":
                            _materialService.SearchMaterial();
                            break;
                        case "3":
                            _materialService.UpdateMaterial();
                            break;
                        case "4":
                            _materialService.DeleteMaterial();
                            break;
                        case "5":
                            _materialService.ViewAvailableMaterials();
                            break;
                        case "6":
                            _materialService.SearchAllMaterials();
                            break;
                        case "9":
                            Console.WriteLine("Regresando al menú anterior...");
                            Thread.Sleep(1000);
                            Console.Clear();
                            flagMenu = false;
                            break;
                        case "0":
                            Console.WriteLine("Saliendo del sistema...");
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Opción inválida. Intente de nuevo.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHandler.MaterialHandle(ex);
                    Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }
    }
}
