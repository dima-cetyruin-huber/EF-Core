using System.Collections.Generic;

namespace BankProject.Models
{
    // Отделение банка (Одна группа для множества клиентов)
    public class BankBranch
    {
        public int Id { get; set; }
        public string BranchName { get; set; } = null!;
        public string Address { get; set; } = null!;

        // Навигационное свойство: один ко многим
        public List<BankCustomer> Customers { get; set; } = new();
    }
}