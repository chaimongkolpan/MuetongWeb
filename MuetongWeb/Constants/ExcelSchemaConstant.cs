namespace MuetongWeb.Constants
{
    public static class ExcelSchemaConstant
    {
        public static ExcelDataSchema Customer = new ExcelDataSchema()
        {
            SheetName = "Customer",
            Datas = new List<ExcelData> {
                new ExcelData() { Name = "Id", DataType = typeof(long) },
                new ExcelData() { Name = "Name", DataType = typeof(string) },
                new ExcelData() { Name = "Detail", DataType = typeof(string) },
                new ExcelData() { Name = "Address", DataType = typeof(string) },
                new ExcelData() { Name = "ProvinceId", DataType = typeof(long?) },
                new ExcelData() { Name = "PhoneNo", DataType = typeof(string) },
                new ExcelData() { Name = "Email", DataType = typeof(string) },
                new ExcelData() { Name = "TaxNo", DataType = typeof(string) },
                new ExcelData() { Name = "BranchNo", DataType = typeof(string) }
            }
        };
        public static ExcelDataSchema Project = new ExcelDataSchema()
        {
            SheetName = "Project",
            Datas = new List<ExcelData> {
                new ExcelData() { Name = "Id", DataType = typeof(long) },
                new ExcelData() { Name = "Name", DataType = typeof(string) },
                new ExcelData() { Name = "ContractNo", DataType = typeof(string) },
                new ExcelData() { Name = "Address", DataType = typeof(string) },
                new ExcelData() { Name = "ProvinceId", DataType = typeof(long?) },
                new ExcelData() { Name = "CustomerId", DataType = typeof(long) }
            }
        };
        public static ExcelDataSchema ProjectCode = new ExcelDataSchema()
        {
            SheetName = "ProjectCode",
            Datas = new List<ExcelData> {
                new ExcelData() { Name = "Code", DataType = typeof(string) },
                new ExcelData() { Name = "Detail", DataType = typeof(string) },
                new ExcelData() { Name = "Budjet", DataType = typeof(decimal?) },
                new ExcelData() { Name = "Cost", DataType = typeof(decimal?) },
                new ExcelData() { Name = "ProjectId", DataType = typeof(long) }
            }
        };
        public static ExcelDataSchema Contractor = new ExcelDataSchema()
        {
            SheetName = "Contractor",
            Datas = new List<ExcelData> {
                new ExcelData() { Name = "Id", DataType = typeof(long) },
                new ExcelData() { Name = "Name", DataType = typeof(string) },
                new ExcelData() { Name = "Address", DataType = typeof(string) },
                new ExcelData() { Name = "PhoneNo", DataType = typeof(string) },
                new ExcelData() { Name = "Email", DataType = typeof(string) },
                new ExcelData() { Name = "ProvinceId", DataType = typeof(long?) },
                new ExcelData() { Name = "TaxNo", DataType = typeof(string) },
                new ExcelData() { Name = "DirectorName", DataType = typeof(string) },
                new ExcelData() { Name = "Type", DataType = typeof(string) }
            }
        };
        public static ExcelDataSchema ProjectContractor = new ExcelDataSchema()
        {
            SheetName = "ProjectContractor",
            Datas = new List<ExcelData> {
                new ExcelData() { Name = "ProjectId", DataType = typeof(long) },
                new ExcelData() { Name = "ContractorId", DataType = typeof(long) }
            }
        };
        public static ExcelDataSchema Store = new ExcelDataSchema()
        {
            SheetName = "Store",
            Datas = new List<ExcelData> {
                new ExcelData() { Name = "Id", DataType = typeof(long) },
                new ExcelData() { Name = "Name", DataType = typeof(string) },
                new ExcelData() { Name = "Address", DataType = typeof(string) },
                new ExcelData() { Name = "ProvinceId", DataType = typeof(long?) },
                new ExcelData() { Name = "PhoneNo", DataType = typeof(string) },
                new ExcelData() { Name = "Email", DataType = typeof(string) },
                new ExcelData() { Name = "TaxNo", DataType = typeof(string) },
                new ExcelData() { Name = "ContractName", DataType = typeof(string) }
            }
        };
        public static ExcelDataSchema Payment = new ExcelDataSchema()
        {
            SheetName = "Payment",
            Datas = new List<ExcelData> {
                new ExcelData() { Name = "StoreId", DataType = typeof(long) },
                new ExcelData() { Name = "AccountNo", DataType = typeof(string) },
                new ExcelData() { Name = "AccountName", DataType = typeof(string) },
                new ExcelData() { Name = "Bank", DataType = typeof(string) },
                new ExcelData() { Name = "Type", DataType = typeof(string) }
            }
        };
        public static ExcelDataSchema Product = new ExcelDataSchema()
        {
            SheetName = "Product",
            Datas = new List<ExcelData> {
                new ExcelData() { Name = "Name", DataType = typeof(string) },
                new ExcelData() { Name = "Unit", DataType = typeof(string) }
            }
        };
    }
}
