using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniShoppingSystem.Data;
using MiniShoppingSystem.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace MiniShoppingSystem.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hosting;

        public BooksController(ApplicationDbContext context, IHostingEnvironment hosting)
        {
            _context = context;
            _hosting = hosting;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Books.Include(b => b.Genre);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "GenreName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookName,AuthorName,Price,Image,GenreId")] Book book)
        {
            var file = HttpContext.Request.Form.Files["clientFile"];

            if (string.IsNullOrEmpty(book.Image))
            {
                // Assign the default image path if no image is provided
                book.Image = "NoImage.png";
            }

            // Retrieve the corresponding Genre object based on the entered GenreId
            var genre = _context.Genres.Find(book.GenreId);

            if (genre != null)
            {
                // Assign the retrieved Genre object to the Genre property of the Book model
                book.Genre = genre;

                // Check if a file was uploaded
                if (file != null && file.Length > 0)
                {
                    // Save the file to the server
                    string fileName = Path.GetFileName(file.FileName);
                    string uploadPath = Path.Combine(_hosting.WebRootPath, "Images", fileName);
                    using (var fileStream = new FileStream(uploadPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    // Update the book's Image property with the file name
                    book.Image = fileName;
                }

                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // If the entered GenreId does not correspond to any existing Genre in the database, add a model error
                ModelState.AddModelError("GenreId", "Invalid Genre");
            }

            // Populate ViewBag.GenreId for the dropdown
            ViewBag.GenreId = new SelectList(_context.Genres, "Id", "GenreName", book.GenreId);

            return View(book);
        }


        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "GenreName", book.GenreId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("Id,BookName,AuthorName,Price,Image,GenreId")] Book book, bool noImageCheckbox)
        {
            // Find the existing book from the database
            var existingBook = await _context.Books.FindAsync(id);

            
            // If "No Image" checkbox is not checked, retain the existing image if no new image is uploaded
            if (book.Image == null)
            {
                book.Image = existingBook.Image;
            }
            

            // Retrieve the corresponding Genre object based on the entered GenreId
            var genre = _context.Genres.Find(book.GenreId);

            if (genre != null)
            {
                // Assign the retrieved Genre object to the Genre property of the Book model
                book.Genre = genre;

                // Check if a file was uploaded
                var file = HttpContext.Request.Form.Files["clientFile"];
                if (file != null && file.Length > 0)
                {
                    // Save the file to the server
                    string fileName = Path.GetFileName(file.FileName);
                    string uploadPath = Path.Combine(_hosting.WebRootPath, "Images", fileName);
                    using (var fileStream = new FileStream(uploadPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    // Update the book's Image property with the file name
                    book.Image = fileName;
                }

                // Update the existing book with the edited values
                existingBook.BookName = book.BookName;
                existingBook.AuthorName = book.AuthorName;
                existingBook.Price = book.Price;
                existingBook.Image = book.Image; // Update the image property with the new or existing value
                existingBook.GenreId = book.GenreId;

                // Save changes to the database
                try
                {
                    _context.Update(existingBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(existingBook.Id))
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
            else
            {
                // If the entered GenreId does not correspond to any existing Genre in the database, add a model error
                ModelState.AddModelError("GenreId", "Invalid Genre");
            }

            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "GenreName", book.GenreId);
            return View(book);
        }




        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
