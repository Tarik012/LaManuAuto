﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaManuAuto.Data;
using LaManuAuto.Models;
using Microsoft.AspNetCore.Authorization;

namespace LaManuAuto.Controllers
{
    public class TutorielsController : Controller
    {
        private readonly LAMANU_AUTOContext _context;

        public TutorielsController(LAMANU_AUTOContext context)
        {
            _context = context;
        }

        // GET: Tutoriels
        //[AllowAnonymous]
        [Authorize]
        public async Task<IActionResult> Index()
        {
              return _context.Tutoriels != null ? 
                          View(await _context.Tutoriels.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Tutoriels'  is null.");
        }

        // GET: Tutoriels/Details/5
        //[AllowAnonymous]
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tutoriels == null)
            {
                return NotFound();
            }

            var tutoriel = await _context.Tutoriels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tutoriel == null)
            {
                return NotFound();
            }

            return View(tutoriel);
        }

        // GET: Tutoriels/Create
       // [Authorize(Roles = "Admin, Employee")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tutoriels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       // [Authorize(Roles = "Admin, Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titre,Dcc,Contenu,VideoLink,Dml")] Tutoriel tutoriel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tutoriel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tutoriel);
        }

        // GET: Tutoriels/Edit/5
       // [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tutoriels == null)
            {
                return NotFound();
            }

            var tutoriel = await _context.Tutoriels.FindAsync(id);
            if (tutoriel == null)
            {
                return NotFound();
            }
            return View(tutoriel);
        }

        // POST: Tutoriels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       // [Authorize(Roles = "Admin, Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titre,Dcc,Contenu,VideoLink,Dml")] Tutoriel tutoriel)
        {
            if (id != tutoriel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tutoriel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TutorielExists(tutoriel.Id))
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
            return View(tutoriel);
        }

        // GET: Tutoriels/Delete/5
       // [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tutoriels == null)
            {
                return NotFound();
            }

            var tutoriel = await _context.Tutoriels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tutoriel == null)
            {
                return NotFound();
            }

            return View(tutoriel);
        }

        // POST: Tutoriels/Delete/5
       // [Authorize(Roles = "Admin, Employee")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tutoriels == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tutoriels'  is null.");
            }
            var tutoriel = await _context.Tutoriels.FindAsync(id);
            if (tutoriel != null)
            {
                _context.Tutoriels.Remove(tutoriel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TutorielExists(int id)
        {
          return (_context.Tutoriels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
