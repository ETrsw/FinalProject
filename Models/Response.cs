
using System;
using sandwichAPI.Models;

namespace sandwichAPI.Models
{
    public class Response
    {
        public int statusCode { get; set; }
        public string statusDescription { get; set; }
         public sandwich?  sandwich { get; set; }
        public List<sandwich>? sandwichList { get; set; }

    }
}
