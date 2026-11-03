using System.ComponentModel.DataAnnotations;

namespace AubgAcademicTracker.Models
{
    public class PlannedCourse
    {
        [Required(ErrorMessage = "Course code is required.")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Course name is required.")]
        public string Name { get; set; } = string.Empty;

        [Range(1, 10, ErrorMessage = "Credits must be between 1 and 10.")]
        public int Credits { get; set; }
    }
}
