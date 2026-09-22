using AubgAcademicTracker.Models;

namespace AubgAcademicTracker.Services
{
    public class GpaCalculator
    {
        public double CalculateGpa(IEnumerable<Course> courses)
        {
            List<Course> courseList = courses.ToList();

            int attemptedCredits = courseList.Sum(course => course.Credits);

            if (attemptedCredits == 0)
            {
                return 0;
            }

            double qualityPoints = courseList.Sum(course => GetGradePoints(course.Grade) * course.Credits);

            return qualityPoints / attemptedCredits;
        }

        public double GetGradePoints(string grade)
        {
            return grade switch
            {
                "A" => 4.0,
                "A-" => 3.7,
                "B+" => 3.3,
                "B" => 3.0,
                "B-" => 2.7,
                "C+" => 2.3,
                "C" => 2.0,
                "C-" => 1.7,
                "D+" => 1.3,
                "D" => 1.0,
                "F" => 0.0,
                _ => throw new ArgumentException("Invalid grade.")
            };
        }
    }
}