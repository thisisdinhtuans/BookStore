using System;

namespace BookStore.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string UserId { get; set; } // Tham chiếu đến người dùng
        public int BookId { get; set; } // Tham chiếu đến cuốn sách
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
