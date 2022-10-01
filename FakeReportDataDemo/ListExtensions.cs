using FakeReportDataDemo.Models;
using System.Data;

namespace FakeReportDataDemo {
    internal static class ListExtensions {
        public static DataTable ToDataTable(this List<Transaction> iList) {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(nameof(Transaction.Id), typeof(int));
            dataTable.Columns.Add(nameof(Transaction.CustomerId), typeof(int));
            dataTable.Columns.Add(nameof(Transaction.ProductId), typeof(int));
            dataTable.Columns.Add(nameof(Transaction.DateTimeSold), typeof(DateTime));
            dataTable.Columns.Add(nameof(Transaction.QuantitySold), typeof(int));
            dataTable.Columns.Add(nameof(Transaction.PricePerUnit), typeof(decimal));
            dataTable.Columns.Add(nameof(Transaction.TotalPrice), typeof(decimal));

            object[] values = new object[7];

            foreach (var item in iList) {
                values[0] = item.Id;
                values[1] = item.CustomerId;
                values[2] = item.ProductId;
                values[3] = item.DateTimeSold;
                values[4] = item.QuantitySold;
                values[5] = item.PricePerUnit;
                values[6] = item.TotalPrice;

                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }
}
