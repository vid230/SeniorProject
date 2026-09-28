using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AubgAcademicTracker.Models
{
    public class Semester
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Term is required.")]
        public string Term { get; set; } = string.Empty;

        [Range(2000, 2100, ErrorMessage = "Enter a valid year.")]
        public int Year { get; set; }

        public List<Course> Courses { get; set; } = new();

        [NotMapped]
        public string DisplayName => $"{Term} {Year}";
    }
}