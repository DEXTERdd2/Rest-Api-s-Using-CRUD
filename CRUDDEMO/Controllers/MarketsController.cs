using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUDDEMO;
using CRUDDEMO.Models;

namespace CRUDDEMO.Controllers
{
    public class MarketsController : Controller
    {
        private readonly CourseContext _context;

        public MarketsController(CourseContext context)
        {
            _context = context;
        }

        // GET: Markets
        public async Task<IActionResult> Index()
        {
              return _context.Markets != null ? 
                          View(await _context.Markets.ToListAsync()) :
                          Problem("Entity set 'CourseContext.Markets'  is null.");
        }

        // GET: Markets/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Markets == null)
            {
                return NotFound();
            }

            var market = await _context.Markets
                .FirstOrDefaultAsync(m => m.StorId == id);
            if (market == null)
            {
                return NotFound();
            }

            return View(market);
        }

        // GET: Markets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Markets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StorId,StorName,StorAddress,City,State,Zip")] Market market)
        {
            if (ModelState.IsValid)
            {
                _context.Add(market);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(market);
        }

        // GET: Markets/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Markets == null)
            {
                return NotFound();
            }

            var market = await _context.Markets.FindAsync(id);
            if (market == null)
            {
                return NotFound();
            }
            return View(market);
        }

        // POST: Markets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StorId,StorName,StorAddress,City,State,Zip")] Market market)
        {
            if (id != market.StorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(market);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarketExists(market.StorId))
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
            return View(market);
        }

        // GET: Markets/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Markets == null)
            {
                return NotFound();
            }

            var market = await _context.Markets
                .FirstOrDefaultAsync(m => m.StorId == id);
            if (market == null)
            {
                return NotFound();
            }

            return View(market);
        }

        // POST: Markets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Markets == null)
            {
                return Problem("Entity set 'CourseContext.Markets'  is null.");
            }
            var market = await _context.Markets.FindAsync(id);
            if (market != null)
            {
                _context.Markets.Remove(market);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarketExists(string id)
        {
          return (_context.Markets?.Any(e => e.StorId == id)).GetValueOrDefault();
        }
    }
}
