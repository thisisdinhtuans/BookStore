using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly BookStoreContext _context;
        private readonly Cart _cart;
        private readonly UserManager<DefaultUser> _userManager;

        public OrderController(UserManager<DefaultUser> userManager, BookStoreContext context, Cart cart)
        {
            _userManager = userManager;
            _context = context;
            _cart = cart;
        }
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            var cartItems = _cart.GetAllCartItems();
            _cart.CartItems = cartItems;

            if (_cart.CartItems.Count == 0)
            {
                ModelState.AddModelError(key: "", errorMessage: "Cart is emty, please add a book first");
            }

            if (ModelState.IsValid)
            {
                await CreateOrder(order);
                _cart.ClearCart();
                return View(viewName: "CheckoutComplete", order);
            }

            return View(order);
        }

        public IActionResult CheckoutComplete(Order order)
        {
            return View(order);
        }

        public async Task CreateOrder(Order order)
        {
            DefaultUser user = await _userManager.GetUserAsync(User);

            // Kiểm tra xem người dùng có tồn tại không
            if (user == null)
            {
                // Xử lý lỗi, người dùng không được tìm thấy hoặc không đăng nhập
                BadRequest("User not found or not logged in.");
            }
            order.OrderPlaced = DateTime.Now;
            var cartItems = _cart.CartItems;
            order.UserId = user.Id;
            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem()
                {
                    Quantity = item.Quantity,
                    BookId = item.Book.Id,
                    OrderId = order.Id,
                    Price = item.Book.Price * item.Quantity
                };
                order.OrderItems.Add(orderItem);
                order.OrderTotal += orderItem.Price;
            }
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}
