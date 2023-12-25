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
    public class StoreMVCController : Controller
    {
        private readonly CourseContext _context;

        public StoreMVCController(CourseContext context)
        {
            _context = context;
        }

        // GET: StoreMVC
        public async Task<IActionResult> Index()
        {
              return _context.Stores != null ? 
                          View(await _context.Stores.ToListAsync()) :
                          Problem("Entity set 'CourseContext.Stores'  is null.");
        }

        // GET: StoreMVC/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Stores == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .FirstOrDefaultAsync(m => m.StorId == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // GET: StoreMVC/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StoreMVC/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StorId,StorName,StorAddress,City,State,Zip")] Stores store)
        {
            if (ModelState.IsValid)
            {
                _context.Add(store);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(store);
        }

        // GET: StoreMVC/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Stores == null)
            {
                return NotFound();
            }

            var store = await _context.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }
            return View(store);
        }

        // POST: StoreMVC/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> v(string id, [Bind("StorId,StorName,StorAddress,City,State,Zip")] Stores store)
        {
            if (id != store.StorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(store);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreExists(store.StorId))
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
            return View(store);
        }

        // GET: StoreMVC/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Stores == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .FirstOrDefaultAsync(m => m.StorId == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // POST: StoreMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Stores == null)
            {
                return Problem("Entity set 'CourseContext.Stores'  is null.");
            }
            var store = await _context.Stores.FindAsync(id);
            if (store != null)
            {
                _context.Stores.Remove(store);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreExists(string id)
        {
          return (_context.Stores?.Any(e => e.StorId == id)).GetValueOrDefault();
        }
    }
}
