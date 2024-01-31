using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTemplateCSharp.Entities;
using WebTemplateCSharp.Models;

namespace WebTemplateCSharp.Controllers
{
    public class UserAccountsController : Controller
    {
        private readonly UserAccountsDbContext _db;
        public UserAccountsController(UserAccountsDbContext db)
        {
            _db = db;
        }
        private async Task<List<UserAccounts>> GetUserAccounts()
        {
            return await _db.UserAccounts.ToListAsync();
        }
        private async Task<int> CountUserAccounts(string id_number, string full_name = "", string role = "")
        {
            var query = _db.UserAccounts.AsQueryable();

            if (!string.IsNullOrEmpty(id_number))
            {
                query = query.Where(item => item.IdNumber.StartsWith(id_number));
            }

            if (!string.IsNullOrEmpty(full_name))
            {
                query = query.Where(item => item.FullName.StartsWith(full_name));
            }

            if (!string.IsNullOrEmpty(role))
            {
                query = query.Where(item => item.Role == role);
            }

            return await query.CountAsync();
        }
        private async Task<UserAccounts> GetUserAccountById(int id)
        {
            return await _db.UserAccounts.FirstOrDefaultAsync(ua => ua.Id == id);
        }
        private async Task<List<UserAccounts>> GetUserAccountsSearch(string id_number, string full_name, string role)
        {
            //return await _db.UserAccounts.Where(ua => ua.id_number.Contains(id_number) && ua.full_name.Contains(full_name) && ua.role.Contains(role)).ToListAsync();
            var query = _db.UserAccounts.AsQueryable();

            if (!string.IsNullOrEmpty(id_number))
            {
                //query = query.Where(item => item.id_number.Contains(id_number));
                query = query.Where(item => item.IdNumber.StartsWith(id_number));
            }

            if (!string.IsNullOrEmpty(full_name))
            {
                //query = query.Where(item => item.full_name.Contains(full_name));
                query = query.Where(item => item.FullName.StartsWith(full_name));
            }

            if (!string.IsNullOrEmpty(role))
            {
                //query = query.Where(item => item.role.Contains(role));
                query = query.Where(item => item.Role == role);
            }

            return await query.ToListAsync();
        }
        private async Task<List<UserAccounts>> GetUserAccountsSearchP(string id_number, string full_name = "", string role = "", int order_by_code = 0, int page_first_result = 0, int results_per_page = 10)
        {
            var query = _db.UserAccounts.AsQueryable();

            if (!string.IsNullOrEmpty(id_number))
            {
                query = query.Where(item => item.IdNumber.StartsWith(id_number));
            }

            if (!string.IsNullOrEmpty(full_name))
            {
                query = query.Where(item => item.FullName.StartsWith(full_name));
            }

            if (!string.IsNullOrEmpty(role))
            {
                query = query.Where(item => item.Role == role);
            }

            switch (order_by_code)
            {
                case 0:
                    query = query.OrderBy(item => item.Id);
                    break;
                case 1:
                    query = query.OrderByDescending(item => item.Id);
                    break;
                case 2:
                    query = query.OrderBy(item => item.IdNumber);
                    break;
                case 3:
                    query = query.OrderByDescending(item => item.IdNumber);
                    break;
                case 4:
                    query = query.OrderBy(item => item.Username);
                    break;
                case 5:
                    query = query.OrderByDescending(item => item.Username);
                    break;
                case 6:
                    query = query.OrderBy(item => item.FullName);
                    break;
                case 7:
                    query = query.OrderByDescending(item => item.FullName);
                    break;
                case 8:
                    query = query.OrderBy(item => item.Section);
                    break;
                case 9:
                    query = query.OrderByDescending(item => item.Section);
                    break;
                case 10:
                    query = query.OrderBy(item => item.Role);
                    break;
                case 11:
                    query = query.OrderByDescending(item => item.Role);
                    break;
                default:
                    break;
            }

            return await query.Skip(page_first_result).Take(results_per_page).ToListAsync();
        }
        private async Task<int> InsertUserAccount(UserAccounts user_account)
        {
            _db.UserAccounts.Add(user_account);
            return await _db.SaveChangesAsync();
        }
        private async Task<int> UpdateUserAccount(UserAccounts updated_user_account)
        {
            var user_account = await GetUserAccountById(updated_user_account.Id);
            if (user_account != null)
            {
                user_account.IdNumber = updated_user_account.IdNumber;
                user_account.FullName = updated_user_account.FullName;
                user_account.Username = updated_user_account.Username;
                user_account.Password = updated_user_account.Password;
                user_account.Section = updated_user_account.Section;
                user_account.Role = updated_user_account.Role;
                return await _db.SaveChangesAsync();
            } else
            {
                return 0;
            }
        }
        private async Task<int> DeleteUserAccount(int id)
        {
            _db.UserAccounts.Remove(await GetUserAccountById(id));
            return await _db.SaveChangesAsync();
        }
        [HttpGet]
        public async Task<IActionResult> CountAsync([FromQuery] string employee_no = "", string full_name = "", string user_type = "")
        {
            int total = await CountUserAccounts(employee_no, full_name, user_type);
            return Content(total.ToString(), "text/html");
        }
        [HttpGet]
        public async Task<IActionResult> LoadUserAccountsJsonAsync()
        {
            var user_accounts = await GetUserAccounts();
            int c = 0;
            List<string> data = new List<string>();

            if (user_accounts != null)
            {
                foreach(var user_account in user_accounts)
                {
                    c++;
                    string row = "<tr style=\"cursor: pointer; \" class=\"modal-trigger\" data-toggle=\"modal\" data-target=\"#update_account\" onclick=\"";
                    row += "get_accounts_details(&quot;" + user_account.Id.ToString() + "~!~" + user_account.IdNumber + "~!~" + user_account.Username + "~!~" + user_account.FullName + "~!~" + user_account.Password + "~!~" + user_account.Section + "~!~" + user_account.Role + "&quot;)";
                    row += "\">";

                    row += "<td>" + c.ToString() + "</td>";
                    row += "<td>" + user_account.IdNumber + "</td>";
                    row += "<td>" + user_account.Username + "</td>";
                    row += "<td>" + user_account.FullName + "</td>";
                    row += "<td>" + user_account.Section + "</td>";
                    row += "<td>" + user_account.Role.ToUpper() + "</td>";

                    row += "</tr>";

                    data.Add(row);
                }
            } else
            {
                data.Add("<tr><td colspan=\"6\" style=\"text - align:center; color: red; \">No Result !!!</td></tr>");
            }

            return Json(data);
        }
        [HttpGet]
        public async Task<IActionResult> LoadUserAccountsAsync()
        {
            var user_accounts = await GetUserAccounts();
            int c = 0;
            string data = "";

            if (user_accounts != null)
            {
                foreach (var user_account in user_accounts)
                {
                    c++;
                    string row = "<tr style=\"cursor: pointer; \" class=\"modal-trigger\" data-toggle=\"modal\" data-target=\"#update_account\" onclick=\"";
                    row += "get_accounts_details(&quot;" + user_account.Id.ToString() + "~!~" + user_account.IdNumber + "~!~" + user_account.Username + "~!~" + user_account.FullName + "~!~" + user_account.Password + "~!~" + user_account.Section + "~!~" + user_account.Role + "&quot;)";
                    row += "\">";

                    row += "<td>" + c.ToString() + "</td>";
                    row += "<td>" + user_account.IdNumber + "</td>";
                    row += "<td>" + user_account.Username + "</td>";
                    row += "<td>" + user_account.FullName + "</td>";
                    row += "<td>" + user_account.Section + "</td>";
                    row += "<td>" + user_account.Role.ToUpper() + "</td>";

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
        public async Task<IActionResult> SearchUserAccountsAsync([FromQuery] string employee_no = "", string full_name = "", string user_type = "")
        {
            var user_accounts = await GetUserAccountsSearch(employee_no, full_name, user_type);
            int c = 0;
            string data = "";

            if (user_accounts != null)
            {
                foreach (var user_account in user_accounts)
                {
                    c++;
                    string row = "<tr style=\"cursor: pointer; \" class=\"modal-trigger\" data-toggle=\"modal\" data-target=\"#update_account\" onclick=\"";
                    row += "get_accounts_details(&quot;" + user_account.Id.ToString() + "~!~" + user_account.IdNumber + "~!~" + user_account.Username + "~!~" + user_account.FullName + "~!~" + user_account.Password + "~!~" + user_account.Section + "~!~" + user_account.Role + "&quot;)";
                    row += "\">";

                    row += "<td>" + c.ToString() + "</td>";
                    row += "<td>" + user_account.IdNumber + "</td>";
                    row += "<td>" + user_account.Username + "</td>";
                    row += "<td>" + user_account.FullName + "</td>";
                    row += "<td>" + user_account.Section + "</td>";
                    row += "<td>" + user_account.Role.ToUpper() + "</td>";

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
        public async Task<IActionResult> SearchPagePAsync([FromQuery] int current_page, int order_by_code = 0, string employee_no = "", string full_name = "", string user_type = "")
        {
            int c = 0;
            int number_of_result = await CountUserAccounts(employee_no, full_name, user_type);
            int results_per_page = 10;

            //determine the sql LIMIT starting number for the results on the displaying page
            int page_first_result = (current_page - 1) * results_per_page;

            // Table Header Sort Behavior
            switch (order_by_code) {
                case 0:
                case 2:
                case 4:
                case 6:
                case 8:
                case 10:
                    c = page_first_result;
                    break;
                case 1:
                case 3:
                case 5:
                case 7:
                case 9:
                case 11:
                    c = (number_of_result - page_first_result) + 1;
                    break;
                default:
                    break;
            }

            var user_accounts = await GetUserAccountsSearchP(employee_no, full_name, user_type, order_by_code, page_first_result, results_per_page);
            
            string data = "";

            if (user_accounts != null)
            {
                foreach (var user_account in user_accounts)
                {
                    // Table Header Sort Behavior
                    switch (order_by_code) {
                        case 0:
                        case 2:
                        case 4:
                        case 6:
                        case 8:
                        case 10:
                            c++;
                            break;
                        case 1:
                        case 3:
                        case 5:
                        case 7:
                        case 9:
                        case 11:
                            c--;
                            break;
                        default:
                            break;
                    }

                    string row = "<tr>";

                    row += "<td>" + c.ToString() + "</td>";
                    row += "<td>" + user_account.IdNumber + "</td>";
                    row += "<td>" + user_account.Username + "</td>";
                    row += "<td>" + user_account.FullName + "</td>";
                    row += "<td>" + user_account.Section + "</td>";
                    row += "<td>" + user_account.Role.ToUpper() + "</td>";

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
        public async Task<IActionResult> SearchPageLAsync([FromQuery] int current_page, string employee_no = "", string full_name = "", string user_type = "")
        {
            int results_per_page = 10;

            //determine the sql LIMIT starting number for the results on the displaying page
            int page_first_result = (current_page - 1) * results_per_page;

            int c = page_first_result;

            var user_accounts = await GetUserAccountsSearchP(employee_no, full_name, user_type, 0, page_first_result, results_per_page);

            string data = "";

            if (user_accounts != null)
            {
                foreach (var user_account in user_accounts)
                {
                    c++;

                    string row = "<tr>";

                    row += "<td>" + c.ToString() + "</td>";
                    row += "<td>" + user_account.IdNumber + "</td>";
                    row += "<td>" + user_account.Username + "</td>";
                    row += "<td>" + user_account.FullName + "</td>";
                    row += "<td>" + user_account.Section + "</td>";
                    row += "<td>" + user_account.Role.ToUpper() + "</td>";

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
        public async Task<IActionResult> SearchPageKAsync([FromQuery] int current_page, string employee_no = "")
        {
            int results_per_page = 10;

            //determine the sql LIMIT starting number for the results on the displaying page
            int page_first_result = (current_page - 1) * results_per_page;

            int c = page_first_result;

            var user_accounts = await GetUserAccountsSearchP(employee_no, "", "", 0, page_first_result, results_per_page);

            string data = "";

            if (user_accounts != null)
            {
                foreach (var user_account in user_accounts)
                {
                    c++;

                    string row = "<tr>";

                    row += "<td>" + c.ToString() + "</td>";
                    row += "<td>" + user_account.IdNumber + "</td>";
                    row += "<td>" + user_account.Username + "</td>";
                    row += "<td>" + user_account.FullName + "</td>";
                    row += "<td>" + user_account.Section + "</td>";
                    row += "<td>" + user_account.Role.ToUpper() + "</td>";

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
        public async Task<IActionResult> SearchPaginationPAsync([FromQuery] string employee_no = "", string full_name = "", string user_type = "")
        {
            int results_per_page = 10;

            int number_of_result = await CountUserAccounts(employee_no, full_name, user_type);

            //determine the total number of pages available
            int number_of_page = (int)Math.Ceiling(Convert.ToDecimal(number_of_result) / Convert.ToDecimal(results_per_page));

            string data = "";

            for (int page = 1; page <= number_of_page; page++) {
                data += "<option value=\"" + page.ToString() + "\">" + page.ToString() + "</option>";
            }

            return Content(data, "text/html");
        }
        [HttpGet]
        public async Task<IActionResult> SearchLastPageLAsync([FromQuery] string employee_no = "", string full_name = "", string user_type = "")
        {
            int results_per_page = 10;

            int number_of_result = await CountUserAccounts(employee_no, full_name, user_type);

            //determine the total number of pages available
            int number_of_page = (int)Math.Ceiling(Convert.ToDecimal(number_of_result) / Convert.ToDecimal(results_per_page));

            return Content(number_of_page.ToString(), "text/html");
        }
        [HttpGet]
        public async Task<IActionResult> SearchLastPageKAsync([FromQuery] string employee_no = "")
        {
            int results_per_page = 10;

            int number_of_result = await CountUserAccounts(employee_no, "", "");

            //determine the total number of pages available
            int number_of_page = (int)Math.Ceiling(Convert.ToDecimal(number_of_result) / Convert.ToDecimal(results_per_page));

            return Content(number_of_page.ToString(), "text/html");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert(UserAccounts user_account)
        {
            string data = "";

            if (!ModelState.IsValid)
                return BadRequest("Enter Required Fields");

            int inserted = await InsertUserAccount(user_account);

            if (inserted > 0)
            {
                data = "success";
            }

            return Content(data, "text/html");
        }
        /*[HttpPost]
        public async Task<IActionResult> InsertAsync(IFormCollection fc)
        {
            string data = "";

            UserAccountsModel user_account = new UserAccountsModel
            {
                Id = 0,
                IdNumber = fc["IdNumber"],
                FullName = fc["FullName"],
                Username = fc["Username"],
                Password = fc["Password"],
                Section = fc["Section"],
                Role = fc["Role"]
            };

            int inserted = await InsertUserAccount(user_account);

            if (inserted > 0)
            {
                data = "success";
            }

            return Content(data, "text/html");
        }*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UserAccounts user_account)
        {
            string data = "";

            if (!ModelState.IsValid)
                return BadRequest("Enter Required Fields");

            int updated = await UpdateUserAccount(user_account);

            if (updated > 0)
            {
                data = "success";
            }

            return Content(data, "text/html");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(IFormCollection fc)
        {
            string data = "";

            if (!ModelState.IsValid)
                return BadRequest("Enter Required Fields");

            int deleted = await DeleteUserAccount(int.Parse(fc["Id"]));

            if (deleted > 0)
            {
                data = "success";
            }

            return Content(data, "text/html");
        }
        [HttpGet]
        public async Task<IActionResult> ExportAsync([FromQuery] string employee_no = "", string full_name = "")
        {
            int c = 0;
            DateTime datenow = DateTime.Now;
            string filename = "Export Accounts 3 - " + datenow.ToString("yyyy-MM-dd") + ".csv";

            StringBuilder csv = new StringBuilder();

            csv.AppendLine("#,ID Number,Full Name,Username,Password,Section,Role");

            var user_accounts = await GetUserAccountsSearch(employee_no, full_name, "");

            if (user_accounts != null)
            {
                foreach (var user_account in user_accounts)
                {
                    c++;

                    StringBuilder csv_row = new StringBuilder();

                    csv_row.Append(c.ToString().Replace(",", "") + ",");
                    csv_row.Append(user_account.IdNumber.ToString().Replace(",", "") + ",");
                    csv_row.Append(string.Format("\"{0}\",", user_account.FullName.ToString()));
                    csv_row.Append(user_account.Username.ToString().Replace(",", "") + ",");
                    csv_row.Append(user_account.Password.ToString().Replace(",", "") + ",");
                    csv_row.Append(user_account.Section.ToString().Replace(",", "") + ",");
                    csv_row.Append(user_account.Role.ToString().Replace(",", ""));

                    csv.AppendLine(csv_row.ToString());
                }
            }
            else
            {
                csv.AppendLine("No Result !!!");
            }

            byte[] buffer = Encoding.UTF8.GetBytes('\uFEFF' + csv.ToString()); // Convert CSV string to byte array
            return File(buffer, "text/csv;charset=utf-8", filename); // Return file result
        }
        [HttpGet]
        public async Task<IActionResult> Export3Async([FromQuery] string employee_no = "", string full_name = "")
        {
            // Not a true Excel File
            int c = 0;
            DateTime datenow = DateTime.Now;
            string filename = "Export Accounts 3 - " + datenow.ToString("yyyy-MM-dd") + ".xls";

            StringBuilder csv = new StringBuilder();

            csv.AppendLine("#,ID Number,Full Name,Username,Password,Section,Role");

            var user_accounts = await GetUserAccountsSearch(employee_no, full_name, "");

            if (user_accounts != null)
            {
                foreach (var user_account in user_accounts)
                {
                    c++;

                    StringBuilder csv_row = new StringBuilder();

                    csv_row.Append(c.ToString().Replace(",", "") + ",");
                    csv_row.Append(user_account.IdNumber.ToString().Replace(",", "") + ",");
                    csv_row.Append(string.Format("\"{0}\",", user_account.FullName.ToString()));
                    csv_row.Append(user_account.Username.ToString().Replace(",", "") + ",");
                    csv_row.Append(user_account.Password.ToString().Replace(",", "") + ",");
                    csv_row.Append(user_account.Section.ToString().Replace(",", "") + ",");
                    csv_row.Append(user_account.Role.ToString().Replace(",", ""));

                    csv.AppendLine(csv_row.ToString());
                }
            }
            else
            {
                csv.AppendLine("No Result !!!");
            }

            byte[] buffer = Encoding.UTF8.GetBytes('\uFEFF' + csv.ToString()); // Convert CSV string to byte array
            return File(buffer, "application/vnd.ms-excel", filename); // Return file result
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportAsync()
        {
            var files = Request.Form.Files;
            if (files.Count > 0)
            {
                var file = files[0];
                string fileName = file.FileName;

                if (!string.IsNullOrEmpty(fileName))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/csv", fileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                    }

                    if (System.IO.File.Exists(filePath))
                    {
                        try
                        {
                            using (var reader = new StreamReader(filePath, Encoding.UTF8))
                            {
                                // Skip first line (header)
                                await reader.ReadLineAsync();

                                string line;
                                while ((line = await reader.ReadLineAsync()) != null)
                                {
                                    var row = line.Split(',');

                                    var user_account = new UserAccounts
                                    {
                                        Id = 0,
                                        IdNumber = row[0],
                                        FullName = row[1],
                                        Username = row[2],
                                        Password = row[3],
                                        Section = row[4],
                                        Role = row[5]
                                    };

                                    _db.UserAccounts.Add(user_account);
                                }
                                await _db.SaveChangesAsync();
                            }
                        }
                        catch (Exception ex)
                        {
                            return Ok("SYSTEM ERROR: " + ex.Message + " " + ex.ToString());
                        }
                    }
                    else
                    {
                        return Ok("File not found after saving to temp path, Try to upload file again");
                    }
                }
                else
                {
                    return Ok("Failed to get filename. Please upload file");
                }
            }

            return Ok("");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import2Async()
        {
            string data = "";

            if (!string.IsNullOrEmpty(Request.Form["download_template"]))
            {
                return Redirect("~/template/accounts_template.csv");
            }

            if (!string.IsNullOrEmpty(Request.Form["upload"]))
            {
                var files = Request.Form.Files;
                if (files.Count > 0)
                {
                    var file = files[0];
                    string fileName = file.FileName;

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/csv", fileName);
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await file.CopyToAsync(stream);
                        }

                        if (System.IO.File.Exists(filePath))
                        {
                            try
                            {
                                using (var reader = new StreamReader(filePath, Encoding.UTF8))
                                {
                                    // Skip first line (header)
                                    await reader.ReadLineAsync();

                                    string line;
                                    while ((line = await reader.ReadLineAsync()) != null)
                                    {
                                        var row = line.Split(',');

                                        var user_account = new UserAccounts
                                        {
                                            Id = 0,
                                            IdNumber = row[0],
                                            FullName = row[1],
                                            Username = row[2],
                                            Password = row[3],
                                            Section = row[4],
                                            Role = row[5]
                                        };

                                        _db.UserAccounts.Add(user_account);
                                    }
                                    await _db.SaveChangesAsync();

                                }
                            }
                            catch (Exception ex)
                            {
                                data += "<script>alert('Error: SYSTEM ERROR: " + ex.Message + " " + ex.ToString() + "')</script>";
                            }

                            data += "<script>alert('Success')</script>";
                        }
                        else
                        {
                            data += "<script>alert('Error: File not found after saving to temp path, Try to upload file again')</script>";
                        }
                    }
                    else
                    {
                        data += "<script>alert('Error: Failed to get filename. Please upload file')</script>";
                    }
                }
                else
                {
                    data += "<script>alert('Error: Please upload file')</script>";
                }

                data += "<script> window.location.href = '" + @Url.Action("Accounts", "Admin") + "' </script>";
            }

            return Content(data, "text/html");
        }
    }
}
