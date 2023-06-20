public class ReportModel
{
    public short PlaceOfHoldingTechNumber { get; set; }
    public string ISIN { get; set; }
    public int SecurityQuantity { get; set; }
    public string TransactionTypeCode { get; set; }
    public string InstructionTypeCode { get; set; }
    public string MatchingReference { get; set; }
    public DateTime SettlementDate { get; set; }
    public string PlaceOfSettlement { get; set; }
    public decimal SettlementCashAmount { get; set; }
    public string CalendarId { get; set; }
    public string PartyId { get; set; }
    public string CounterPartyId { get; set; }
    public string PartyRoleCode { get; set; }
    public string CounterPartyRoleCode { get; set; }
    public string FailingPartyRoleCode { get; set; }
    public decimal PenaltyAmount { get; set; }
    public string Sign { get; set; }
}