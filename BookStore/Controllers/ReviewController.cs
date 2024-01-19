using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using BookStore.Data;

namespace BookStore.Controllers
{
    
    public class ReviewController : Controller
    {
        private readonly BookStoreContext _context;
        private readonly UserManager<DefaultUser> _userManager;

        public ReviewController(BookStoreContext context, UserManager<DefaultUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReview(int bookId, string comment, int rating)
        {
            DefaultUser user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return BadRequest("User not found or not logged in.");
            }

            Book book = _context.Books.Find(bookId);

            if (book == null)
            {
                return NotFound("Book not found.");
            }
            
            var review = new Review
            {
                UserId = user.Id,
                BookId = book.Id,
                Comment = comment,
                Rating = rating,
                CreatedAt = DateTime.Now,
            };

            _context.Reviews.Add(review);
            _context.SaveChanges();

            return RedirectToAction("Details", "Store", new { id = bookId });
        }
    }
}
