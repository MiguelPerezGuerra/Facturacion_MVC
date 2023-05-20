using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Factuacion_MVC.Models;

namespace Factuacion_MVC.Controllers
{
    public class TblrolesController : Controller
    {
        private readonly DbfacturasContext _context;

        public TblrolesController(DbfacturasContext context)
        {
            _context = context;
        }

        // GET: Tblroles
        public async Task<IActionResult> Index(string buscar)
        {

            var roles = from Tblrole in _context.Tblroles select Tblrole;

            if (!string.IsNullOrEmpty(buscar))
            {
                roles = roles.Where(s => s.StrDescripcion!.Contains(buscar));
            }

            return View(await roles.ToListAsync());

              //return _context.Tblroles != null ? 
              //            View(await _context.Tblroles.ToListAsync()) :
              //            Problem("Entity set 'DbfacturasContext.Tblroles'  is null.");
        }

        // GET: Tblroles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tblroles == null)
            {
                return NotFound();
            }

            var tblrole = await _context.Tblroles
                .FirstOrDefaultAsync(m => m.IdRolEmpleado == id);
            if (tblrole == null)
            {
                return NotFound();
            }

            return View(tblrole);
        }

        // GET: Tblroles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tblroles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRolEmpleado,StrDescripcion")] Tblrole tblrole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblrole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblrole);
        }

        // GET: Tblroles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tblroles == null)
            {
                return NotFound();
            }

            var tblrole = await _context.Tblroles.FindAsync(id);
            if (tblrole == null)
            {
                return NotFound();
            }
            return View(tblrole);
        }

        // POST: Tblroles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRolEmpleado,StrDescripcion")] Tblrole tblrole)
        {
            if (id != tblrole.IdRolEmpleado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblrole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblroleExists(tblrole.IdRolEmpleado))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tblrole);
        }

        // GET: Tblroles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tblroles == null)
            {
                return NotFound();
            }

            var tblrole = await _context.Tblroles
                .FirstOrDefaultAsync(m => m.IdRolEmpleado == id);
            if (tblrole == null)
            {
                return NotFound();
            }

            return View(tblrole);
        }

        // POST: Tblroles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tblroles == null)
            {
                return Problem("Entity set 'DbfacturasContext.Tblroles'  is null.");
            }
            var tblrole = await _context.Tblroles.FindAsync(id);
            if (tblrole != null)
            {
                _context.Tblroles.Remove(tblrole);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblroleExists(int id)
        {
          return (_context.Tblroles?.Any(e => e.IdRolEmpleado == id)).GetValueOrDefault();
        }
    }
}
