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
    public class TblproductoesController : Controller
    {
        private readonly DbfacturasContext _context;

        public TblproductoesController(DbfacturasContext context)
        {
            _context = context;
        }

        // GET: Tblproductoes
        public async Task<IActionResult> Index(string buscar)
        {
            var productos = from Tblproducto in _context.Tblproductos select Tblproducto;

            if (!string.IsNullOrEmpty(buscar))
            {
                productos = productos.Where(p => p.StrNombre.Contains(buscar));
            }

            productos = productos.Include(p => p.IdCategoriaNavigation);

            return View(await productos.ToListAsync());

            //var dbfacturasContext = _context.Tblproductos.Include(t => t.IdCategoriaNavigation);
            //return View(await dbfacturasContext.ToListAsync());
        }


        // GET: Tblproductoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tblproductos == null)
            {
                return NotFound();
            }

            var tblproducto = await _context.Tblproductos
                .Include(t => t.IdCategoriaNavigation)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (tblproducto == null)
            {
                return NotFound();
            }

            return View(tblproducto);
        }

        // GET: Tblproductoes/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.TblcategoriaProds, "IdCategoria", "StrDescripcion");
            return View();
        }

        // POST: Tblproductoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProducto,StrNombre,StrCodigo,NumPrecioCompra,NumPrecioVenta,IdCategoria,StrDetalle,StrFoto,NumStock")] Tblproducto tblproducto)
        {
            if (ModelState.IsValid)
            {
                tblproducto.DtmFechaModifica = DateTime.Now;
                tblproducto.StrUsuarioModifica = "Miguel";

                _context.Add(tblproducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.TblcategoriaProds, "IdCategoria", "IdCategoria", tblproducto.IdCategoria);
            return View(tblproducto);
        }

        // GET: Tblproductoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tblproductos == null)
            {
                return NotFound();
            }

            var tblproducto = await _context.Tblproductos.FindAsync(id);
            if (tblproducto == null)
            {
                return NotFound();
            }

            ViewData["IdCategoria"] = new SelectList(_context.TblcategoriaProds, "IdCategoria", "StrDescripcion", tblproducto.IdCategoria);
            
            return View(tblproducto);
        }

        // POST: Tblproductoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProducto,StrNombre,StrCodigo,NumPrecioCompra,NumPrecioVenta,IdCategoria,StrDetalle,StrFoto,NumStock")] Tblproducto tblproducto)
        {
            if (id != tblproducto.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tblproducto.DtmFechaModifica = DateTime.Now;
                    tblproducto.StrUsuarioModifica = "Miguel";

                    _context.Update(tblproducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblproductoExists(tblproducto.IdProducto))
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
            ViewData["IdCategoria"] = new SelectList(_context.TblcategoriaProds, "IdCategoria", "IdCategoria", tblproducto.IdCategoria);
            return View(tblproducto);
        }

        // GET: Tblproductoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tblproductos == null)
            {
                return NotFound();
            }

            var tblproducto = await _context.Tblproductos
                .Include(t => t.IdCategoriaNavigation)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (tblproducto == null)
            {
                return NotFound();
            }

            return View(tblproducto);
        }

        // POST: Tblproductoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tblproductos == null)
            {
                return Problem("Entity set 'DbfacturasContext.Tblproductos'  is null.");
            }
            var tblproducto = await _context.Tblproductos.FindAsync(id);
            if (tblproducto != null)
            {
                _context.Tblproductos.Remove(tblproducto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblproductoExists(int id)
        {
          return (_context.Tblproductos?.Any(e => e.IdProducto == id)).GetValueOrDefault();
        }
    }
}
