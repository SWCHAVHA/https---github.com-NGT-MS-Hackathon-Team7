using System.Data;
namespace Penalty_Calculation1.Models
{
    public class HelperForBindingData
    {
        public List<ReportModel> BindReportData(DataTable table)
        {
            List<ReportModel> reportModels = new List<ReportModel>();
            try{        
                foreach(DataRow row in table.Rows)
                {
                    ReportModel reportModel = new ReportModel();
                    reportModel.PlaceOfHoldingTechNumber = Convert.ToInt16(row["place_of_holding_tech_number"].ToString());
                    reportModel.ISIN = row["isin"].ToString(); 
                    reportModel.SecurityQuantity =  Convert.ToInt32(row["security_quantity"].ToString());
                    reportModel.TransactionTypeCode =  row["transaction_type_code"].ToString();
                    reportModel.InstructionTypeCode =  row["instruction_type_code"].ToString();
                    reportModel.MatchingReference = row["matching_reference"].ToString();
                    reportModel.SettlementDate= Convert.ToDateTime( row["settlement_date"].ToString());
                    reportModel.SettlementCashAmount = Convert.ToDecimal( row["settlement_cash_amount"].ToString());
                    reportModel.CalendarId =  row["calendar_id"].ToString();
                    reportModel.PartyId=  row["party_id"].ToString();
                    reportModel.CounterPartyId=  row["counter_party_id"].ToString();
                    reportModel.PartyRoleCode=  row["party_role_cd"].ToString();
                    reportModel.CounterPartyRoleCode=  row["counter_party_role_cd"].ToString();
                    reportModel.FailingPartyRoleCode=  row["failing_party_role_cd"].ToString();
                    reportModel.PenaltyAmount=  Convert.ToDecimal(row["penalty_amount"].ToString());
                    reportModel.Sign=  row["sign"].ToString();
                    reportModels.Add(reportModel);  
                }
            }
            catch(Exception)
            {
                return new List<ReportModel>();
            }
            return reportModels;
        }
    }
}