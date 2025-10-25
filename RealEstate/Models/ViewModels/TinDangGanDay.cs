using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Models.ViewModels
{
    public class TinDangGanDay
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public int TongSoPost {  get; set; }
    }
}