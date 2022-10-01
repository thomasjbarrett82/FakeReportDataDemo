using Dapper.Contrib.Extensions;

namespace FakeReportDataDemo.Models {
    [Table("FactTransaction")]
    internal class Transaction {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public DateTime DateTimeSold { get; set; }
        public int QuantitySold { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
