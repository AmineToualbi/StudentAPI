using StudentAPI.Models;
using StudentAPI.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentAPI.Controllers
{
    public class StudentController : ApiController
    {

        private SqlConnection sqlConnection;
        private SqlDataAdapter sqlAdapter;
        private StudentRepository studentRepository;

        public StudentController()
        {
            this.studentRepository = new StudentRepository();
        }


        // GET: api/Student
        public IEnumerable<Student> Get()
        {
            return studentRepository.GetAllStudents();
        }

        // GET: api/Student/5
        public Student Get(int id)
        {
            return studentRepository.GetStudent(id);
        }

        [Route("api/Student/GetScore/{firstName}/{lastName}")]
        [HttpGet]
        public string GetScore(string firstName, string lastName)
        {
            return studentRepository.GetScore(firstName, lastName);
        }

        [Route("api/Student/GetBestStudent")]
        [HttpGet]
        public Student GetBestStudent()
        {
            return studentRepository.GetBestStudent();
        }

        // POST: api/Student
        public HttpResponseMessage Post([FromBody] Student value)
        {
            studentRepository.InsertStudent(value);
            var response = Request.CreateResponse<Student>(System.Net.HttpStatusCode.Created, value);
            return response;
        }

        // PUT: api/Student/5
        public HttpResponseMessage Put(int id, [FromBody] Student value)
        {
            studentRepository.UpdateStudent(id, value);
            var response = Request.CreateResponse<Student>(System.Net.HttpStatusCode.Created, value);
            return response;
        }

        // DELETE: api/Student/5
        public string Delete(int id)
        {
            return studentRepository.DeleteStudent(id);
        }
    }
}
