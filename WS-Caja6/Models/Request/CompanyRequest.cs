namespace WS_Caja6.Models.Request
{
    public class CompanyRequest
    {
        public int Id { get; set; }
        public string BusinessName { get; set; }
        public string TaxId { get; set; }
        public int CompanyType { get; set; }
        public string AccountNumber { get; set; }
        public string Cbu { get; set; }

    }
}
