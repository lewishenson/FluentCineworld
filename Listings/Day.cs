﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LewisHenson.FluentCineworld.Listings
{
    [DebuggerDisplay("Date = {Date}")]
    public class Day
    {
        public Day()
        {
        }

        public Day(DateTime date, IEnumerable<Show> shows)
        {
            Date = date;
            Shows = shows;
        }

        public DateTime Date { get; set; }

        public IEnumerable<Show> Shows { get; set; }
    }
}