using Library.Application.DTOs;

namespace Library.Presentation.UI.Printers
{
    internal static class MaterialPrinter
    {
        public static void Print(MaterialDTO material)
        {
            switch (material)
            {
                case BookDTO book:
                    BookPrinter.Print(book);
                    break;
                case AudioVisualDTO av:
                    AudioVisualPrinter.Print(av);
                    break;
                default:
                    Console.WriteLine("Tipo de material desconocido.");
                    break;
            }
        }
    }
}
