using System;
namespace BANK.Model
{
    public class ClientCSVRow
    {
        public string userId { get; set; }
        public DateTime dueDate { get; set; }

        public ClientCSVRow(string userId, DateTime dueDate)
        {
            this.userId = userId;
            this.dueDate = dueDate;
        }
    }
}

