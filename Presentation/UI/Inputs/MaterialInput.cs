using Library.Application.DTOs;
using Library.Domain.Enumerations;
using Library.Domain.Structures;
using static Library.Application.Validations.ValidationHelper.Validations;

namespace Library.Presentation.UI.Inputs
{
    internal static class MaterialInput
    {
        public static int GetMaterialFromInput()
        {
            while (true)
            {
                Console.WriteLine("Seleccione tipo de material:");
                Console.WriteLine("1. Libro\n2. Audiovisual");

                string? input = Console.ReadLine();

                if (input == "1" || input == "2")
                {
                    return int.Parse(input);
                }
                else
                {
                    Console.WriteLine("Opción inválida. Por favor intente de nuevo.");
                }
            }
        }

        public static BookDTO GetBookFromInput()
        {
            Console.WriteLine(
                "╔══════════════════════════════╗\n" +
                "║     REGISTRO DE LIBRO        ║\n" +
                "╚══════════════════════════════╝\n");

            string title = TextValidate(Request("Ingrese el título:"), "Título");
            string author = TextValidate(Request("Ingrese el autor:"), "Autor");
            int publicationYear = IntegerValidate(Request("Año de publicación:"), "Año de publicación", min: 1000, max: DateTime.Now.Year);
            MaterialTopic topic = SelectTopic();
            int pages = IntegerValidate(Request("Número de páginas:"), "Páginas", min: 1);

            return new BookDTO
            {
                Title = title,
                Author = author,
                PublicationYear = publicationYear,
                MaterialStatus = MaterialStatus.Available,
                MaterialCondition = MaterialCondition.New,
                MaterialTopic = topic,
                Pages = pages
            };
        }

        public static AudioVisualDTO GetAudioVisualFromInput()
        {
            Console.WriteLine(
                "╔═══════════════════════════════╗\n" +
                "║    REGISTRO DE AUDIOVISUAL    ║\n" +
                "╚═══════════════════════════════╝\n");

            string title = TextValidate(Request("Ingrese el título:"), "Título");
            string author = TextValidate(Request("Ingrese el autor:"), "Autor");
            int publicationYear = IntegerValidate(Request("Año de publicación:"), "Año de publicación", min: 1000, max: DateTime.Now.Year);
            MaterialTopic topic = SelectTopic();
            string format = TextValidate(Request("Formato del material (DVD, Blu-Ray, etc.):"), "Formato");
            string duration = TextValidate(Request("Duración del material (ej. 1h 30m):"), "Duración");

            return new AudioVisualDTO
            {
                Title = title,
                Author = author,
                PublicationYear = publicationYear,
                MaterialStatus = MaterialStatus.Available,
                MaterialCondition = MaterialCondition.New,
                MaterialTopic = topic,
                Format = format,
                Duration = duration
            };
        }

        public static int GetMaterialId()
        {
            return IntegerValidate(Request("Ingrese el ID del material:"), "Material ID", min: 1);
        }

        public static string GetMaterialFilter()
        {
            Console.Write("Ingresa título, autor, año o ID del material: ");
            string? input = Console.ReadLine()?.Trim();
            while (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("La entrada no puede estar vacía. Intenta de nuevo.");
                Console.Write("Ingresa título, autor, año o ID del material: ");
                input = Console.ReadLine()?.Trim();
            }
            return input;
        }

