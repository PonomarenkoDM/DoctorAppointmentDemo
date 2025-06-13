using MyDoctorAppointment.Domain.Enums;
using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Service.Services;

class Program
{
    static void Main()
    {
        Console.WriteLine("Оберіть формат збереження:");
        Console.WriteLine("1 - JSON");
        Console.WriteLine("2 - XML");
        Console.Write("Ваш вибір: ");

        var key = Console.ReadKey().KeyChar;
        Console.WriteLine();

        BaseTypes type = key switch
        {
            '1' => BaseTypes.Json,
            '2' => BaseTypes.XML,
            _ => BaseTypes.Json
        };

        var doctorService = new DoctorService(type);

        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine($"Поточний формат збереження: {type}");
            Console.WriteLine("Оберіть дію:");
            Console.WriteLine("1 - Показати всіх лікарів");
            Console.WriteLine("2 - Додати лікаря");
            Console.WriteLine("3 - Видалити лікаря");
            Console.WriteLine("4 - Вийти");

            Console.Write("Ваш вибір: ");
            var action = Console.ReadKey().KeyChar;
            Console.WriteLine();

            switch (action)
            {
                case '1':
                    var doctors = doctorService.GetAll();
                    Console.WriteLine("Список лікарів:");
                    foreach (var doc in doctors)
                    {
                        Console.WriteLine($"{doc.Id}: {doc.Name} {doc.Surname} ({doc.DoctorType})");
                    }
                    break;

                case '2':
                    Console.Write("Ім'я: ");
                    var name = Console.ReadLine();
                    Console.Write("Прізвище: ");
                    var surname = Console.ReadLine();

                    Console.WriteLine("Оберіть тип лікаря:");
                    foreach (var value in Enum.GetValues(typeof(DoctorTypes)))
                    {
                        Console.WriteLine($"{(int)value} - {value}");
                    }

                    Console.Write("Ваш вибір: ");
                    int typeChoice = int.TryParse(Console.ReadLine(), out var val) ? val : 1;
                    DoctorTypes selectedType = Enum.IsDefined(typeof(DoctorTypes), typeChoice)
                        ? (DoctorTypes)typeChoice
                        : DoctorTypes.FamilyDoctor;

                    var newDoctor = new Doctor
                    {
                        Name = name ?? "",
                        Surname = surname ?? "",
                        Experience = 0,
                        Salary = 0,
                        DoctorType = selectedType
                    };

                    var created = doctorService.Create(newDoctor);
                    Console.WriteLine($"Лікаря додано з ID: {created.Id}");
                    break;

                case '3':
                    Console.Write("Введіть ID лікаря для видалення: ");
                    if (int.TryParse(Console.ReadLine(), out int idToDelete))
                    {
                        bool deleted = doctorService.Delete(idToDelete);
                        Console.WriteLine(deleted ? "Успішно видалено." : "Лікаря не знайдено.");
                    }
                    else
                    {
                        Console.WriteLine("Невірний ID.");
                    }
                    break;

                case '4':
                    exit = true;
                    continue;

                default:
                    Console.WriteLine("Невідома дія.");
                    break;
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу для продовження...");
            Console.ReadKey();
        }
    }
}
