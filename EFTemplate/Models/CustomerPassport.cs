namespace BankProject.Models
{
    // Паспорт клиента (Связь один-к-одному)
    public class CustomerPassport
    {
        public int Id { get; set; }
        public string PassportNumber { get; set; } = null!;
        public string IssueDate { get; set; } = null!;

        // Внешний ключ и навигационное свойство на Клиента
        public int BankCustomerId { get; set; }
        public BankCustomer BankCustomer { get; set; } = null!;
    }
}
