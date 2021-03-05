using MDS.Vue.Core;
using MDS.Vue.Web.Models.Department;
using MDS.Vue.Web.Models.Direction;
using MDS.Vue.Web.Models.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDS.Vue.Web.Controllers
{
    [ApiController]
    public class ListsController : ControllerBase
    {
        private readonly OrgUnitContext _context;

        public ListsController(OrgUnitContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/list/direction")]
        public async Task<IEnumerable<DirectionListItemDto>> DirectionList()
        {
            var query = _context.OrgUnits
                .Where(x => x.Type == UnitType.Direction && !x.Deleted)
                .Select(x => new DirectionListItemDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    DepartmentsCount = _context.OrgUnits.Where(y => y.ParentId == x.Id && !y.Deleted).Count(),
                });

            var directions = await query.ToListAsync();

            return directions;
        }

        [HttpGet]
        [Route("api/list/department")]
        public async Task<ActionResult<DepartmentListDto>> GetDirection([FromQuery] long id)
        {
            var query = _context.OrgUnits
                .Where(x => x.Id == id && x.Type == UnitType.Direction && !x.Deleted)
                .Select(x => new DepartmentListDto
                {
                    DirectionId = x.Id,
                    DirectionName = x.Name,
                    DepartmentsCount = _context.OrgUnits.Where(y => y.ParentId == x.Id && !y.Deleted).Count(),
                    Departments = _context.OrgUnits
                        .Where(y => y.ParentId == x.Id && !y.Deleted)
                        .Select(x => new DepartmentListItemDto
                        {
                            Id = x.Id,
                            Name = x.Name,
                            EmployeesCount = _context.OrgUnits
                            .Where(y => y.ParentId == x.Id && !y.Deleted).Count()
                        }).ToList()
                });

            var departmenList = await query.FirstOrDefaultAsync();

            if (departmenList == null) return NotFound();

            var heads = _context.OrgUnits
                .Where(x =>
                    !x.Deleted &&
                    departmenList.Departments.Select(y => (long?)y.Id).ToList().Contains(x.ParentId) &&
                    x.Type == UnitType.Employee && x.IsHeadOfOrgUnit)
                .ToList();

            departmenList.Departments.ForEach(x =>
            {
                var head = heads.FirstOrDefault(y => y.ParentId == x.Id);
                x.HeadName = head?.Name;
                x.HeadPosition = head?.Position;
            });

            return departmenList;
        }

        [HttpGet]
        [Route("api/list/employee")]
        public async Task<ActionResult<EmployeeListDto>> GetDepartment([FromQuery] long id)
        {
            var query = _context.OrgUnits
                .Where(x => x.Id == id && x.Type == UnitType.Department && !x.Deleted)
                .Select(x => new EmployeeListDto
                {
                    DirectionId = x.Parent.Id,
                    DirectionName = x.Parent.Name,
                    DepartmentId = x.Id,
                    DepartmentName = x.Name,
                    EmployeesCount = _context.OrgUnits.Where(y => y.ParentId == x.Id && !y.Deleted).Count(),
                    Employees = _context.OrgUnits
                        .Where(y => y.ParentId == x.Id && !y.Deleted)
                        .Select(x => new EmployeeListItemDto
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Position = x.Position
                        }).ToList()
                });

            var employeeList = await query.FirstOrDefaultAsync();

            if (employeeList == null) return NotFound();

            var head = await _context.OrgUnits
                .Where(x => !x.Deleted && x.ParentId == id && x.IsHeadOfOrgUnit)
                .FirstOrDefaultAsync();

            employeeList.HeadName = head?.Name;

            employeeList.HeadPosition = head?.Position;

            return employeeList;
        }
    }
}
