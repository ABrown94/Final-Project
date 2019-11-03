using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Models
{
        [MetadataTypeAttribute(typeof(Course.CourseMetadata))]
        public partial class Course
        {
            internal sealed class CourseMetadata
            {
                [Required]
                [Display(Name = "Course Name")]
                public string CourseName { get; set; }
            }
        }

        [MetadataTypeAttribute(typeof(Course_Detail.CourseDetailMetadata))]
        public partial class Course_Detail
        {
            internal sealed class CourseDetailMetadata
            {
                [Required]
                [DataType(DataType.Date)]
                //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
                //[Range(typeof(DateTime), "01/01/2017", "01/01/2020", ErrorMessage = "Please enter a valid date")]
                [Display(Name = "Start Date")]
                public DateTime StartDate { get; set; }

                [Required]
                [DataType(DataType.Date)]
                //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
                //[Range(typeof(DateTime), "01/01/2017", "01/01/2020", ErrorMessage = "Please enter a valid date")]
                [Display(Name = "End Date")]
                public DateTime EndDate { get; set; }

                [Required]
                [Display(Name = "Price")]
                public decimal Price { get; set; }

                [Required]
                [Display(Name = "Teacher")]
                public int TeacherId { get; set; }
            }
        }
        [MetadataTypeAttribute(typeof(Course_Schedule.CourseScheduleMetadata))]
        public partial class Course_Schedule
        {
            internal sealed class CourseScheduleMetadata
            {
                [Required]
                [Display(Name = "Day of Week")]
                public string DayOfWeek { get; set; }

                [Required]
                //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
                //[RegularExpression(@"^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time.")]
                //[DataType(DataType.Time)]
                [Display(Name = "Start Time")]
                public DateTime StartTime { get; set; }

                [Required]
                //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
                //[RegularExpression(@"^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time.")]
                //[DataType(DataType.Time)]
                [Display(Name = "End Time")]
                public DateTime EndTime { get; set; }

                //    [Required]
                //    [RegularExpression(@"^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time.")]
                //    public TimeSpan StartTime { get; set; }

                //    [Required]
                //    [RegularExpression(@"^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time.")]
                //    public TimeSpan EndTime { get; set; }
  
            }
        }  
    }
