using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string FeedBack { get; set; }
        public int Rating { get; set; }
        public DateTime DateComment { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
    }
}
