using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FSDRealEstate.Data;
using FSDRealEstate.Models;
using FSDRealEstate.Repository;

namespace FSDRealEstate.Controllers
{
    [Route("Image")]
    public class ImagesController : Controller
    {
        private readonly IImage _context;

        public ImagesController(IImage context)
        {
            _context = context;
        }

        // GET: Images
        public async Task<IActionResult> Index()
        {
              return View(_context.GetAll().ToList());
        }

        // GET: Images/Details/5
        [Route("Details")]
        public async Task<IActionResult> Details(int id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var image = _context.GetObject(id);

            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // GET: Images/Create
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create([Bind("Id,Property_id,ImageUrl")] Image image)
        {
            if (ModelState.IsValid)
            {
                _context.Create(image);
                return RedirectToAction(nameof(Index));
            }
            return View(image);
        }

        // GET: Images/Edit/5
        [Route("Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = _context.GetObject(id);
            if (image == null)
            {
                return NotFound();
            }
            return View(image);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Property_id,ImageUrl")] Image image)
        {
            if (id != image.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(image);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageExists(image.Id))
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
            return View(image);
        }

        // GET: Images/Delete/5
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = _context.Delete(id);

            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GetObject(id) == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Image'  is null.");
            }
            var image = _context.GetObject(id);
            if (image != null)
            {
                _context.Delete(id);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool ImageExists(int id)
        {
            //return _context.Image.Any(e => e.Id == id);
            return true;
        }
    }
}