        public static BookDTO GetBookUpdateInput(BookDTO book)
        {
            Console.WriteLine(
                "╔════════════════════════════════╗\n" +
                "║    ACTUALIZACIÓN DE LIBRO      ║\n" +
                "╚════════════════════════════════╝\n");

            string inputTitle = Request($"Título actual: {book.Title}\nNuevo título (Enter para omitir):");
            string newTitle = string.IsNullOrWhiteSpace(inputTitle) ? book.Title : TextValidate(inputTitle, "Título");

            string inputAuthor = Request($"Autor actual: {book.Author}\nNuevo autor (Enter para omitir):");
            string newAuthor = string.IsNullOrWhiteSpace(inputAuthor) ? book.Author : TextValidate(inputAuthor, "Autor");

            string inputYear = Request($"Año de publicación actual: {book.PublicationYear}\nNuevo año (Enter para omitir):");
            int newYear = string.IsNullOrWhiteSpace(inputYear) ? book.PublicationYear : IntegerValidate(inputYear, "Año", 1000, DateTime.Now.Year);

            Console.WriteLine($"Tema actual: {book.MaterialTopic}");
            string inputTopic = Request("Nuevo tema (Enter para omitir):");
            MaterialTopic newTopic = string.IsNullOrWhiteSpace(inputTopic)
                ? book.MaterialTopic
                : Enum.TryParse<MaterialTopic>(inputTopic, true, out var parsedTopic) ? parsedTopic : book.MaterialTopic;

            string inputPages = Request($"Páginas actuales: {book.Pages}\nNuevo número de páginas (Enter para omitir):");
            int newPages = string.IsNullOrWhiteSpace(inputPages) ? book.Pages : IntegerValidate(inputPages, "Páginas", min: 1);

            book.Title = newTitle;
            book.Author = newAuthor;
            book.PublicationYear = newYear;
            book.MaterialTopic = newTopic;
            book.Pages = newPages;

            return book;
        }

        public static AudioVisualDTO GetAudioVisualUpdateInput(AudioVisualDTO av)
        {
            Console.WriteLine(
                "╔════════════════════════════════════╗\n" +
                "║    ACTUALIZACIÓN DE AUDIOVISUAL    ║\n" +
                "╚════════════════════════════════════╝\n");

            string inputTitle = Request($"Título actual: {av.Title}\nNuevo título (Enter para omitir):");
            string newTitle = string.IsNullOrWhiteSpace(inputTitle) ? av.Title : TextValidate(inputTitle, "Título");

            string inputAuthor = Request($"Autor actual: {av.Author}\nNuevo autor (Enter para omitir):");
            string newAuthor = string.IsNullOrWhiteSpace(inputAuthor) ? av.Author : TextValidate(inputAuthor, "Autor");

            string inputYear = Request($"Año de publicación actual: {av.PublicationYear}\nNuevo año (Enter para omitir):");
            int newYear = string.IsNullOrWhiteSpace(inputYear) ? av.PublicationYear : IntegerValidate(inputYear, "Año", 1000, DateTime.Now.Year);

            Console.WriteLine($"Tema actual: {av.MaterialTopic}");
            string inputTopic = Request("Nuevo tema (Enter para omitir):");
            MaterialTopic newTopic = string.IsNullOrWhiteSpace(inputTopic)
                ? av.MaterialTopic
                : Enum.TryParse<MaterialTopic>(inputTopic, true, out var parsedTopic) ? parsedTopic : av.MaterialTopic;

            string inputFormat = Request($"Formato actual: {av.Format}\nNuevo formato (Enter para omitir):");
            string newFormat = string.IsNullOrWhiteSpace(inputFormat) ? av.Format : TextValidate(inputFormat, "Formato");

            string inputDuration = Request($"Duración actual: {av.Duration}\nNueva duración (Enter para omitir):");
            string newDuration = string.IsNullOrWhiteSpace(inputDuration) ? av.Duration : TextValidate(inputDuration, "Duración");

            av.Title = newTitle;
            av.Author = newAuthor;
            av.PublicationYear = newYear;
            av.MaterialTopic = newTopic;
            av.Format = newFormat;
            av.Duration = newDuration;

            return av;
        }

        private static MaterialTopic SelectTopic()
        {
            Console.WriteLine("Seleccione un tema:");
            var topics = MaterialTopic.GetAll();

            for (int i = 0; i < topics.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {topics[i]}");
            }

            while (true)
            {
                var input = Request("Ingrese el número correspondiente al tema:");
                if (int.TryParse(input, out int index) && index >= 1 && index <= topics.Count)
                {
                    return topics[index - 1];
                }
                Console.WriteLine("Opción inválida. Intente nuevamente.");
            }
        }

        public static bool ConfirmAction(string materialTitle)
        {
            Console.Clear();
            Console.WriteLine($"¿Confirma que desea continuar con esta acción para el material '{materialTitle}'? (s/n)");
            var respuesta = Console.ReadLine();
            return respuesta?.ToLower() == "s";
        }

        private static string Request(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine()!;
        }
    }
}
