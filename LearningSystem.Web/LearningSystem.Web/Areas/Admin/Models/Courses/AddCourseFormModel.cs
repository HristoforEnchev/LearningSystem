namespace LearningSystem.Web.Areas.Admin.Models.Courses
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class AddCourseFormModel
    {
        [Required]
        [MinLength(CourseNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(CourseDescriptionMaxLength)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string TrainerId { get; set; }
    }
}
