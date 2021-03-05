using MDS.Vue.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDS.Vue.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrgUnitsController : ControllerBase
    {
        private readonly OrgUnitContext _context;

        public OrgUnitsController(OrgUnitContext context)
        {
            _context = context;
        }

        // GET: api/OrgUnits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrgUnit>>> GetOrgUnits()
        {
            return await _context.OrgUnits.ToListAsync();
        }

        // GET: api/OrgUnits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrgUnit>> GetOrgUnit(long id)
        {
            var orgUnit = await _context.OrgUnits
                .Include(x => x.Parent)
                .Where(x => !x.Deleted)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (orgUnit == null)
            {
                return NotFound();
            }

            return orgUnit;
        }

        // PUT: api/OrgUnits/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrgUnit(long id, OrgUnit orgUnit)
        {
            if (id != orgUnit.Id)
            {
                return BadRequest();
            }

            _context.Entry(orgUnit).State = EntityState.Modified;

            try
            {
                DropHeadOfOrgUnit(orgUnit);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrgUnitExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OrgUnits
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<OrgUnit>> PostOrgUnit(OrgUnit orgUnit)
        {
            _context.OrgUnits.Add(orgUnit);

            DropHeadOfOrgUnit(orgUnit);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrgUnit", new { id = orgUnit.Id }, orgUnit);
        }

        // DELETE: api/OrgUnits/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrgUnit>> DeleteOrgUnit(long id)
        {
            var orgUnit = await _context.OrgUnits.FindAsync(id);

            if (orgUnit == null)
            {
                return NotFound();
            }

            orgUnit.Deleted = true;

            await _context.SaveChangesAsync();

            return orgUnit;
        }

        private bool OrgUnitExists(long id)
        {
            return _context.OrgUnits.Any(e => e.Id == id && !e.Deleted);
        }

        private void DropHeadOfOrgUnit(OrgUnit employee)
        {
            if (employee.Type != UnitType.Employee) return;

            if (!employee.IsHeadOfOrgUnit) return;

            var subordinates = _context.OrgUnits
                .Where(x =>
                    !x.Deleted && x.ParentId == employee.ParentId && x.Id != employee.Id).ToList();

            subordinates.ForEach(x =>
            {
                x.IsHeadOfOrgUnit = false;
            });
        }
    }
}
