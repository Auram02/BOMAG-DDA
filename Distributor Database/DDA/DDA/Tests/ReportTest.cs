using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

using NUnit.Framework;

namespace DDA.Tests
{
    [TestFixture]
    public class ReportTests
    {
        [SetUp]
        public void Init()
        {
            DDA.BusinessLogic.InitializeProgram.InitializeGlobals();
        }

        [Test, Category("Tests")]
        public void GetReportTest()
        {

            List<string> stateList = new List<string>();
            stateList.Add("Illinois");
            stateList.Add("Missouri");

            DataSet ds = DDA.DataObjects.Reports.GenerateContractListByCategoryListStateList(7, stateList);

            Assert.Greater(ds.Tables[0].Rows.Count, 0);
            Assert.Greater(ds.Tables[1].Rows.Count, 0);
            com.findbomag.www.MapReportService svc = new com.findbomag.www.MapReportService();

            string reportID = svc.UploadReportData(ds, "Bomag Light", stateList.ToArray());

            Assert.Greater(reportID.Length, 0);
        }

        //[Test, Category("Tests")]
        //public void GetReportTest2()
        //{

        //    List<string> stateList = new List<string>();
        //    stateList.Add("Illinois");
        //    stateList.Add("Missouri");

        //    DataTable dt = DDA.DataObjects.Reports.GenerateContractListByCategoryListStateList(7, stateList);

        //    Assert.Greater(dt.Rows.Count, 0);

        //    Dealer_Locator.BR.MapReport mr = new Dealer_Locator.BR.MapReport();
        //    string outputJsonEmpty = mr.BuildReportData(dt);


        //}


        //[Test, Category("Tests")]
        //public void GetReportTest3()
        //{

        //    List<string> stateList = new List<string>();
        //    stateList.Add("Alabama");
        //    stateList.Add("Alaska");
        //    stateList.Add("Arizona");
        //    stateList.Add("Arkansas");
        //    stateList.Add("California");
        //    stateList.Add("Colorado");
        //    stateList.Add("Connecticut");
        //    stateList.Add("Deleware");
        //    stateList.Add("District of Columbia");
        //    stateList.Add("Florida");
        //    stateList.Add("Georgia");
        //    stateList.Add("Guam and New Pacific Area");
        //    stateList.Add("Hawaii");
        //    stateList.Add("Idaho");
        //    stateList.Add("Illinois");
        //    stateList.Add("Indiana");
        //    stateList.Add("Iowa");
        //    stateList.Add("Kansas");
        //    stateList.Add("Kentucky");
        //    stateList.Add("Louisiana");
        //    stateList.Add("Maine");
        //    stateList.Add("Maryland");
        //    stateList.Add("Massachusetts");
        //    stateList.Add("Michigan");
        //    stateList.Add("Minnesota");
        //    stateList.Add("Mississippi");
        //    stateList.Add("Missouri");
        //    stateList.Add("Montana");
        //    stateList.Add("Nebraska");
        //    stateList.Add("Nevada");
        //    stateList.Add("New Hampshire");
        //    stateList.Add("New Jersey");
        //    stateList.Add("New Mexico");
        //    stateList.Add("New York");
        //    stateList.Add("North Carolina");
        //    stateList.Add("North Dakota");
        //    stateList.Add("Ohio");
        //    stateList.Add("Oklahoma");
        //    stateList.Add("Oregon");
        //    stateList.Add("Pennsylvania");
        //    stateList.Add("Rhode Island");
        //    stateList.Add("South Carolina");
        //    stateList.Add("South Dakota");
        //    stateList.Add("Tennessee");
        //    stateList.Add("Texas");
        //    stateList.Add("Utah");
        //    stateList.Add("Vermont");
        //    stateList.Add("Virginia");
        //    stateList.Add("Washington");
        //    stateList.Add("West Virginia");
        //    stateList.Add("Wisconsin");
        //    stateList.Add("Wyoming");

        //    DataTable dt = DDA.DataObjects.Reports.GenerateContractListByCategoryListStateList(15, stateList);

        //    Assert.Greater(dt.Rows.Count, 0);

        //    Dealer_Locator.BR.MapReport mr = new Dealer_Locator.BR.MapReport();
        //    string outputJsonEmpty = mr.BuildReportData(dt);


        //}

        [Test, Category("Tests")]
        public void GetReportByContractNumberTest()
        {

            string contractNumber = "AIR982006-586";

            DataTable dtCategories = DDA.DataAccess.Contract_da.GetContractCategories(contractNumber, true).Tables[0];

            foreach (DataRow dr in dtCategories.Rows)
            {
                int categoryID = Convert.ToInt32(dr["CategoryID"].ToString());
                DataSet ds = DDA.DataObjects.Reports.GetContractDataByContractNumberCategoryID(contractNumber, categoryID);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Assert.Greater(ds.Tables[0].Rows.Count, 0);
                    Assert.Greater(ds.Tables[1].Rows.Count, 0);
                    com.findbomag.www.MapReportService svc = new com.findbomag.www.MapReportService();

                    System.Net.WebProxy proxy = new System.Net.WebProxy();

                    proxy.UseDefaultCredentials = true;

                    List<string> stateList = new List<string>();
                    stateList.Add("Illinois");

                    string reportID = svc.UploadReportData(ds, "Bomag Light", stateList.ToArray());

                    Assert.Greater(reportID.Length, 0);

                    reportID = reportID.Substring(0, reportID.IndexOf(".json"));

                    System.Diagnostics.Process.Start("http://www.findbomag.com/admin/Reports/MapReport.aspx?id=" + reportID);
                }
            }

        }
        //[Test, Category("Tests")]
        //public void GetReportByContractNumberTest2()
        //{

        //    string contractNumber = "AIR982006-586";


        //    DataTable dtCategories = DDA.DataAccess.Contract_da.GetContractCategories(contractNumber, true).Tables[0];

        //    Dealer_Locator.BR.MapReport mr = new Dealer_Locator.BR.MapReport();

        //    foreach (DataRow dr in dtCategories.Rows)
        //    {
        //        int categoryID = Convert.ToInt32(dr["CategoryID"].ToString());
        //        DataTable dt = DDA.DataObjects.Reports.GetContractDataByContractNumberCategoryID(contractNumber, categoryID);

        //        Assert.Greater(dt.Rows.Count, 0);

        //        string outputJsonEmpty = mr.BuildReportData(dt);

        //    }
            
        //}
    }
}
