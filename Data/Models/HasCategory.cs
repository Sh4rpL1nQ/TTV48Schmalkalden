using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class HasCategory
    {
        [Key, ForeignKey("News"), Column(Order = 0)]
        public int NewsId { get; set; }

        [Key, ForeignKey("Category"), Column(Order = 1)]
        public int CategoryId { get; set; }


        public News News { get; set; }

        [Key]
        public Category Category { get; set; }
    }
}
