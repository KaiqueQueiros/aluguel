#nullable disable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aluguel.Data;
using Aluguel.Models;

namespace Aluguel.Controllers
{
    public class MotoristaController : Controller
    {
        private readonly AluguelContext _context;

        public MotoristaController(AluguelContext context)
        {
            _context = context;
        }

        // GET: Motorista
        public async Task<IActionResult> Index()
        {
            return View(await _context.Motorista.ToListAsync());
        }

        // GET: Motorista/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorista = await _context.Motorista
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motorista == null)
            {
                return NotFound();
            }

            return View(motorista);
        }

        // GET: Motorista/Create
        public IActionResult Create()
        {
            ViewBag.Carros = new SelectList(_context.Carro.ToList(), "Id", "Modelo");

            return View();
        }

        // POST: Motorista/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,DataNascimento,Endereco,CarroId")] Motorista motorista)
        {
            motorista.DataNascimento = DateTime.SpecifyKind(motorista.DataNascimento, DateTimeKind.Utc);

            if (!ModelState.IsValid)
            {
                _context.Add(motorista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Carros = new SelectList(_context.Carro.ToList(), "Id", "Modelo", motorista.CarroId);

            return View(motorista);
        }

        // GET: Motorista/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorista = await _context.Motorista.FindAsync(id);
            if (motorista == null)
            {
                return NotFound();
            }
            ViewBag.Carros = new SelectList(_context.Carro.ToList(), "Id", "Modelo", motorista.CarroId);

            return View(motorista);
        }

        // POST: Motorista/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,DataNascimento,Endereco,CarroId")] Motorista motorista)
        {
            if (id != motorista.Id)
            {
                return NotFound();
            }
            motorista.DataNascimento = DateTime.SpecifyKind(motorista.DataNascimento, DateTimeKind.Utc);

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(motorista);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MotoristaExists(motorista.Id))
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
            ViewBag.Carros = new SelectList(_context.Carro.ToList(), "Id", "Modelo", motorista.CarroId);

            return View(motorista);
        }

        // GET: Motorista/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorista = await _context.Motorista
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motorista == null)
            {
                return NotFound();
            }

            return View(motorista);
        }

        // POST: Motorista/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var motorista = await _context.Motorista.FindAsync(id);
            _context.Motorista.Remove(motorista);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MotoristaExists(int id)
        {
            return _context.Motorista.Any(e => e.Id == id);
        }
    }
}
