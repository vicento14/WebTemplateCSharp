using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTemplateCSharp.Entities;

namespace WebTemplateCSharp.Controllers
{
    public class TTSLMController : Controller
    {
        private readonly TT1DbContext _db;
        private readonly TT2DbContext _db2;
        public TTSLMController(TT1DbContext db, TT2DbContext db2)
        {
            _db = db;
            _db2 = db2;
        }
        private async Task<List<TT1>> GetTT1(int page_first_result = 0, int results_per_page = 10)
        {
            return await _db.TT1.Skip(page_first_result).Take(results_per_page).ToListAsync();
        }
        private async Task<List<TT2>> GetTT2ByC1(string c1, int page_first_result = 0, int results_per_page = 10)
        {
            return await _db2.TT2.Where(item => item.C1.StartsWith(c1)).Skip(page_first_result).Take(results_per_page).ToListAsync();
        }
        private async Task<int> CountTT1Data()
        {
            return await _db.TT1.CountAsync();
        }
        private async Task<int> CountTT2Data(string c1)
        {
            return await _db2.TT2.Where(item => item.C1.StartsWith(c1)).CountAsync();
        }
        [HttpGet]
        public async Task<IActionResult> CountTT1Async()
        {
            int total = await CountTT1Data();
            return Content(total.ToString(), "text/html");
        }
        [HttpGet]
        public async Task<IActionResult> CountTT2Async([FromQuery] string c1 = "")
        {
            int total = await CountTT2Data(c1);
            return Content(total.ToString(), "text/html");
        }
        [HttpGet]
        public async Task<IActionResult> LastPageTT1Async()
        {
            int results_per_page = 10;

            int number_of_result = await CountTT1Data();

            //determine the total number of pages available
            int number_of_page = (int)Math.Ceiling(Convert.ToDecimal(number_of_result) / Convert.ToDecimal(results_per_page));

            return Content(number_of_page.ToString(), "text/html");
        }
        [HttpGet]
        public async Task<IActionResult> LastPageTT2Async([FromQuery] string c1 = "")
        {
            int results_per_page = 10;

            int number_of_result = await CountTT2Data(c1);

            //determine the total number of pages available
            int number_of_page = (int)Math.Ceiling(Convert.ToDecimal(number_of_result) / Convert.ToDecimal(results_per_page));

            return Content(number_of_page.ToString(), "text/html");
        }
        [HttpGet]
        public async Task<IActionResult> LoadTT1Async([FromQuery] int current_page)
        {
            string data = "";

            int results_per_page = 10;

            //determine the sql LIMIT starting number for the results on the displaying page
            int page_first_result = (current_page - 1) * results_per_page;

            int c = page_first_result;

            var tt1s = await GetTT1(page_first_result, results_per_page);

            if (tt1s != null)
            {
                foreach (var tt1 in tt1s)
                {
                    c++;
                    string row = "<tr style=\"cursor: pointer; \" class=\"modal-trigger\" onclick=\"";
                    row += "load_t_t2(&quot;" + tt1.Id.ToString() + "~!~" + tt1.C1 + "&quot;)";
                    row += "\">";

                    row += "<td>" + c.ToString() + "</td>";
                    row += "<td>" + tt1.C1 + "</td>";
                    row += "<td>" + tt1.C2 + "</td>";
                    row += "<td>" + tt1.C3 + "</td>";
                    row += "<td>" + tt1.C4 + "</td>";
                    row += "<td>" + tt1.DateUpdated + "</td>";

                    row += "</tr>";

                    data += row;
                }
            }
            else
            {
                data = "<tr><td colspan=\"6\" style=\"text - align:center; color: red; \">No Result !!!</td></tr>";
            }

            return Content(data, "text/html");
        }
        [HttpGet]
        public async Task<IActionResult> LoadTT2Async([FromQuery] int current_page, string c1 = "")
        {
            string data = "";

            int results_per_page = 10;

            //determine the sql LIMIT starting number for the results on the displaying page
            int page_first_result = (current_page - 1) * results_per_page;

            int c = page_first_result;

            var tt2s = await GetTT2ByC1(c1, page_first_result, results_per_page);

            if (tt2s != null)
            {
                foreach (var tt2 in tt2s)
                {
                    c++;
                    string row = "<tr>";

                    row += "<td>" + c.ToString() + "</td>";
                    row += "<td>" + tt2.C1 + "</td>";
                    row += "<td>" + tt2.D1 + "</td>";
                    row += "<td>" + tt2.D2 + "</td>";
                    row += "<td>" + tt2.D3 + "</td>";
                    row += "<td>" + tt2.DateUpdated + "</td>";

                    row += "</tr>";

                    data += row;
                }
            }
            else
            {
                data = "<tr><td colspan=\"6\" style=\"text - align:center; color: red; \">No Result !!!</td></tr>";
            }

            return Content(data, "text/html");
        }
    }
}
