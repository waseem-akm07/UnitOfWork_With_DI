using System.Collections.Generic;

namespace DataTransferObject
{
    public class StudentGetModel
    {
        public StudentModel StudentModel { get; set; }
        public ClassModel ClassModel { get; set; }
        public List<SubjectModel> SubjectModel { get; set; }
    }
   
}
