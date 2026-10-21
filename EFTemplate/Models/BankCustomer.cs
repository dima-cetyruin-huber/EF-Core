namespace BankProject.Models
{
    public class BankCustomer
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;

        // Связь один-ко-многим: ссылка на отделение банка
        public int BankBranchId { get; set; }
        public BankBranch BankBranch { get; set; } = null!;

        // Связь один-к-одному: ссылка на паспорт (навигационное свойство)
        public CustomerPassport? Passport { get; set; }
    }
}
