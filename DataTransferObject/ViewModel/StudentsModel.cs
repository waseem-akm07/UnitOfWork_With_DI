using System.ComponentModel.DataAnnotations;

namespace DataTransferObject
{
    public class StudentsModel
    {
        public int StudentId { get; set; }
        [Required]
        public string StudentName { get; set; }
        [Required]
        public string StudentAddress { get; set; }
        [Required]
        public string StudentPhone { get; set; }
        public int ClassId { get; set; }
        [Required]
        public string ClassName { get; set; }
        public int SubjectId { get; set; }
        [Required]
        public string SubjectName { get; set; }
    }
    
}
