using System;
using System.Data;
using System.Linq;

namespace CodeSmells.Bloaters.LongParameterList
{
    public class Advertiser
    {
        public Advertiser(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }
        public string Name { get; set; }
    }
    public class SummaryCampaignActivity
    {

        protected void ProcessData(DataTable advertiserActivity)
        {
            var advertiser = new Advertiser(123);
            int previousPlacementId = 0;
            foreach (DataRow dr in advertiserActivity.Rows)
            {
                var placementId = Int32.Parse(dr["Placement ID"].ToString());
                if (placementId == previousPlacementId) continue; // skip duplicates
                previousPlacementId = placementId;
                var campaignId = Int32.Parse(dr["Campaign ID"].ToString());
                string campaignName = dr["Campaign Name"].ToString();
                string placementName = dr["Placement Name"].ToString();
                var placementStartDate = DateTime.Parse(dr["Placement Start Date"].ToString());
                var placementEndDate = DateTime.Parse(dr["Placement End Date"].ToString());
                var unitsBought = Int32.Parse(dr["Units Bought"].ToString());
                var impressionsDeliveredReportPeriod = Int32.Parse(dr["Placement Impressions"].ToString());
                var clicksDeliveredReportPeriod = Int32.Parse(dr["Placement Clicks"].ToString());
                var impressionsDeliveredTotal = Int32.Parse(dr["Total Impressions"].ToString());
                var clicksDeliveredTotal = Int32.Parse(dr["Total Clicks"].ToString());
                var weight = Int32.Parse(dr["Weight"].ToString());
                AddSummaryRow(advertiser.Id, advertiser.Name,
                              campaignId, campaignName,
                              placementId, placementName,
                              placementStartDate, placementEndDate,
                              unitsBought, "Impressions",
                              impressionsDeliveredReportPeriod, clicksDeliveredReportPeriod,
                              impressionsDeliveredTotal, clicksDeliveredTotal, weight);
            }

        }

        protected void AddSummaryRow(
            int advertiserId,
            string advertiserName,
            int campaignId,
            string campaignName,
            int placementId,
            string placementName,
            DateTime placementStartDate,
            DateTime placementEndDate,
            int unitsBought,
            string unitName,
            int impressionsDeliveredReportPeriod,
            int clicksDeliveredReportPeriod,
            int impressionsDeliveredTotal,
            int clicksDeliveredTotal,
            int weight)
        {
            // call a stored procedure with these values
        }
    }
}