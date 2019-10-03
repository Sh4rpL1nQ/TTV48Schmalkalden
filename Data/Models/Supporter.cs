using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Supporter
    {
        public int Id { get; set; }

        public string Bussines { get; set; }

        public string Owner { get; set; }

        public string Link { get; set; }

        public string LinkName { get; set; }

        public SupportingType SupportingType { get; set; }
    }

    public class SupportingType
    {
        public int Id { get; set; }

        public string TypeName { get; set; }
    }
}
