using Dapper.Contrib.Extensions;

namespace FakeReportDataDemo.Models {
    [Table("DimCustomer")]
    internal class Customer {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
