using System;

namespace AbbeSays.Web.ViewModels
{
    public class QuotesIndexVM
    {
        public int Id { get; set; }
        public int KidId { get; set; }
        public string Quote { get; set; }
        public DateTime SaidAt { get; set; }

        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}