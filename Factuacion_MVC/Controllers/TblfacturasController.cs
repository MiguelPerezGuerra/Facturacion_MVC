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
    public class TblfacturasController : Controller
    {
        private readonly DbfacturasContext _context;

        public TblfacturasController(DbfacturasContext context)
        {
            _context = context;
        }

        // GET: Tblfacturas
        public async Task<IActionResult> Index()
        {
            var dbfacturasContext = _context.Tblfacturas.Include(t => t.IdClienteNavigation).Include(t => t.IdEmpleadoNavigation).Include(t => t.IdEstadoNavigation);
            return View(await dbfacturasContext.ToListAsync());
        }

        // GET: Tblfacturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tblfacturas == null)
            {
                return NotFound();
            }

            var tblfactura = await _context.Tblfacturas
                .Include(t => t.IdClienteNavigation)
                .Include(t => t.IdEmpleadoNavigation)
                .Include(t => t.IdEstadoNavigation)
                .FirstOrDefaultAsync(m => m.IdFactura == id);
            if (tblfactura == null)
            {
                return NotFound();
            }

            return View(tblfactura);
        }

        // GET: Tblfacturas/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Tblclientes, "IdCliente", "IdCliente");
            ViewData["IdEmpleado"] = new SelectList(_context.Tblempleados, "IdEmpleado", "IdEmpleado");
            ViewData["IdEstado"] = new SelectList(_context.TblestadoFacturas, "IdEstadoFactura", "IdEstadoFactura");
            return View();
        }

        // POST: Tblfacturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFactura,DtmFecha,IdCliente,IdEmpleado,NumDescuento,NumImpuesto,NumValorTotal,IdEstado,DtmFechaModifica,StrUsuarioModifica")] Tblfactura tblfactura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblfactura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Tblclientes, "IdCliente", "IdCliente", tblfactura.IdCliente);
            ViewData["IdEmpleado"] = new SelectList(_context.Tblempleados, "IdEmpleado", "IdEmpleado", tblfactura.IdEmpleado);
            ViewData["IdEstado"] = new SelectList(_context.TblestadoFacturas, "IdEstadoFactura", "IdEstadoFactura", tblfactura.IdEstado);
            return View(tblfactura);
        }

        // GET: Tblfacturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tblfacturas == null)
            {
                return NotFound();
            }

            var tblfactura = await _context.Tblfacturas.FindAsync(id);
            if (tblfactura == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Tblclientes, "IdCliente", "IdCliente", tblfactura.IdCliente);
            ViewData["IdEmpleado"] = new SelectList(_context.Tblempleados, "IdEmpleado", "IdEmpleado", tblfactura.IdEmpleado);
            ViewData["IdEstado"] = new SelectList(_context.TblestadoFacturas, "IdEstadoFactura", "IdEstadoFactura", tblfactura.IdEstado);
            return View(tblfactura);
        }

        // POST: Tblfacturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFactura,DtmFecha,IdCliente,IdEmpleado,NumDescuento,NumImpuesto,NumValorTotal,IdEstado,DtmFechaModifica,StrUsuarioModifica")] Tblfactura tblfactura)
        {
            if (id != tblfactura.IdFactura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblfactura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblfacturaExists(tblfactura.IdFactura))
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
            ViewData["IdCliente"] = new SelectList(_context.Tblclientes, "IdCliente", "IdCliente", tblfactura.IdCliente);
            ViewData["IdEmpleado"] = new SelectList(_context.Tblempleados, "IdEmpleado", "IdEmpleado", tblfactura.IdEmpleado);
            ViewData["IdEstado"] = new SelectList(_context.TblestadoFacturas, "IdEstadoFactura", "IdEstadoFactura", tblfactura.IdEstado);
            return View(tblfactura);
        }

        // GET: Tblfacturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tblfacturas == null)
            {
                return NotFound();
            }

            var tblfactura = await _context.Tblfacturas
                .Include(t => t.IdClienteNavigation)
                .Include(t => t.IdEmpleadoNavigation)
                .Include(t => t.IdEstadoNavigation)
                .FirstOrDefaultAsync(m => m.IdFactura == id);
            if (tblfactura == null)
            {
                return NotFound();
            }

            return View(tblfactura);
        }

        // POST: Tblfacturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tblfacturas == null)
            {
                return Problem("Entity set 'DbfacturasContext.Tblfacturas'  is null.");
            }
            var tblfactura = await _context.Tblfacturas.FindAsync(id);
            if (tblfactura != null)
            {
                _context.Tblfacturas.Remove(tblfactura);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblfacturaExists(int id)
        {
          return (_context.Tblfacturas?.Any(e => e.IdFactura == id)).GetValueOrDefault();
        }
    }
}
