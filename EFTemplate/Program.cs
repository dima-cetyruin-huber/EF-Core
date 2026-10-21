using System;
using BankProject.Data;
using BankProject.Demos;

namespace BankProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ЗАПУСК БАНКОВСКОЙ СИСТЕМЫ EF CORE ===");

            using (var context = new AppDbContext())
            {
                // Сброс старой базы и наполнение начальными данными
                Console.WriteLine("Пересоздание базы данных...");
                DbInitializer.ResetAndSeed(context);
                Console.WriteLine("База данных успешно обновлена!");
            }

            // Запуск демонстрационных блоков
            CrudOperations.Run();
            ChangeTrackerDemo.Run();
            RelationshipsDemo.Run();

            Console.WriteLine("\n=== ВСЕ СЦЕНАРИИ ВЫПОЛНЕНЫ УСПЕШНО ===");
        }
    }
}
