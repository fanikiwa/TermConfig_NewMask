//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KruAll.Core.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PlannedCalendarTime
    {
        public long Id { get; set; }
        public long CalendarId { get; set; }
        public int CalendarMonth { get; set; }
        public System.TimeSpan Day1Hours { get; set; }
        public System.TimeSpan Day2Hours { get; set; }
        public System.TimeSpan Day3Hours { get; set; }
        public System.TimeSpan Day4Hours { get; set; }
        public System.TimeSpan Day5Hours { get; set; }
        public System.TimeSpan Day6Hours { get; set; }
        public System.TimeSpan Day7Hours { get; set; }
        public System.TimeSpan Day8Hours { get; set; }
        public System.TimeSpan Day9Hours { get; set; }
        public System.TimeSpan Day10Hours { get; set; }
        public System.TimeSpan Day11Hours { get; set; }
        public System.TimeSpan Day12Hours { get; set; }
        public System.TimeSpan Day13Hours { get; set; }
        public System.TimeSpan Day14Hours { get; set; }
        public System.TimeSpan Day15Hours { get; set; }
        public System.TimeSpan Day16Hours { get; set; }
        public System.TimeSpan Day17Hours { get; set; }
        public System.TimeSpan Day18Hours { get; set; }
        public System.TimeSpan Day19Hours { get; set; }
        public System.TimeSpan Day20Hours { get; set; }
        public System.TimeSpan Day21Hours { get; set; }
        public System.TimeSpan Day22Hours { get; set; }
        public System.TimeSpan Day23Hours { get; set; }
        public System.TimeSpan Day24Hours { get; set; }
        public System.TimeSpan Day25Hours { get; set; }
        public System.TimeSpan Day26Hours { get; set; }
        public System.TimeSpan Day27Hours { get; set; }
        public System.TimeSpan Day28Hours { get; set; }
        public System.TimeSpan Day29Hours { get; set; }
        public System.TimeSpan Day30Hours { get; set; }
        public System.TimeSpan Day31Hours { get; set; }
    
        public virtual PlannedCalendar PlannedCalendar { get; set; }
    }
}