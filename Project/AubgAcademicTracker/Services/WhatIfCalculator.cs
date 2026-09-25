using AubgAcademicTracker.Models;

namespace AubgAcademicTracker.Services
{
    public class WhatIfCalculator
    {
        private readonly GpaCalculator gpaCalculator;
        private readonly CreditCalculator creditCalculator;

        public WhatIfCalculator(GpaCalculator gpaCalculator, CreditCalculator creditCalculator)
        {
            this.gpaCalculator = gpaCalculator;
            this.creditCalculator = creditCalculator;
        }

        public double CalculateProjectedGpa(IEnumerable<Course> currentCourses, IEnumerable<WhatIfCourse> hypotheticalCourses)
        {
            List<Course> currentCourseList = currentCourses.ToList();
            List<WhatIfCourse> hypotheticalCourseList = hypotheticalCourses.ToList();

            int currentCredits = creditCalculator.CalculateAttemptedCredits(currentCourseList);

            int hypotheticalCredits = hypotheticalCourseList.Sum(course => course.Credits);

            int totalCredits = currentCredits + hypotheticalCredits;

            if (totalCredits == 0)
            {
                return 0;
            }

            double currentQualityPoints = currentCourseList.Sum(course => gpaCalculator.GetGradePoints(course.Grade) * course.Credits);

            double hypotheticalQualityPoints = hypotheticalCourseList.Sum(course => gpaCalculator.GetGradePoints(course.Grade) * course.Credits);

            return (currentQualityPoints + hypotheticalQualityPoints) / totalCredits;
        }
    }
}