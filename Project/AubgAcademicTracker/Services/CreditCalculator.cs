using AubgAcademicTracker.Models;

namespace AubgAcademicTracker.Services
{
    public class CreditCalculator
    {
        public int CalculateAttemptedCredits(IEnumerable<Course> courses)
        {
            return courses.Sum(course => course.Credits);
        }

        public int CalculateEarnedCredits(IEnumerable<Course> courses)
        {
            return courses.Where(course => IsPassingGrade(course.Grade)).Sum(course => course.Credits);
        }

        public int CalculateCompletedCourses(IEnumerable<Course> courses)
        {
            return courses.Count(course => IsPassingGrade(course.Grade));
        }

        public bool IsPassingGrade(string grade)
        {
            switch (grade)
            {
                case "A":
                case "A-":
                case "B+":
                case "B":
                case "B-":
                case "C+":
                case "C":
                case "C-":
                case "D+":
                case "D":
                    return true;
                default:
                    return false;
            }
        }
    }
}
