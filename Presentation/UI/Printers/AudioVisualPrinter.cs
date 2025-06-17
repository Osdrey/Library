using Library.Application.DTOs;

namespace Library.Presentation.UI.Printers
{
    internal static class AudioVisualPrinter
    {
        public static void Print(AudioVisualDTO av)
        {
            Console.WriteLine("\n══════════════════════════════════════════════════\n");
            Console.WriteLine($"Tipo material: Audiovisual");
            Console.WriteLine($"ID del material: {av.MaterialId}");
            Console.WriteLine($"Título: {av.Title}");
            Console.WriteLine($"Autor: {av.Author}");
            Console.WriteLine($"Año de publicación: {av.PublicationYear}");
            Console.WriteLine($"Estado del material: {av.MaterialStatus}");
            Console.WriteLine($"Condición del material: {av.MaterialCondition}");
            Console.WriteLine($"Tema: {av.MaterialTopic}");
            Console.WriteLine($"Formato: {av.Format}");
            Console.WriteLine($"Duración: {av.Duration}");
        }
    }
}
