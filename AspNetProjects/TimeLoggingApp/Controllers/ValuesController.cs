using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeLoggingApp.Data;

namespace TimeLoggingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        ApplicationDbContext context;

        public ValuesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult DataTable()
        {
            //i didnt defined the model for what datatables is posting; but you can define it and use rich data type
            var draw = Request.Form["draw"].FirstOrDefault();               // given forms is dictionary (string, value) paris; i can use Linq Extension Methods
            var start = Request.Form["start"].FirstOrDefault();             // first or default will give me the first value or the default value; we can specify
            var length = Request.Form["length"].FirstOrDefault();           // the default value using default(type) for instance default(int) will be 0
            var sortColumn = Request.Form["order[0][column]"].FirstOrDefault();     // we discussed default values for reference and value types; reference values default value is null
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var q = from t in context.TeamMembers
                    select t;

            //sorting
            int column;
            if (!string.IsNullOrEmpty(sortColumn) && int.TryParse(sortColumn, out column))
            {
                if (column == 1 && sortColumnDirection == "asc")
                    q = q.OrderBy(o => o.TeamMemberId);
                else if (column == 1)
                    q = q.OrderByDescending(o => o.TeamMemberId);
                else if (column == 2 && sortColumnDirection == "asc")
                    q = q.OrderBy(o => o.FirstName);
                else if (column == 2)
                    q = q.OrderByDescending(o => o.FirstName);
                else if (column == 3 && sortColumnDirection == "asc")
                    q = q.OrderBy(o => o.LastName);
                else if (column == 3)
                    q = q.OrderByDescending(o => o.LastName);
                else if (column == 4 && sortColumnDirection == "asc")
                    q = q.OrderBy(o => o.Email);
                else if (column == 4)
                    q = q.OrderByDescending(o => o.Email);
            }

            //searching
            if (!string.IsNullOrEmpty(searchValue))
            {
                q = q.Where(o => o.FirstName.Contains(searchValue) ||
                    o.LastName.Contains(searchValue) ||
                    o.Email.Contains(searchValue));
            }

            int skip = start != null ? Convert.ToInt32(start) : 0;
            int pageSize = length != null ? Convert.ToInt32(length) : 0;

            // so far q is just expression
            int recordsTotal = q.Count();                           // these two making the db provider calls (backend / hitting sql/network)
            var data = q.Skip(skip).Take(pageSize).ToList();        // we want the whole result set and then the data; to tell ui; 25 of 100 items etc
                                                                    // skip and take are again linq extension methods; ToList() and Count() are basically hitting db

            //anonymous type; according to datatables requirement
            var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data };

            return Ok(jsonData);        // Ok; is basically base.Ok() there are other such helper methods to return different http status codes
        }
    }
}
