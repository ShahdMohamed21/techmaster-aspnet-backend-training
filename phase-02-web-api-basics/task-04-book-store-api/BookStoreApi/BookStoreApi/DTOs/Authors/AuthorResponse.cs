using BookStoreApi.Models;
using Microsoft.Extensions.Options;
using System.Diagnostics.Metrics;

namespace BookStoreApi.DTOs.Authors
{
    public class AuthorResponse
    {
        public int AuthorId { get; set; }
        public string FullName { get; set; }
        public string Country { get; set; }
        public DateOnly? BirthDate { get; set; }
        public DateTime CreatedAt { get; set; }


    }
}
