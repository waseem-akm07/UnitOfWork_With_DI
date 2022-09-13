using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Model;

namespace DataTransferObject
{
    public class SubjectModel
    {       
        //public SubjectModel(Subject subject)
        //{
        //    SubjectId = subject.SubjectId;
        //    SubjectName = subject.SubjectName;
        //}
               
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
    }
}
