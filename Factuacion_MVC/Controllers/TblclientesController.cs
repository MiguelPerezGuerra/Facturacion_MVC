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
    public class TblclientesController : Controller
    {
        private readonly DbfacturasContext _context;

        public TblclientesController(DbfacturasContext context)
        {
            _context = context;
        }

        // GET: Tblclientes
        public async Task<IActionResult> Index()
        {
              return _context.Tblclientes != null ? 
                          View(await _context.Tblclientes.ToListAsync()) :
                          Problem("Entity set 'DbfacturasContext.Tblclientes'  is null.");
        }

        // GET: Tblclientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tblclientes == null)
            {
                return NotFound();
            }

            var tblcliente = await _context.Tblclientes
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (tblcliente == null)
            {
                return NotFound();
            }

            return View(tblcliente);
        }

        // GET: Tblclientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tblclientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,StrNombre,NumDocumento,StrDireccion,StrTelefono,StrEmail,DtmFechaModifica,StrUsuarioModifica")] Tblcliente tblcliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblcliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblcliente);
        }

        // GET: Tblclientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tblclientes == null)
            {
                return NotFound();
            }

            var tblcliente = await _context.Tblclientes.FindAsync(id);
            if (tblcliente == null)
            {
                return NotFound();
            }
            return View(tblcliente);
        }

        // POST: Tblclientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCliente,StrNombre,NumDocumento,StrDireccion,StrTelefono,StrEmail,DtmFechaModifica,StrUsuarioModifica")] Tblcliente tblcliente)
        {
            if (id != tblcliente.IdCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblcliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblclienteExists(tblcliente.IdCliente))
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
            return View(tblcliente);
        }

        // GET: Tblclientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tblclientes == null)
            {
                return NotFound();
            }

            var tblcliente = await _context.Tblclientes
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (tblcliente == null)
            {
                return NotFound();
            }

            return View(tblcliente);
        }

        // POST: Tblclientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tblclientes == null)
            {
                return Problem("Entity set 'DbfacturasContext.Tblclientes'  is null.");
            }
            var tblcliente = await _context.Tblclientes.FindAsync(id);
            if (tblcliente != null)
            {
                _context.Tblclientes.Remove(tblcliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblclienteExists(int id)
        {
          return (_context.Tblclientes?.Any(e => e.IdCliente == id)).GetValueOrDefault();
        }
    }
}
