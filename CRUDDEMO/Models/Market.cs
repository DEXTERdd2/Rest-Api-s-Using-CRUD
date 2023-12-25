using System;
using System.Collections.Generic;

namespace CRUDDEMO.Models
{
    public partial class Market
    {
        public string StorId { get; set; } = null!;
        public string? StorName { get; set; }
        public string? StorAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
    }
}
