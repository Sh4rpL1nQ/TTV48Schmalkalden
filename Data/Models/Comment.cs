using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public string Body { get; set; }

        public DateTime Written { get; set; }

        public News News { get; set; }
    }
}
