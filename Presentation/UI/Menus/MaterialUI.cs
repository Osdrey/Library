using Library.Application.Interfaces;

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
                Console.WriteLine(
                    "Bienvenido al gestor de materiales.\n" +
                    "¿Qué acción vas a realizar? Ingresa una de las opciones disponibles:");
                Console.WriteLine(
                    "1. Crear material\n" +
                    "2. Buscar material\n" +
                    "3. Actualizar material\n" +
                    "4. Eliminar material\n" +
                    "5. Ver materiales disponibles\n" +
                    "9. Volver al menú anterior\n" +
                    "0. Salir");

                var input = Console.ReadLine();
                Console.Clear();

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
                        _materialService.ViewAvaraibleMaterials();
                        break;
                    case "9":
                        Console.WriteLine("Regresando al menú anterior...");
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
        }
    }
}
