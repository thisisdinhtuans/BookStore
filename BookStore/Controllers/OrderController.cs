using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly BookStoreContext _context;
        private readonly Cart _cart;

        public OrderController(BookStoreContext context, Cart cart)
        {
            _context = context;
            _cart = cart;
        }
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var cartItems = _cart.GetAllCartItems();
            _cart.CartItems = cartItems;

            if(_cart.CartItems.Count==0)
            {
                ModelState.AddModelError(key : "", errorMessage: "Cart is emty, please add a book first");
            }

            if (ModelState.IsValid)
            {
                CreateOrder(order);
                _cart.ClearCart();
                return View(viewName: "CheckoutComplete", order);
            }

            return View(order);
        }

        public IActionResult CheckoutComplete(Order order)
        {
            return View(order);
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            var cartItems = _cart.CartItems;
            foreach(var item in cartItems)
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
