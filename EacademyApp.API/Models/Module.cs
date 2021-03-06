using System;

namespace EacademyApp.API.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Boolean HasFileAttachment { get; set; }
        public string AssignmentName { get; set; }
        public Boolean HasAssignment { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}