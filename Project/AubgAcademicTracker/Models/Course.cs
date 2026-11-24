using System.ComponentModel.DataAnnotations;

namespace AubgAcademicTracker.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Course code is required.")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Course name is required.")]
        public string Name { get; set; } = string.Empty;

        [Range(0, 4, ErrorMessage = "Credits must be between 0 and 4.")]
        public int Credits { get; set; }

        [Required(ErrorMessage = "Grade is required.")]
        public string Grade { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Semester is required.")]
        public int SemesterId { get; set; }

        public Semester Semester { get; set; } = null!;
    }
}