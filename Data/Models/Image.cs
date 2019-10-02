using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Image
    {
        public int Id  { get; set; }

        public string ImageUrl { get; set; }

        public string ImageDescription { get; set; }

        public News News { get; set; }
    }
}
