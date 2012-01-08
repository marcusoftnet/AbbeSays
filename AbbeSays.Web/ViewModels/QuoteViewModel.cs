using System;

namespace AbbeSays.Web.ViewModels
{
    public class QuoteViewModel
    {
        public int Id { get; set; }
        public string Quote { get; set; }
        public DateTime SaidAt { get; set; }
        public int Likes { get; set; }
    }
}