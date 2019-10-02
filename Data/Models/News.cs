using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Models
{
    public class News
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Body { get; set; }

        public DateTime Written { get; set; }

        public string ImageUrl { get; set; }
    }
}
