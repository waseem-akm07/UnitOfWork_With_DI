using System.Collections.Generic;
using DataTransferObject;

namespace BusinessAccessLayer.Repository
{
    public interface IBusinessLayer
    {
        /// <summary>
        /// To fetch all student
        /// </summary>
        /// <returns> Student data </returns>
        IEnumerable<StudentGetModel> GetStudents();

        /// <summary>
        /// To fetch specific Student
        /// </summary>
        /// <param name="id"> Specific Student id </param>
        /// <returns> Specific Student data </returns>
        IEnumerable<StudentGetModel> GetStudent(int id);

        /// <summary>
        /// To add new Student 
        /// </summary>
        /// <param name="studentModel"> To fetch student data </param>
        void CreateStudent(StudentsModel studentModel);

        /// <summary>
        /// To update specific Student
        /// </summary>
        /// <param name="id"> Specific student id </param>
        /// <param name="studentModel"> to fetch specific student data </param>
        void UpdateStudent(int id, StudentsModel studentModel);

        /// <summary>
        /// To delete specific student
        /// </summary>
        /// <param name="id"> specific student id </param>
        void DeleteStudent(int id);
    }
}
