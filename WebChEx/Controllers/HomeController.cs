using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using WebChEx.Models;

namespace WebChEx.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ExModel mdl = new ExModel();
            return View(mdl);
        }
        [HttpPost]
        public async Task<IActionResult> Index(ExModel mdl)
        {
            string res = String.Empty;
            var selDate = mdl.ExDate; // "161109";
            var urlCon = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?valcode=usd&json&date=" + selDate;
            using (HttpClient hGet = new HttpClient())
            {
                var msgGet =  hGet.GetAsync(urlCon).Result;
                res = await msgGet.Content.ReadAsStringAsync();
            }

            dynamic jresarr = JArray.Parse(res);
            dynamic curRow = jresarr[0];
            string curRate = curRow.rate;
            double dsum = 0;
            double rate = 8;
            string sTotals = String.Empty;
            string sNewLine = String.Empty;
            string sOldHistory = String.Empty;
            string tmplHistory = "{0} | rate: {1} | {2}uah -> <b>{3}</b><br />";
            if (Double.TryParse(curRate.Replace(',', '.'), NumberStyles.Number, CultureInfo.InvariantCulture, out rate))
            {
                ViewData["exRate"] = curRate;
                      if (Double.TryParse(mdl.ExSum.Replace(',', '.'), NumberStyles.Number, CultureInfo.InvariantCulture, out dsum))
                      {
                            sTotals = (dsum / rate).ToString("C2", new CultureInfo("en-US"));
                            sNewLine = String.Format(tmplHistory, mdl.ExRealDate, curRate, mdl.ExSum, sTotals);
                      }
                      else
                      {
                            sTotals = "something wrong: " + mdl.ExSum;
                      }               
            }
            else
            {
                ViewData["exRate"] = "wrong exchange rate: " + curRate;
            }
            ViewData["exTotal"] = sTotals;            
            byte[] baHistory;
            if (HttpContext.Session.TryGetValue("HistoryList", out baHistory))
            {
                sOldHistory = HttpContext.Session.GetString("HistoryList");              
            }
            HttpContext.Session.SetString("HistoryList", sNewLine + sOldHistory);
            ViewData["HistoryList"] = sNewLine + sOldHistory;
            return View(mdl);
        }

    
        public IActionResult Error()
        {
            return View();
        }
    }
}
