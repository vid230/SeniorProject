using System.ComponentModel.DataAnnotations;

namespace AubgAcademicTracker.Models
{
    public class WhatIfCourse
    {
        [Required(ErrorMessage = "Course name is required.")]
        public string Name { get; set; } = string.Empty;

        [Range(1, 10, ErrorMessage = "Credits must be between 1 and 10.")]
        public int Credits { get; set; }

        [Required(ErrorMessage = "Grade is required.")]
        public string Grade { get; set; } = string.Empty;
    }
}