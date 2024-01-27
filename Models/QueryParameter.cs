using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SortedCodingTest.Models
{
    public class QueryParameter
    {
        [Range(1, 100)]
        public int? Count { get; set; }
    }
}
