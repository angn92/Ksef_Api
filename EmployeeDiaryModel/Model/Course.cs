using EmployeeDiaryModel.Enums;
using JetBrains.Annotations;

namespace EmployeeDiaryModel.Model
{
    public class Course
    {
        public Guid Id { get; set; }
        public string CourseName { get; set; }
        public Status CourseStatus { get; set; }

        public Course()
        {
        }

        public Course([NotNull] string courseName, [NotNull] Status status)
        {
            Id = Guid.NewGuid();
            CourseName = courseName;
            CourseStatus = status;
        }
    }
}
