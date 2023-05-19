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
    public class TblempleadoController : Controller
    {
        private readonly DbfacturasContext _context;

        public TblempleadoController(DbfacturasContext context)
        {
            _context = context;
        }

        // GET: Tblempleado
        public async Task<IActionResult> Index(string buscar)
        {
            var empleado = from Tblempleado in _context.Tblempleados select Tblempleado;

            if (!string.IsNullOrEmpty(buscar))
            {
                empleado = empleado.Where(s => s.StrNombre!.Contains(buscar));
            }

            return View(await empleado.ToListAsync());
        }

        // GET: Tblempleado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tblempleados == null)
            {
                return NotFound();
            }

            var tblempleado = await _context.Tblempleados
                .Include(t => t.IdRolEmpleadoNavigation)
                .FirstOrDefaultAsync(m => m.IdEmpleado == id);
            if (tblempleado == null)
            {
                return NotFound();
            }

            return View(tblempleado);
        }

        // GET: Tblempleado/Create
        public IActionResult Create()
        {
            ViewData["IdRolEmpleado"] = new SelectList(_context.Tblroles, "IdRolEmpleado", "StrDescripcion");
            return View();
        }

        // POST: Tblempleado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEmpleado,StrNombre,NumDocumento,StrDireccion,StrTelefono,StrEmail,IdRolEmpleado,DtmIngreso,DtmRetiro,StrDatosAdicionales,DtmFechaModifica,StrUsuarioModifico")] Tblempleado tblempleado)
        {
            if (ModelState.IsValid)
            {
                tblempleado.DtmFechaModifica = DateTime.Now;
                tblempleado.StrUsuarioModifico = "Javier";

                _context.Add(tblempleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRolEmpleado"] = new SelectList(_context.Tblroles, "IdRolEmpleado", "StrDescripcion", tblempleado.IdRolEmpleado);
            return View(tblempleado);
        }

        // GET: Tblempleado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tblempleados == null)
            {
                return NotFound();
            }

            var tblempleado = await _context.Tblempleados.FindAsync(id);
            if (tblempleado == null)
            {
                return NotFound();
            }
            ViewData["IdRolEmpleado"] = new SelectList(_context.Tblroles, "IdRolEmpleado", "StrDescripcion", tblempleado.IdRolEmpleado);
            return View(tblempleado);
        }

        // POST: Tblempleado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind($"IdEmpleado,StrNombre,NumDocumento,StrDireccion,StrTelefono,StrEmail,IdRolEmpleado,DtmIngreso,DtmRetiro,StrDatosAdicionales,DtmFechaModifica,StrUsuarioModifico")] Tblempleado tblempleado)
        {
            if (id != tblempleado.IdEmpleado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tblempleado.DtmFechaModifica = DateTime.Now;
                    tblempleado.StrUsuarioModifico = "Javier";

                    _context.Update(tblempleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblempleadoExists(tblempleado.IdEmpleado))
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
            ViewData["IdRolEmpleado"] = new SelectList(_context.Tblroles, "IdRolEmpleado", "StrDescripcion", tblempleado.IdRolEmpleado);
            return View(tblempleado);
        }

        // GET: Tblempleado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tblempleados == null)
            {
                return NotFound();
            }

            var tblempleado = await _context.Tblempleados
                .Include(t => t.IdRolEmpleadoNavigation)
                .FirstOrDefaultAsync(m => m.IdEmpleado == id);
            if (tblempleado == null)
            {
                return NotFound();
            }

            return View(tblempleado);
        }

        // POST: Tblempleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tblempleados == null)
            {
                return Problem("Entity set 'DbfacturasContext.Tblempleados'  is null.");
            }
            var tblempleado = await _context.Tblempleados.FindAsync(id);
            if (tblempleado != null)
            {
                _context.Tblempleados.Remove(tblempleado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblempleadoExists(int id)
        {
          return (_context.Tblempleados?.Any(e => e.IdEmpleado == id)).GetValueOrDefault();
        }
    }
}
