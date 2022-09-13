using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using BusinessAccessLayer.Repository;
using DataAccessLayer.Model;
using DataTransferObject;


namespace BusinessAccessLayer
{
    public class BusinessLayer : IBusinessLayer
    {
        private MvcdbEntities _entities;
        private MvcdbEntities _context = new MvcdbEntities();

        /// <summary>
        /// Constructore to initialize the DbContext
        /// </summary>
        /// <param name="entities"> DbContext </param>
        public BusinessLayer(MvcdbEntities entities)
        {
            _entities = entities;
        }


        /// <summary>
        /// To fetch all student
        /// </summary>
        /// <returns> Student data </returns>
        public IEnumerable<StudentGetModel> GetStudents()
        {
            try
            {
                var data = _entities.Students
                              .Include(x => x.Class)
                              .Include(x => x.Mappings.Select(s => s.Student))
                              .AsEnumerable()
                              .Select(x => new StudentGetModel
                              {
                                  StudentModel = new StudentModel
                                  {
                                      StudentId = x.StudentId,
                                      StudentName = x.StudentName,
                                      StudentAddress = x.StudentAddress,
                                      StudentPhone = x.StudentPhone
                                  },
                                  ClassModel = new ClassModel
                                  {
                                      ClassId = x.Class.ClassId,
                                      ClassName = x.Class.ClassName
                                  },
                                  SubjectModel = parseStudentSubjects(x.Mappings.ToList())
                              }).ToList();

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To add Subject in a list
        /// </summary>
        /// <param name="subjects"> Get List of Subject </param>
        /// <returns> Subject List </returns>
        private List<SubjectModel> parseStudentSubjects(List<Mapping> subjects)
        {
            List<SubjectModel> _subject = new List<SubjectModel>();
            foreach (var sub in subjects)
            {
                SubjectModel subject = new SubjectModel()
                {
                    SubjectId = sub.Subject.SubjectId,
                    SubjectName = sub.Subject.SubjectName
                };
                _subject.Add(subject);
            }
            return _subject;
        }

        /// <summary>
        /// To fetch specific Student
        /// </summary>
        /// <param name="id"> Specific Student id </param>
        /// <returns> Specific Student data </returns>
        public IEnumerable<StudentGetModel> GetStudent(int id)
        {
            try
            {                
                var data = _entities.Students
                              .Include(x => x.Class)
                              .Include(x => x.Mappings.Select(s => s.Student))
                              .AsEnumerable()
                              .Where(x=>x.StudentId == id)
                              .Select(x => new StudentGetModel
                              {
                                  StudentModel = new StudentModel
                                  {
                                      StudentId = x.StudentId,
                                      StudentName = x.StudentName,
                                      StudentAddress = x.StudentAddress,
                                      StudentPhone = x.StudentPhone
                                  },
                                  ClassModel = new ClassModel
                                  {
                                      ClassId = x.Class.ClassId,
                                      ClassName = x.Class.ClassName
                                  },
                                  SubjectModel = parseStudentSubjects(x.Mappings.ToList())
                              }).ToList();
                              
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To add new Student 
        /// </summary>
        /// <param name="studentModel"> To fetch student data </param>
        public void CreateStudent(StudentsModel studentModel)
        {
            #region
            /* Checking that class is exist in class table which is assign to user
             * If exist then get that class id
             * If not exist then add new class in class table
             */
            Class @class = new Class();
            var cls = _entities.Classes.Where(x => x.ClassName == studentModel.ClassName).FirstOrDefault();
            var ClassId = 0;
            // If cls is null then add new class in Class table  
            if (cls == null)
            {
                @class.ClassName = studentModel.ClassName;
                _entities.Classes.Add(@class);
                Save();
                ClassId = @class.ClassId;
            }
            else
            { 
                ClassId = cls.ClassId;
            }
            #endregion

            #region
            // for add new student in Student table 
            Student student = new Student();
            student.StudentName = studentModel.StudentName;
            student.StudentAddress = studentModel.StudentAddress;
            student.StudentPhone = studentModel.StudentPhone;
            student.StudentClassId = ClassId;
            _entities.Students.Add(student);
            Save();
            var StudentId = student.StudentId;
            #endregion

            #region
            /* Checking that subject is exist in Subject table which is assign to user
             * If exist then get that subject id
             * If not exist then add new subject in subject table
             */
            Subject subject = new Subject();
            var sub = _entities.Subjects.Where(x => x.SubjectName == studentModel.SubjectName).FirstOrDefault();
            var SubjectId = 0;
            // If sub is null then add new class in subject table
            if (sub == null)
            {
                subject.SubjectName = studentModel.SubjectName;
                _entities.Subjects.Add(subject);
                Save();
                SubjectId = subject.SubjectId;
            }
            else
            {
                SubjectId = sub.SubjectId;
            }
            #endregion

            #region
            // for assign new subject to student by mapping 
            Mapping mapping = new Mapping();
            mapping.MapStudentId = StudentId;
            mapping.MapSubjectId = SubjectId;
            _entities.Mappings.Add(mapping);
            #endregion

            Save();

        }

        /// <summary>
        /// To update specific Student
        /// </summary>
        /// <param name="id"> Specific student id </param>
        /// <param name="studentModel"> to fetch specific student data </param>
        public void UpdateStudent(int id, StudentsModel studentModel)
        {

            //try
            //{ 
                //var iid = "";
                //Guid guid = Guid.NewGuid();
                //iid = Convert.ToString(guid);

                #region MainRegion for update Student
                var student = _entities.Students.Where(x => x.StudentId == id).FirstOrDefault();
                student.StudentName = studentModel.StudentName;
                student.StudentAddress = studentModel.StudentAddress;
                student.StudentPhone = studentModel.StudentPhone;
                _entities.Entry(student).State = EntityState.Modified;
                #endregion

                #region for update Class
                var @class = _entities.Classes.Where(x => x.ClassId == student.StudentClassId).FirstOrDefault();
                @class.ClassName = studentModel.ClassName;
                _entities.Entry(@class).State = EntityState.Modified;
                #endregion

                #region for subject Mapping
                var map = _entities.Mappings.Where(x => x.MapStudentId == student.StudentId).FirstOrDefault();
                var subject = _entities.Subjects.Where(x => x.SubjectId == map.MapSubjectId).FirstOrDefault();
                subject.SubjectName = studentModel.SubjectName;
                _entities.Entry(subject).State = EntityState.Modified;
                #endregion
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        /// <summary>
        /// To delete specific student
        /// </summary>
        /// <param name="id"> specific student id </param>
        public void DeleteStudent(int id)
        {
            // get specific student to delete it           
            var data = _entities.Students.Where(x => x.StudentId == id).FirstOrDefault();
            _entities.Entry(data).State = EntityState.Deleted;
           
        }

        /// <summary>
        /// To save data in DbContext
        /// </summary>
        public void Save()
        {
            // to save data in database 
            _entities.SaveChanges();
        }
    }
}
