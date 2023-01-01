namespace MuetongWeb.Constants
{
    public class ExcelDataSchema
    {
        public string SheetName { get; set; } = null!;
        public List<ExcelData> Datas { get; set; } = new List<ExcelData>();
    }
    public class ExcelData
    {
        public string Name { get; set; } = null!;
        public Type DataType { get; set; } = null!;
    }
    public class ExcelCustomerData
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Detail { get; set; }
        public string? Address { get; set; }
        public long? ProvinceId { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }
        public string? TaxNo { get; set; }
        public string? BranchNo { get; set; }
        public long? InsertedId { get; set; }
    }
    public class ExcelProjectData
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string ContractNo { get; set; } = null!;
        public string? Address { get; set; }
        public long? ProvinceId { get; set; }
        public long CustomerId { get; set; }
        public long? InsertedId { get; set; }
    }
    public class ExcelProjectCodeData
    {
        public string Code { get; set; } = null!;
        public string? Detail { get; set; }
        public decimal? Budjet { get; set; }
        public decimal? Cost { get; set; }
        public long ProjectId { get; set; }
    }
    public class ExcelContractorData
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }
        public long? ProvinceId { get; set; }
        public string? TaxNo { get; set; }
        public string? DirectorName { get; set; }
        public string? Type { get; set; }
        public long? InsertedId { get; set; }
    }
    public class ExcelProjectContractorData
    {
        public long ProjectId { get; set; }
        public long ContractorId { get; set; }
    }
    public class ExcelStoreData
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public long? ProvinceId { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }
        public string? TaxNo { get; set; }
        public string? ContractName { get; set; }
        public long? InsertedId { get; set; }
    }
    public class ExcelPaymentData
    {
        public long StoreId { get; set; }
        public string AccountNo { get; set; } = null!;
        public string? AccountName { get; set; }
        public string? Bank { get; set; }
        public string? Type { get; set; }
    }
    public class ExcelProductData
    {
        public string Name { get; set; } = null!;
        public string Unit { get; set; } = null!;
    }
}
