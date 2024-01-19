using System;

namespace BookStore.ViewModels
{
    public class ReviewViewModel
    {
        public string UserName { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
