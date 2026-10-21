using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BankProject.Models;
using System;

namespace BankProject.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<BankBranch> BankBranches { get; set; } = null!;
        public DbSet<BankCustomer> BankCustomers { get; set; } = null!;
        public DbSet<CustomerPassport> CustomerPassports { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Настройка подключения (например, SQLite для простоты)
            optionsBuilder.UseSqlite("Data Source=bank.db")
                          .LogTo(Console.WriteLine, LogLevel.Information) // Вывод SQL в консоль
                          .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка связи один-к-одному между Customer и Passport
            modelBuilder.Entity<BankCustomer>()
                .HasOne(c => c.Passport)
                .WithOne(p => p.BankCustomer)
                .HasForeignKey<CustomerPassport>(p => p.BankCustomerId)
                .OnDelete(DeleteBehavior.Cascade); // Каскадное удаление паспорта при удалении клиента
        }
    }
}
