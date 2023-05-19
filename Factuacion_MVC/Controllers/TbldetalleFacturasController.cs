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
    public class TbldetalleFacturasController : Controller
    {
        private readonly DbfacturasContext _context;

        public TbldetalleFacturasController(DbfacturasContext context)
        {
            _context = context;
        }

        // GET: TbldetalleFacturas
        public async Task<IActionResult> Index()
        {
            var dbfacturasContext = _context.TbldetalleFacturas.Include(t => t.IdFacturaNavigation).Include(t => t.IdProductoNavigation);
            return View(await dbfacturasContext.ToListAsync());
        }

        // GET: TbldetalleFacturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbldetalleFacturas == null)
            {
                return NotFound();
            }

            var tbldetalleFactura = await _context.TbldetalleFacturas
                .Include(t => t.IdFacturaNavigation)
                .Include(t => t.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdDetalle == id);
            if (tbldetalleFactura == null)
            {
                return NotFound();
            }

            return View(tbldetalleFactura);
        }

        // GET: TbldetalleFacturas/Create
        public IActionResult Create()
        {
            ViewData["IdFactura"] = new SelectList(_context.Tblfacturas, "IdFactura", "IdFactura");
            ViewData["IdProducto"] = new SelectList(_context.Tblproductos, "IdProducto", "IdProducto");
            return View();
        }

        // POST: TbldetalleFacturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetalle,IdFactura,NumCantidad,IdProducto,NumPrecio")] TbldetalleFactura tbldetalleFactura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbldetalleFactura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdFactura"] = new SelectList(_context.Tblfacturas, "IdFactura", "IdFactura", tbldetalleFactura.IdFactura);
            ViewData["IdProducto"] = new SelectList(_context.Tblproductos, "IdProducto", "IdProducto", tbldetalleFactura.IdProducto);
            return View(tbldetalleFactura);
        }

        // GET: TbldetalleFacturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbldetalleFacturas == null)
            {
                return NotFound();
            }

            var tbldetalleFactura = await _context.TbldetalleFacturas.FindAsync(id);
            if (tbldetalleFactura == null)
            {
                return NotFound();
            }
            ViewData["IdFactura"] = new SelectList(_context.Tblfacturas, "IdFactura", "IdFactura", tbldetalleFactura.IdFactura);
            ViewData["IdProducto"] = new SelectList(_context.Tblproductos, "IdProducto", "IdProducto", tbldetalleFactura.IdProducto);
            return View(tbldetalleFactura);
        }

        // POST: TbldetalleFacturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDetalle,IdFactura,NumCantidad,IdProducto,NumPrecio")] TbldetalleFactura tbldetalleFactura)
        {
            if (id != tbldetalleFactura.IdDetalle)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbldetalleFactura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbldetalleFacturaExists(tbldetalleFactura.IdDetalle))
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
            ViewData["IdFactura"] = new SelectList(_context.Tblfacturas, "IdFactura", "IdFactura", tbldetalleFactura.IdFactura);
            ViewData["IdProducto"] = new SelectList(_context.Tblproductos, "IdProducto", "IdProducto", tbldetalleFactura.IdProducto);
            return View(tbldetalleFactura);
        }

        // GET: TbldetalleFacturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbldetalleFacturas == null)
            {
                return NotFound();
            }

            var tbldetalleFactura = await _context.TbldetalleFacturas
                .Include(t => t.IdFacturaNavigation)
                .Include(t => t.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdDetalle == id);
            if (tbldetalleFactura == null)
            {
                return NotFound();
            }

            return View(tbldetalleFactura);
        }

        // POST: TbldetalleFacturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbldetalleFacturas == null)
            {
                return Problem("Entity set 'DbfacturasContext.TbldetalleFacturas'  is null.");
            }
            var tbldetalleFactura = await _context.TbldetalleFacturas.FindAsync(id);
            if (tbldetalleFactura != null)
            {
                _context.TbldetalleFacturas.Remove(tbldetalleFactura);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbldetalleFacturaExists(int id)
        {
          return (_context.TbldetalleFacturas?.Any(e => e.IdDetalle == id)).GetValueOrDefault();
        }
    }
}
