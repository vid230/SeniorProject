using AubgAcademicTracker.Models;

namespace AubgAcademicTracker.Services
{
    public class TargetGpaSolver
    {
        private readonly GpaCalculator gpaCalculator;
        private readonly CreditCalculator creditCalculator;

        private readonly string[] possibleGrades =
        {
            "A",
            "A-",
            "B+",
            "B",
            "B-",
            "C+",
            "C",
            "C-",
            "D+",
            "D"
        };

        public TargetGpaSolver(GpaCalculator gpaCalculator, CreditCalculator creditCalculator)
        {
            this.gpaCalculator = gpaCalculator;
            this.creditCalculator = creditCalculator;
        }

        public TargetGpaResult Solve(IEnumerable<Course> currentCourses, IEnumerable<RemainingCourse> remainingCourses, double targetGpa)
        {
            if ((targetGpa < 0) || (targetGpa > 4.0))
            {
                throw new ArgumentOutOfRangeException(nameof(targetGpa), "Target GPA must be between 0.00 and 4.00.");
            }

            List<Course> currentCourseList = currentCourses.ToList();

            List<RemainingCourse> remainingCourseList = remainingCourses.ToList();

            int existingCredits = creditCalculator.CalculateAttemptedCredits(currentCourseList);

            double existingQualityPoints = currentCourseList.Sum(course => gpaCalculator.GetGradePoints(course.Grade) * course.Credits);

            int remainingCredits = remainingCourseList.Sum(course => course.Credits);

            int finalCredits = existingCredits + remainingCredits;

            TargetGpaResult result = new();

            if (finalCredits == 0)
            {
                return result;
            }

            List<GradeRecommendation> currentCombination = new();

            List<GradeRecommendation>? bestCombination = null;

            double bestNewQualityPoints = double.PositiveInfinity;

            double bestProjectedGpa = 0;

            long nodesVisited = 0;
            long branchesPruned = 0;

            // stores how many credits remain from each position onward.
            int[] remainingCreditsFromIndex = new int[remainingCourseList.Count + 1];

            for (int i = remainingCourseList.Count - 1; i >= 0; i--)
            {
                remainingCreditsFromIndex[i] = remainingCreditsFromIndex[i + 1] + remainingCourseList[i].Credits;
            }

            double highestGradePoints = gpaCalculator.GetGradePoints("A");

            double lowestPassingGradePoints = gpaCalculator.GetGradePoints("D");

            void SaveCombination(double newQualityPoints, double finalGpa, IEnumerable<GradeRecommendation> combination)
            {
                if (newQualityPoints >= bestNewQualityPoints)
                {
                    return;
                }

                bestNewQualityPoints = newQualityPoints;
                bestProjectedGpa = finalGpa;

                bestCombination = combination.Select(item =>
                new GradeRecommendation
                {
                    CourseName = item.CourseName,
                    Credits = item.Credits,
                    Grade = item.Grade
                }).ToList();
            }

            void Search(int courseIndex, double newQualityPoints)
            {
                nodesVisited++;

                // BASE CASE:
                // Every remaining course has received a grade.
                if (courseIndex == remainingCourseList.Count)
                {
                    double finalGpa = (existingQualityPoints + newQualityPoints) / finalCredits;

                    if (finalGpa >= targetGpa)
                    {
                        SaveCombination(newQualityPoints, finalGpa, currentCombination);
                    }

                    return;
                }

                int creditsStillUnassigned = remainingCreditsFromIndex[courseIndex];

                // PRUNING RULE 1:
                // Assume every remaining course receives an A.
                // If the target is still impossible, stop this branch.
                double bestPossibleAdditionalPoints = creditsStillUnassigned * highestGradePoints;
            }

            Search(0, 0);

            result.NodesVisited = nodesVisited;

            if (bestCombination != null)
            {
                result.IsAchievable = true;
                result.ProjectedGpa = bestProjectedGpa;
                result.Recommendations = bestCombination;
            }

            return result;
        }
    }
}