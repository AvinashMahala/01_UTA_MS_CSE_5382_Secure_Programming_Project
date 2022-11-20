using System.ComponentModel.DataAnnotations;
using PhoneBook.CustomAttributes;

namespace PhoneBook.Model
{
    public class PhoneBookEntry
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        [PhoneMask]
        public string? PhoneNumber { get; set; }
    }
}