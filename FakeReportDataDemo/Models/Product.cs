using Dapper.Contrib.Extensions;

namespace FakeReportDataDemo.Models {
    [Table("DimProduct")]
    internal class Product {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
