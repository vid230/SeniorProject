using AubgAcademicTracker.Models;
using AubgAcademicTracker.Services;

namespace AubgAcademicTracker.Tests
{
    public class CoursePlannerServiceTests
    {
        [Fact]
        public void CreatePlan_WithSimpleDependencyChain_ReturnsValidOrder()
        {
            // Arrange
            var service = new CoursePlannerService();

            var courses = new List<PlannedCourse>
            {
                new PlannedCourse
                {
                    Code = "COS120",
                    Name = "Course 1",
                    Credits = 3
                },

                new PlannedCourse
                {
                    Code = "COS220",
                    Name = "Course 2",
                    Credits = 3
                },

                new PlannedCourse
                {
                    Code = "COS320",
                    Name = "Course 3",
                    Credits = 3
                }
            };

            var relations = new List<PrerequisiteRelation>
            {
                new PrerequisiteRelation
                {
                    PrerequisiteCode = "COS120",
                    CourseCode = "COS220"
                },

                new PrerequisiteRelation
                {
                    PrerequisiteCode = "COS220",
                    CourseCode = "COS320"
                }
            };

            // Act
            CoursePlanResult result = service.CreatePlan(courses, relations);

            // Assert
            Assert.True(result.Success);
            Assert.False(result.HasCycle);

            Assert.Equal(3, result.OrderedCourses.Count);

            Assert.Equal("COS120", result.OrderedCourses[0].Code);

            Assert.Equal("COS220", result.OrderedCourses[1].Code);

            Assert.Equal("COS320", result.OrderedCourses[2].Code);
        }

        [Fact]
        public void CreatePlan_WithMultiplePrerequisites_RespectsAllDependencies()
        {
            // Arrange
            var service = new CoursePlannerService();

            var courses = new List<PlannedCourse>
            {
                new PlannedCourse
                {
                    Code = "COS120",
                    Name = "Intro",
                    Credits = 3
                },

                new PlannedCourse
                {
                    Code = "COS220",
                    Name = "Course A",
                    Credits = 3
                },

                new PlannedCourse
                {
                    Code = "COS210",
                    Name = "Course B",
                    Credits = 3
                },

                new PlannedCourse
                {
                    Code = "COS320",
                    Name = "Advanced Course",
                    Credits = 3
                }
            };

            var relations = new List<PrerequisiteRelation>
            {
                new PrerequisiteRelation
                {
                    PrerequisiteCode = "COS120",
                    CourseCode = "COS220"
                },

                new PrerequisiteRelation
                {
                    PrerequisiteCode = "COS120",
                    CourseCode = "COS210"
                },

                new PrerequisiteRelation
                {
                    PrerequisiteCode = "COS220",
                    CourseCode = "COS320"
                },

                new PrerequisiteRelation
                {
                    PrerequisiteCode = "COS210",
                    CourseCode = "COS320"
                }
            };

            // Act
            CoursePlanResult result = service.CreatePlan(courses, relations);

            // Assert
            Assert.True(result.Success);
            Assert.False(result.HasCycle);

            List<string> order = result.OrderedCourses.Select(course => course.Code).ToList();

            int cos120 = order.IndexOf("COS120");

            int cos220 = order.IndexOf("COS220");

            int cos210 = order.IndexOf("COS210");

            int cos320 = order.IndexOf("COS320");

            Assert.True(cos120 < cos220);
            Assert.True(cos120 < cos210);

            Assert.True(cos220 < cos320);
            Assert.True(cos210 < cos320);
        }

        [Fact]
        public void CreatePlan_WithCycle_DetectsCycle()
        {
            // Arrange
            var service = new CoursePlannerService();

            var courses = new List<PlannedCourse>
            {
                new PlannedCourse
                {
                    Code = "COS120",
                    Name = "Course 1",
                    Credits = 3
                },

                new PlannedCourse
                {
                    Code = "COS220",
                    Name = "Course 2",
                    Credits = 3
                },

                new PlannedCourse
                {
                    Code = "COS320",
                    Name = "Course 3",
                    Credits = 3
                }
            };

            var relations = new List<PrerequisiteRelation>
            {
                new PrerequisiteRelation
                {
                    PrerequisiteCode = "COS120",
                    CourseCode = "COS220"
                },

                new PrerequisiteRelation
                {
                    PrerequisiteCode = "COS220",
                    CourseCode = "COS320"
                },

                new PrerequisiteRelation
                {
                    PrerequisiteCode = "COS320",
                    CourseCode = "COS120"
                }
            };

            // Act
            CoursePlanResult result = service.CreatePlan(courses, relations);

            // Assert
            Assert.False(result.Success);
            Assert.True(result.HasCycle);
        }

        [Fact]
        public void CreatePlan_WithNoPrerequisites_ReturnsAllCourses()
        {
            // Arrange
            var service = new CoursePlannerService();

            var courses = new List<PlannedCourse>
            {
                new PlannedCourse
                {
                    Code = "COS100",
                    Name = "Course 1",
                    Credits = 3
                },

                new PlannedCourse
                {
                    Code = "MAT100",
                    Name = "Course 2",
                    Credits = 3
                },

                new PlannedCourse
                {
                    Code = "ENG100",
                    Name = "Course 3",
                    Credits = 3
                }
            };

            var relations = new List<PrerequisiteRelation>();

            // Act
            CoursePlanResult result = service.CreatePlan(courses, relations);

            // Assert
            Assert.True(result.Success);
            Assert.False(result.HasCycle);

            Assert.Equal(3, result.OrderedCourses.Count);
        }

        [Fact]
        public void CreatePlan_WithNoCourses_ReturnsUnsuccessfulResult()
        {
            // Arrange
            var service = new CoursePlannerService();

            var courses = new List<PlannedCourse>();

            var relations = new List<PrerequisiteRelation>();

            // Act
            CoursePlanResult result = service.CreatePlan(courses, relations);

            // Assert
            Assert.False(result.Success);

            Assert.Equal("Add at least one planned course.", result.Message);
        }
    }
}