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
    public class TblseguridadController : Controller
    {
        private readonly DbfacturasContext _context;

        public TblseguridadController(DbfacturasContext context)
        {
            _context = context;
        }

        // GET: Tblseguridad
        public async Task<IActionResult> Index()
        {
            var dbfacturasContext = _context.Tblseguridads.Include(t => t.IdEmpleadoNavigation);
            return View(await dbfacturasContext.ToListAsync());
        }

        // GET: Tblseguridad/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tblseguridads == null)
            {
                return NotFound();
            }

            var tblseguridad = await _context.Tblseguridads
                .Include(t => t.IdEmpleadoNavigation)
                .FirstOrDefaultAsync(m => m.IdSeguridad == id);
            if (tblseguridad == null)
            {
                return NotFound();
            }

            return View(tblseguridad);
        }

        // GET: Tblseguridad/Create
        public IActionResult Create()
        {
            ViewData["IdEmpleado"] = new SelectList(_context.Tblempleados, "IdEmpleado", "IdEmpleado");
            return View();
        }

        // POST: Tblseguridad/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSeguridad,IdEmpleado,StrUsuario,StrClave,DtmFechaModifica,StrUsuarioModifico")] Tblseguridad tblseguridad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblseguridad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEmpleado"] = new SelectList(_context.Tblempleados, "IdEmpleado", "IdEmpleado", tblseguridad.IdEmpleado);
            return View(tblseguridad);
        }

        // GET: Tblseguridad/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tblseguridads == null)
            {
                return NotFound();
            }

            var tblseguridad = await _context.Tblseguridads.FindAsync(id);
            if (tblseguridad == null)
            {
                return NotFound();
            }
            ViewData["IdEmpleado"] = new SelectList(_context.Tblempleados, "IdEmpleado", "IdEmpleado", tblseguridad.IdEmpleado);
            return View(tblseguridad);
        }

        // POST: Tblseguridad/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSeguridad,IdEmpleado,StrUsuario,StrClave,DtmFechaModifica,StrUsuarioModifico")] Tblseguridad tblseguridad)
        {
            if (id != tblseguridad.IdSeguridad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblseguridad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblseguridadExists(tblseguridad.IdSeguridad))
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
            ViewData["IdEmpleado"] = new SelectList(_context.Tblempleados, "IdEmpleado", "IdEmpleado", tblseguridad.IdEmpleado);
            return View(tblseguridad);
        }

        // GET: Tblseguridad/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tblseguridads == null)
            {
                return NotFound();
            }

            var tblseguridad = await _context.Tblseguridads
                .Include(t => t.IdEmpleadoNavigation)
                .FirstOrDefaultAsync(m => m.IdSeguridad == id);
            if (tblseguridad == null)
            {
                return NotFound();
            }

            return View(tblseguridad);
        }

        // POST: Tblseguridad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tblseguridads == null)
            {
                return Problem("Entity set 'DbfacturasContext.Tblseguridads'  is null.");
            }
            var tblseguridad = await _context.Tblseguridads.FindAsync(id);
            if (tblseguridad != null)
            {
                _context.Tblseguridads.Remove(tblseguridad);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblseguridadExists(int id)
        {
          return (_context.Tblseguridads?.Any(e => e.IdSeguridad == id)).GetValueOrDefault();
        }
    }
}
