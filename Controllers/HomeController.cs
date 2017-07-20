using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iSnippets.Models;

namespace iSnippets.Controllers
{
    public class HomeController : Controller
    {
        private MyDbContext db = new MyDbContext();
        public async Task<IActionResult> Index(string DescSearch, string LangSearch)
        {
            var snippets = from SnippetTable in db.SnippetTable select SnippetTable;

            if (!String.IsNullOrEmpty(DescSearch))
            {
                snippets = snippets.Where(snippet => snippet.snipDesc.Contains(DescSearch));
            }

            if (!String.IsNullOrEmpty(LangSearch))
            {
                snippets = snippets.Where(snippet => snippet.snipLang.Contains(LangSearch));
            }

            return View(await snippets.ToListAsync());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Snippet Manager 1.0.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        // GET: Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var snippet = await db.SnippetTable.SingleOrDefaultAsync(m => m.Id == id);
            if (snippet == null)
            {
                return NotFound();
            }

            return View(snippet);
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,snipDesc,snipText,snipLang")] Snippet snippet)
        {
            if (ModelState.IsValid)
            {
                db.Add(snippet);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(snippet);
        }

        // GET: Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var snippet = await db.SnippetTable.SingleOrDefaultAsync(m => m.Id == id);
            if (snippet == null)
            {
                return NotFound();
            }
            return View(snippet);
        }

        // POST: Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,snipDesc,snipText,snipLang")] Snippet snippet)
        {
            if (id != snippet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(snippet);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SnippetExists(snippet.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(snippet);
        }

        // GET: Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var snippet = await db.SnippetTable.SingleOrDefaultAsync(m => m.Id == id);
            if (snippet == null)
            {
                return NotFound();
            }

            return View(snippet);
        }

        // POST: Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var snippet = await db.SnippetTable.SingleOrDefaultAsync(m => m.Id == id);
            db.SnippetTable.Remove(snippet);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SnippetExists(int id)
        {
            return db.SnippetTable.Any(e => e.Id == id);
        }
    }
}
