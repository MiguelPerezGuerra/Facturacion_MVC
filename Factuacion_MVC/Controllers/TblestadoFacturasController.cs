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
    public class TblestadoFacturasController : Controller
    {
        private readonly DbfacturasContext _context;

        public TblestadoFacturasController(DbfacturasContext context)
        {
            _context = context;
        }

        // GET: TblestadoFacturas
        public async Task<IActionResult> Index()
        {
              return _context.TblestadoFacturas != null ? 
                          View(await _context.TblestadoFacturas.ToListAsync()) :
                          Problem("Entity set 'DbfacturasContext.TblestadoFacturas'  is null.");
        }

        // GET: TblestadoFacturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblestadoFacturas == null)
            {
                return NotFound();
            }

            var tblestadoFactura = await _context.TblestadoFacturas
                .FirstOrDefaultAsync(m => m.IdEstadoFactura == id);
            if (tblestadoFactura == null)
            {
                return NotFound();
            }

            return View(tblestadoFactura);
        }

        // GET: TblestadoFacturas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblestadoFacturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEstadoFactura,StrDescripcion")] TblestadoFactura tblestadoFactura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblestadoFactura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblestadoFactura);
        }

        // GET: TblestadoFacturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblestadoFacturas == null)
            {
                return NotFound();
            }

            var tblestadoFactura = await _context.TblestadoFacturas.FindAsync(id);
            if (tblestadoFactura == null)
            {
                return NotFound();
            }
            return View(tblestadoFactura);
        }

        // POST: TblestadoFacturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEstadoFactura,StrDescripcion")] TblestadoFactura tblestadoFactura)
        {
            if (id != tblestadoFactura.IdEstadoFactura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblestadoFactura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblestadoFacturaExists(tblestadoFactura.IdEstadoFactura))
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
            return View(tblestadoFactura);
        }

        // GET: TblestadoFacturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblestadoFacturas == null)
            {
                return NotFound();
            }

            var tblestadoFactura = await _context.TblestadoFacturas
                .FirstOrDefaultAsync(m => m.IdEstadoFactura == id);
            if (tblestadoFactura == null)
            {
                return NotFound();
            }

            return View(tblestadoFactura);
        }

        // POST: TblestadoFacturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblestadoFacturas == null)
            {
                return Problem("Entity set 'DbfacturasContext.TblestadoFacturas'  is null.");
            }
            var tblestadoFactura = await _context.TblestadoFacturas.FindAsync(id);
            if (tblestadoFactura != null)
            {
                _context.TblestadoFacturas.Remove(tblestadoFactura);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblestadoFacturaExists(int id)
        {
          return (_context.TblestadoFacturas?.Any(e => e.IdEstadoFactura == id)).GetValueOrDefault();
        }
    }
}
