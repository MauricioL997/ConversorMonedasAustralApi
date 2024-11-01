public class ConversionDto
{
    public int ConversionId { get; set; }
    public required int UserId { get; set; }
    public required string FromCurrency { get; set; }
    public required string ToCurrency { get; set; }
    public required decimal Amount { get; set; }
    public decimal Result { get; set; }
    public DateTime Date { get; set; }
}
