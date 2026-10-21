using BankProject.Models;
using System.Collections.Generic;

namespace BankProject.Data
{
    public static class DbInitializer
    {
        public static void ResetAndSeed(AppDbContext context)
        {
            // Удаляем старую БД и создаем заново с актуальной структурой
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Создаем тестовое отделение
            var centralBranch = new BankBranch
            {
                BranchName = "Центральный Офис",
                Address = "ул. Ленина, д. 10"
            };

            // Создаем клиента сразу с паспортом
            var customer1 = new BankCustomer
            {
                FullName = "Иванов Иван Иванович",
                Passport = new CustomerPassport { PassportNumber = "4510-123456", IssueDate = "2020-05-15" }
            };

            var customer2 = new BankCustomer
            {
                FullName = "Петрова Анна Сергеевна",
                Passport = new CustomerPassport { PassportNumber = "4511-654321", IssueDate = "2022-09-20" }
            };

            // Привязываем клиентов к отделению
            centralBranch.Customers.Add(customer1);
            centralBranch.Customers.Add(customer2);

            // Сохраняем в базу данных
            context.BankBranches.Add(centralBranch);
            context.SaveChanges();
        }
    }
}
