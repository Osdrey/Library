using Library.Application.DTOs;

namespace Library.Presentation.UI.Printers
{
    internal static class BookPrinter
    {
        public static void Print(BookDTO book)
        {
            Console.WriteLine("\n----- Detalles del Libro -----\n");
            Console.WriteLine($"ID del material: {book.MaterialId}");
            Console.WriteLine($"Título: {book.Title}");
            Console.WriteLine($"Autor: {book.Author}");
            Console.WriteLine($"Año de publicación: {book.PublicationYear}");
            Console.WriteLine($"Estado del material: {book.MaterialStatus}");
            Console.WriteLine($"Condición del material: {book.MaterialCondition}");
            Console.WriteLine($"Tema: {book.MaterialTopic}");
            Console.WriteLine($"Número de páginas: {book.Pages}");
        }
    }

}
