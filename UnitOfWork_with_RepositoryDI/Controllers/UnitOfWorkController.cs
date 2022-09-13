using System.Collections.Generic;
using System.Web.Http;
using UnitOfWork;
using DataAccessLayer.Model;
using DataTransferObject;
using System;
using System.Web;

namespace UnitOfWork_with_RepositoryDI.Controllers
{
    public class UnitOfWorkController : ApiController
    {
        private UnitOfWorkRepo _unitOfWork = new UnitOfWorkRepo();
        //UnitOfWorkRepo _unitOfWork = new UnitOfWorkRepo(new MvcdbEntities());


        /// <summary>
        /// To get all data of Students
        /// </summary>
        /// <returns> Student data </returns>
        // GET: api/UnitOfWork
        [HttpGet]
        [Route("api/unitofwork/getstudents")]
        public IEnumerable<StudentGetModel> GetStudents()
        {
            IEnumerable<StudentGetModel> data = new List<StudentGetModel>();
            data = _unitOfWork.BusinessLayer.GetStudents();
            return data;
        }


        /// <summary>
        /// To get specific Student data
        /// </summary>
        /// <param name="id"> Student id </param>
        /// <returns> Specific student data </returns>
        // GET: api/UnitOfWork/5
        [HttpGet]
        [Route("api/unitofwork/getstudent/{id}")]
        public IEnumerable<StudentGetModel> Get(int id)
        {
            IEnumerable<StudentGetModel> data = new List<StudentGetModel>();

            data = _unitOfWork.BusinessLayer.GetStudent(id);
            return data;
        }


        /// <summary>
        /// To add student Data
        /// </summary>
        /// <param name="studentsModel"> To fetch student data </param>
        // POST: api/UnitOfWork
        [HttpPost]
        [Route("api/unityofwork/poststudent")]
        public void Post(StudentsModel studentsModel)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.BusinessLayer.CreateStudent(studentsModel);
                _unitOfWork.Commit();

                if (HttpContext.Current != null && HttpContext.Current.Error != null)
                    _unitOfWork.Rollback();
                else
                    _unitOfWork.Commit();
            }
        }


        /// <summary>
        /// To Update specific student data
        /// </summary>
        /// <param name="id"> Specific Student id </param>
        /// <param name="studentsModel"> To fetch student data </param>
        // PUT: api/UnitOfWork/5
        [HttpPut]
        [Route("api/unityofwork/putstudent/{id}")]
        public void Put(int id, StudentsModel studentsModel)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.BusinessLayer.UpdateStudent(id, studentsModel);
              
                if (HttpContext.Current != null && HttpContext.Current.Error != null)
                    _unitOfWork.Rollback();
                else
                    _unitOfWork.Commit();
            }
        }


        /// <summary>
        /// To Delete specific student
        /// </summary>
        /// <param name="id"> Specific Student id </param>
        // DELETE: api/UnitOfWork/5
        [HttpDelete]
        [Route("api/unityofwork/delete/{id}")]
        public void Delete(int id)
        {
            _unitOfWork.BusinessLayer.DeleteStudent(id);
           
            if (HttpContext.Current != null && HttpContext.Current.Error != null)
                _unitOfWork.Rollback();
            else
                _unitOfWork.Commit();
        }

        //protected override void Dispose(bool disposing)
        //{
        //    _unitOfWork.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}
