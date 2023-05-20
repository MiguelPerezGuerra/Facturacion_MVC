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
    public class TblcategoriaProdsController : Controller
    {
        private readonly DbfacturasContext _context;

        public TblcategoriaProdsController(DbfacturasContext context)
        {
            _context = context;
        }

        // GET: TblcategoriaProds
        public async Task<IActionResult> Index(string buscar)
        {
            var categoria = from TblcategoriaProd in _context.TblcategoriaProds select TblcategoriaProd;

            if (!string.IsNullOrEmpty(buscar))
            {
                categoria = categoria.Where(s => s.StrDescripcion!.Contains(buscar));

            }
            return View(await categoria.ToListAsync());
        }

        // GET: TblcategoriaProds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblcategoriaProds == null)
            {
                return NotFound();
            }

            var tblcategoriaProd = await _context.TblcategoriaProds
                .FirstOrDefaultAsync(m => m.IdCategoria == id);
            if (tblcategoriaProd == null)
            {
                return NotFound();
            }

            return View(tblcategoriaProd);
        }

        // GET: TblcategoriaProds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblcategoriaProds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCategoria,StrDescripcion,DtmFechaModifica,StrUsuarioModifico")] TblcategoriaProd tblcategoriaProd)
        {
            if (ModelState.IsValid)
            {
                tblcategoriaProd.DtmFechaModifica = DateTime.Now;
                tblcategoriaProd.StrUsuarioModifico = "Javier";

                _context.Add(tblcategoriaProd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblcategoriaProd);
        }

        // GET: TblcategoriaProds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblcategoriaProds == null)
            {
                return NotFound();
            }

            var tblcategoriaProd = await _context.TblcategoriaProds.FindAsync(id);
            if (tblcategoriaProd == null)
            {
                return NotFound();
            }
            return View(tblcategoriaProd);
        }

        // POST: TblcategoriaProds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCategoria,StrDescripcion,DtmFechaModifica,StrUsuarioModifico")] TblcategoriaProd tblcategoriaProd)
        {
            if (id != tblcategoriaProd.IdCategoria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tblcategoriaProd.DtmFechaModifica = DateTime.Now;
                    tblcategoriaProd.StrUsuarioModifico = "Javier";

                    _context.Update(tblcategoriaProd);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblcategoriaProdExists(tblcategoriaProd.IdCategoria))
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
            return View(tblcategoriaProd);
        }

        // GET: TblcategoriaProds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblcategoriaProds == null)
            {
                return NotFound();
            }

            var tblcategoriaProd = await _context.TblcategoriaProds
                .FirstOrDefaultAsync(m => m.IdCategoria == id);
            if (tblcategoriaProd == null)
            {
                return NotFound();
            }

            return View(tblcategoriaProd);
        }

        // POST: TblcategoriaProds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblcategoriaProds == null)
            {
                return Problem("Entity set 'DbfacturasContext.TblcategoriaProds'  is null.");
            }
            var tblcategoriaProd = await _context.TblcategoriaProds.FindAsync(id);
            if (tblcategoriaProd != null)
            {
                _context.TblcategoriaProds.Remove(tblcategoriaProd);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblcategoriaProdExists(int id)
        {
          return (_context.TblcategoriaProds?.Any(e => e.IdCategoria == id)).GetValueOrDefault();
        }
    }
}
