using System.Web.Mvc;
using System;
using System.Collections.Generic;
using PagedList;

namespace ActiveRiskMVC
{
    public class HomeController : Controller
    {
        RiskService rs = new RiskService();

        public ViewResult Index(string sortOrder,string currentFilter, string searchString, int? page)
        {
            var risks = rs.GetRisks();
            ViewBag.CurrentSort = sortOrder;

            ViewBag.IDSortParm = String.IsNullOrEmpty(sortOrder) ? "id" : "";
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title" : "";
            ViewBag.OwnerSortParm = String.IsNullOrEmpty(sortOrder) ? "owner" : "";
            ViewBag.RiskStatusSortParm = String.IsNullOrEmpty(sortOrder) ? "riskStatus" : "";
            ViewBag.RiskScoreSortParm = String.IsNullOrEmpty(sortOrder) ? "riskScore" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
              risks =  risks.FindAll(s => s.Title.ToUpper().Contains(searchString.ToUpper()) 
                || s.Owner.Name.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "id":
                    risks.Sort((x, y) => x.Id.CompareTo(y.Id));
                    break;
                case "title":
                    risks.Sort((x, y) => string.Compare(x.Title, y.Title));
                    break;
                case "owner":
                    risks.Sort((x, y) => string.Compare(x.Owner.Name, y.Owner.Name));
                    break;
                case "riskStatus":
                    risks.Sort((x, y) => string.Compare(x.Status.ToString(), y.Status.ToString()));
                    break;
                case "riskScore":
                    risks.Sort((x, y) => string.Compare(x.RiskScore.ToString(), y.RiskScore.ToString()));
                    break;
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(risks.ToPagedList(pageNumber,pageSize));
        }

    }
}