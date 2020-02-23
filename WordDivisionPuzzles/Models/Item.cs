using System.Collections;

namespace WordDivisionPuzzles.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Quotient { get; set; }
        public string Divisor { get; set; }
        public string AlphaDivisor { get; set; }
        public string AlphaQuotient { get; set; }
        public ArrayList Letters { get; set; }
        
    }
}