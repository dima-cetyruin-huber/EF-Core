using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BankProject.Data;
using BankProject.Models;

namespace BankProject.Demos
{
    public static class RelationshipsDemo
    {
        public static void Run()
        {
            Console.WriteLine("\n==========================================");
            Console.WriteLine("--- БЛОК 3: СВЯЗИ И ВЫВОД СВЯЗАННЫХ ДАННЫХ ---");
            Console.WriteLine("==========================================");

            using var db = new AppDbContext();

            // 1. Создание связанных данных
            var branch = new BankBranch { BranchName = "VIP Отделение", Address = "ул. Тверская, д. 1" };
            var customer = new BankCustomer
            {
                FullName = "Богатый Клиент Петрович",
                Passport = new CustomerPassport { PassportNumber = "7777-777777", IssueDate = "2025-01-01" }
            };
            branch.Customers.Add(customer);

            db.BankBranches.Add(branch);
            db.SaveChanges();
            Console.WriteLine("\n[+] В базу успешно загружена цепочка связанных данных!");

            // 2. READ с использованием загрузки связанных данных (.Include)
            Console.WriteLine("\n[R] Запрос данных из базы с помощью .Include():");
            var savedBranch = db.BankBranches
                .Include(b => b.Customers)
                    .ThenInclude(c => c.Passport)
                .FirstOrDefault(b => b.Id == branch.Id);

            if (savedBranch != null)
            {
                Console.WriteLine($"   Главный объект (Отделение): ID={savedBranch.Id} — {savedBranch.BranchName}");
                foreach (var cust in savedBranch.Customers)
                {
                    Console.WriteLine($"      └─ Связанный объект (Клиент): ID={cust.Id} — {cust.FullName}");
                    Console.WriteLine($"            └─ Связанный объект (Паспорт): ID={cust.Passport?.Id}, Номер: {cust.Passport?.PassportNumber}, Выдан: {cust.Passport?.IssueDate}");
                }
            }

            // 3. Каскадное удаление
            Console.WriteLine("\n[D] Удаляем клиента для проверки удаления паспорта...");
            db.BankCustomers.Remove(customer);
            db.SaveChanges();

            // Проверка
            var orphanPassport = db.CustomerPassports.FirstOrDefault(p => p.PassportNumber == "7777-777777");
            if (orphanPassport == null)
            {
                Console.WriteLine("   -> Успех: Паспорт '7777-777777' автоматически удален. В базе данных пусто.");
            }
        }
    }
}
