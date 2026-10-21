using System;
using System.Linq;
using BankProject.Data;
using BankProject.Models;

namespace BankProject.Demos
{
    public static class CrudOperations
    {
        public static void Run()
        {
            Console.WriteLine("\n==========================================");
            Console.WriteLine("--- БЛОК 1: CRUD ОПЕРАЦИИ (ВЫВОД ДАННЫХ) ---");
            Console.WriteLine("==========================================");

            using var db = new AppDbContext();

            // 1. CREATE
            var newBranch = new BankBranch { BranchName = "Филиал Южный", Address = "ул. Гагарина, д. 25" };
            db.BankBranches.Add(newBranch);
            db.SaveChanges();
            Console.WriteLine($"\n[C] Успешно добавлено в БД: ID={newBranch.Id}, Название='{newBranch.BranchName}', Адрес='{newBranch.Address}'");

            // 2. READ (Выводим список всех отделений, которые сейчас есть в базе)
            Console.WriteLine("\n[R] Текущий список всех отделений в базе данных:");
            var allBranches = db.BankBranches.ToList();
            foreach (var b in allBranches)
            {
                Console.WriteLine($"   -> Отделение №{b.Id}: {b.BranchName} (Адрес: {b.Address})");
            }

            // 3. UPDATE
            var branchToUpdate = db.BankBranches.Find(newBranch.Id);
            if (branchToUpdate != null)
            {
                branchToUpdate.BranchName = "Филиал Южный (Обновленный)";
                branchToUpdate.Address = "ул. Новая, д. 100";
                db.SaveChanges();

                Console.WriteLine($"\n[U] Данные после обновления в БД:");
                Console.WriteLine($"   -> Измененный объект: ID={branchToUpdate.Id}, Новый Название='{branchToUpdate.BranchName}', Новый Адрес='{branchToUpdate.Address}'");
            }

            // 4. DELETE
            if (branchToUpdate != null)
            {
                db.BankBranches.Remove(branchToUpdate);
                db.SaveChanges();
                Console.WriteLine($"\n[D] Объект с ID={branchToUpdate.Id} удален.");
            }

            // Проверка удаления
            Console.WriteLine($"\n[R] Проверка базы после удаления. Осталось отделений: {db.BankBranches.Count()}");
        }
    }
}
