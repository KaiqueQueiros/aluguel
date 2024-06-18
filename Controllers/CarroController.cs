#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aluguel.Data;
using Aluguel.Models;

namespace Aluguel.Controllers
{
    public class CarroController : Controller
    {
        private readonly AluguelContext _context;

        public CarroController(AluguelContext context)
        {
            _context = context;
        }

        // GET: Carro
        public async Task<IActionResult> Index()
        {
            var aluguelContext = _context.Carro
                .Include(c => c.Marca);// Inclui CarroMarcas e suas respectivas Marcas

            return View(await aluguelContext.ToListAsync());

        }
        
        

        // GET: Carro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carro = await _context.Carro
                .Include(c => c.Marca)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carro == null)
            {
                return NotFound();
            }

            return View(carro);
        }

        // GET: Carro/Create
        public IActionResult Create()
        {
            ViewBag.Marcas = new SelectList(_context.Marca.ToList(), "Id", "Nome");
            return View();
        }

        // POST: Carro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Modelo,Ano,MarcaId")] Carro carro)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(carro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Marcas = new SelectList(_context.Marca, "Id", "Nome", carro.MarcaId);
            return View(carro);
        }

        // GET: Carro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carro = await _context.Carro.FindAsync(id);
            if (carro == null)
            {
                return NotFound();
            }
            ViewBag.Marcas = new SelectList(_context.Marca, "Id", "Nome", carro.MarcaId);
            return View(carro);
        }

        // POST: Carro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Modelo,Ano, MarcaId")] Carro carro)
        {
            if (id != carro.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(carro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarroExists(carro.Id))
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
            ViewBag.Marcas = new SelectList(_context.Marca, "Id", "Nome", carro.MarcaId);
            return View(carro);
        }

        // GET: Carro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carro = await _context.Carro
                .Include(c => c.Marca)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carro == null)
            {
                return NotFound();
            }

            return View(carro);
        }

        // POST: Carro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carro = await _context.Carro.FindAsync(id);
            _context.Carro.Remove(carro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarroExists(int id)
        {
            return _context.Carro.Any(e => e.Id == id);
        }
    }
}
