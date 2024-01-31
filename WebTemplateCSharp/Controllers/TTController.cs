using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTemplateCSharp.Entities;

namespace WebTemplateCSharp.Controllers
{
    public class TTController : Controller
    {
        private readonly TT1DbContext _db;
        private readonly TT2DbContext _db2;
        public TTController(TT1DbContext db, TT2DbContext db2)
        {
            _db = db;
            _db2 = db2;
        }
        private async Task<List<TT1>> GetTT1()
        {
            return await _db.TT1.ToListAsync();
        }
        private async Task<List<TT2>> GetTT2ByC1(string c1)
        {
            return await _db2.TT2.Where(item => item.C1.StartsWith(c1)).ToListAsync();
        }
        [HttpGet]
        public async Task<IActionResult> LoadTT1Async()
        {
            var tt1s = await GetTT1();
            int c = 0;
            string data = "";

            data += "<thead style=\"text-align: center; \">";
            data += "<tr>";
            data += "<th> # </th>";
            data += "<th> C1 </th>";
            data += "<th> C2 </th>";
            data += "<th> C3 </th>";
            data += "<th> C4 </th>";
            data += "<th> Date Updated </th>";
            data += "</tr>";
            data += "</thead>";
            data += "<tbody id=\"t_t1_data\" style=\"text-align: center; \">";

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

            data += "</tbody>";

            return Content(data, "text/html");
        }
        [HttpGet]
        public async Task<IActionResult> LoadTT2Async([FromQuery] string c1 = "")
        {
            var tt2s = await GetTT2ByC1(c1);
            int c = 0;
            string data = "";

            data += "<thead style=\"text-align: center; \">";
            data += "<tr>";
            data += "<th> # </th>";
            data += "<th> C1 </th>";
            data += "<th> D1 </th>";
            data += "<th> D2 </th>";
            data += "<th> D3 </th>";
            data += "<th> Date Updated </th>";
            data += "</tr>";
            data += "</thead>";
            data += "<tbody id=\"t_t2_data\" style=\"text-align: center; \">";

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

            data += "</tbody>";

            return Content(data, "text/html");
        }
    }
}
