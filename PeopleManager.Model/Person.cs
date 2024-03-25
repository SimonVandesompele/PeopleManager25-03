using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PeopleManager.Model
{
    [Table(nameof(Person))]
    public class Person
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First name")]
        public required string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public required string LastName { get; set; }

        [EmailAddress]
        [Display(Name = "Email address")]
        public string? Email { get; set; }

        [JsonIgnore]
        public IList<Assignment> Assignments { get; set; } = new List<Assignment>();
    }
}
