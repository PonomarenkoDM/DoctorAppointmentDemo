using System;
using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Service.Interfaces;
using MyDoctorAppointment.Service.Services;

namespace MyDoctorAppointment
{
    public class DoctorAppointment
    {
        private readonly IDoctorService _doctorService;

        public DoctorAppointment()
        {
            _doctorService = new DoctorService();
        }

        public void Menu()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== Меню ===");
                Console.WriteLine("1. Показати список лікарів");
                Console.WriteLine("2. Додати лікаря");
                Console.WriteLine("3. Вийти");
                Console.Write("Оберіть опцію: ");

                if (Enum.TryParse<Domain.Enums.MenuOption>(Console.ReadLine(), out Domain.Enums.MenuOption choice))
                {
                    switch (choice)
                    {
                        case Domain.Enums.MenuOption.ShowDoctors:
                            ShowDoctors();
                            break;

                        case Domain.Enums.MenuOption.AddDoctor:
                            AddDoctor();
                            break;

                        case Domain.Enums.MenuOption.Exit:
                            running = false;
                            Console.WriteLine("Вихід...");
                            break;

                        default:
                            Console.WriteLine("Невідома опція. Спробуйте ще раз.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Невірний вибір. Введіть число від 1 до 3.");
                }

                Console.WriteLine("\nНатисніть будь-яку клавішу для продовження...");
                Console.ReadKey();
            }
        }

        private void ShowDoctors()
        {
            Console.WriteLine("\nСписок лікарів:");
            var docs = _doctorService.GetAll();
               foreach (var doc in docs)
                {
                    Console.WriteLine($"Ім'я: {doc.Name}, Прізвище: {doc.Surname}, Досвід: {doc.Experience} років, Тип: {doc.DoctorType}");
                }
        }

        private void AddDoctor()
        {
            Console.WriteLine("\nДодавання нового лікаря:");

            Console.Write("Введіть ім'я: ");
            string name = Console.ReadLine();

            Console.Write("Введіть прізвище: ");
            string surname = Console.ReadLine();

            Console.Write("Введіть досвід (років): ");
            if (!int.TryParse(Console.ReadLine(), out int experience))
            {
                Console.WriteLine("Некоректний ввід! Досвід буде встановлений на 0.");
                experience = 0;
            }

            Console.WriteLine("Виберіть тип лікаря:");
            foreach (var type in Enum.GetValues(typeof(Domain.Enums.DoctorTypes)))
            {
                Console.WriteLine($"{(int)type}. {type}");
            }
            Console.Write("Ваш вибір: ");

            if (Enum.TryParse<Domain.Enums.DoctorTypes>(Console.ReadLine(), out var doctorType))
            {
                var newDoctor = new Doctor
                {
                    Name = name,
                    Surname = surname,
                    Experience = (byte)experience,
                    DoctorType = doctorType
                };

                _doctorService.Create(newDoctor);
                Console.WriteLine("Лікар успішно доданий!");
            }
            else
            {
                Console.WriteLine("Некоректний вибір типу лікаря. Додавання скасовано.");
            }
        }
    }

    public static class Program
    {
        public static void Main()
        {
            var doctorAppointment = new DoctorAppointment();
            doctorAppointment.Menu();
        }
    }
}
