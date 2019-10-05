using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class CalendarTask
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public DateTime Begin { get; set; }

        public DateTime End { get; set; }

        public bool IsRecurrent { get; set; }

        public CalendarTaskType CalendarTaskType { get; set; }
    }

    public class CalendarTaskType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ColorCode { get; set; }

        public string HoverCode { get; set; }
    }
}
