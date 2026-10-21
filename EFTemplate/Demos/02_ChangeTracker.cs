using System;
using Microsoft.EntityFrameworkCore;
using BankProject.Data;
using BankProject.Models;

namespace BankProject.Demos
{
    public static class ChangeTrackerDemo
    {
        public static void Run()
        {
            Console.WriteLine("\n==========================================");
            Console.WriteLine("--- БЛОК 2: CHANGE TRACKER ---");
            Console.WriteLine("==========================================");
            using var db = new AppDbContext();

            // Создаем объект в памяти (Состояние: Detached)
            var branch = new BankBranch { BranchName = "Инвестиционный Офис", Address = "Пресненская наб., д. 12" };
            Console.WriteLine($"1. До добавления: {db.Entry(branch).State}");

            // Добавляем в контекст (Состояние: Added)
            db.BankBranches.Add(branch);
            Console.WriteLine($"2. После Add(): {db.Entry(branch).State}");

            // Сохраняем в БД (Состояние: Unchanged)
            db.SaveChanges();
            Console.WriteLine($"3. После SaveChanges(): {db.Entry(branch).State}");

            // Изменяем поле (Состояние: Modified)
            branch.Address = "Новый адрес, д. 1";
            Console.WriteLine($"4. После изменения свойства: {db.Entry(branch).State}");
        }
    }
}
