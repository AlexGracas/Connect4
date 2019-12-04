using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Connect4.Data;
using Connect4.Models;

namespace Connect4.Controllers
{
    public class JogadorComputadorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JogadorComputadorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JogadorComputadors
        public async Task<IActionResult> Index()
        {
            return View(await _context.JogadorComputador.ToListAsync());
        }

        // GET: JogadorComputadors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogadorComputador = await _context.JogadorComputador
                .SingleOrDefaultAsync(m => m.Id == id);
            if (jogadorComputador == null)
            {
                return NotFound();
            }

            return View(jogadorComputador);
        }

        // GET: JogadorComputadors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JogadorComputadors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("URLServico,NomeComputador,Id")] JogadorComputador jogadorComputador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jogadorComputador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jogadorComputador);
        }

        // GET: JogadorComputadors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogadorComputador = await _context.JogadorComputador.SingleOrDefaultAsync(m => m.Id == id);
            if (jogadorComputador == null)
            {
                return NotFound();
            }
            return View(jogadorComputador);
        }

        // POST: JogadorComputadors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("URLServico,NomeComputador,Id")] JogadorComputador jogadorComputador)
        {
            if (id != jogadorComputador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jogadorComputador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JogadorComputadorExists(jogadorComputador.Id))
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
            return View(jogadorComputador);
        }

        // GET: JogadorComputadors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogadorComputador = await _context.JogadorComputador
                .SingleOrDefaultAsync(m => m.Id == id);
            if (jogadorComputador == null)
            {
                return NotFound();
            }

            return View(jogadorComputador);
        }

        // POST: JogadorComputadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jogadorComputador = await _context.JogadorComputador.SingleOrDefaultAsync(m => m.Id == id);
            _context.JogadorComputador.Remove(jogadorComputador);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JogadorComputadorExists(int id)
        {
            return _context.JogadorComputador.Any(e => e.Id == id);
        }
    }
}
