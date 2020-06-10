using StudentAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace StudentAPI.Services
{
    public class StudentRepository
    {

        private SqlConnection sqlConnection;
        private SqlDataAdapter sqlAdapter;

        public StudentRepository()
        {
            sqlConnection = new SqlConnection();
            sqlAdapter = new SqlDataAdapter();
        }


        public List<Student> GetAllStudents()
        {
            // = new SqlConnection("data source=desktop-j4fslug;Initial catalog=StudentDB; user id =username; password =password");
            sqlConnection.ConnectionString = "Data Source = DESKTOP-J4FSLUG; Initial Catalog = StudentDB; User ID = username; Password = password";
            DataTable dataTable = new DataTable();
            var query = "SELECT * FROM Students";
            /* sqlAdapter = new SqlDataAdapter
             {
                 SelectCommand = new SqlCommand(query, sqlConnection)
             };;*/
            sqlAdapter.SelectCommand = new SqlCommand(query, sqlConnection);
            sqlAdapter.Fill(dataTable);
            List<Student> students = new List<Student>(dataTable.Rows.Count);
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow studentRecord in dataTable.Rows)
                {
                    Student currentStudent = ReadStudent(studentRecord);
                    students.Add(currentStudent);
                }
            }
            return students; 
        }


        public Student GetStudent(int studentID)
        {
            sqlConnection.ConnectionString = "Data Source = DESKTOP-J4FSLUG; Initial Catalog = StudentDB; User ID = username; Password = password";
            DataTable dataTable = new DataTable();
            var query = "SELECT * FROM Students WHERE StudentID = " + studentID;
            sqlAdapter.SelectCommand = new SqlCommand(query, sqlConnection);
            sqlAdapter.Fill(dataTable);
            List<Student> students = new List<Student>(dataTable.Rows.Count);
            if (dataTable.Rows.Count != 1)
            {
                return null;
            }
            return ReadStudent(dataTable.Rows[0]);
        }

        public Student GetBestStudent()
        {
            sqlConnection.ConnectionString = "Data Source = DESKTOP-J4FSLUG; Initial Catalog = StudentDB; User ID = username; Password = password";
            DataTable dataTable = new DataTable();
            var query = "SELECT * FROM Students Order By Score DESC";
            sqlAdapter.SelectCommand = new SqlCommand(query, sqlConnection);
            sqlAdapter.Fill(dataTable);
            List<Student> students = new List<Student>(dataTable.Rows.Count);
            if (dataTable.Rows.Count < 1)
            {
                return null;
            }
            return ReadStudent(dataTable.Rows[0]);
        }

        //Assume that every firstName - LastName combination is unique. 
        public string GetScore(string firstName, string lastName)
        {
            sqlConnection.ConnectionString = "Data Source = DESKTOP-J4FSLUG; Initial Catalog = StudentDB; User ID = username; Password = password";
            DataTable dataTable = new DataTable();
            var query = "SELECT score FROM Students WHERE FirstName = '" + firstName + "' AND LastName = '" + lastName + "'";
            sqlAdapter.SelectCommand = new SqlCommand(query, sqlConnection);
            sqlAdapter.Fill(dataTable);
            List<Student> students = new List<Student>(dataTable.Rows.Count);
            if (dataTable.Rows.Count != 1)
            {
                return "Student record not found.";
            }
            return dataTable.Rows[0]["score"].ToString();
        }


        public string InsertStudent(Student student)
        {
            sqlConnection.ConnectionString = "Data Source = DESKTOP-J4FSLUG; Initial Catalog = StudentDB; User ID = username; Password = password";
            var query = "INSERT INTO Students (FirstName, MiddleName, LastName, Address, BirthDate, Score, DepartmentID)" +
                " VALUES (@FirstName, @MiddleName, @LastName, @Address, @BirthDate, @Score, @DepartmentID)";
            SqlCommand insertCommand = new SqlCommand(query, sqlConnection);
            insertCommand.Parameters.AddWithValue("@FirstName", student.FirstName);
            insertCommand.Parameters.AddWithValue("@MiddleName", student.MiddleName);
            insertCommand.Parameters.AddWithValue("@LastName", student.LastName);
            insertCommand.Parameters.AddWithValue("@Address", student.Address);
            insertCommand.Parameters.AddWithValue("@BirthDate", student.BirthDate);
            insertCommand.Parameters.AddWithValue("@Score", student.Score);
            insertCommand.Parameters.AddWithValue("@DepartmentID", student.DepartmentID);
            sqlConnection.Open();
            int result = insertCommand.ExecuteNonQuery();
            if (result > 0)
            {
                return "Student inserted correctly.";
            }
            else
            {
                return "Error during Student insertion.";
            }
        }


        public string UpdateStudent(int studentID, Student student)
        {
            sqlConnection.ConnectionString = "Data Source = DESKTOP-J4FSLUG; Initial Catalog = StudentDB; User ID = username; Password = password";
            var query = "UPDATE Students SET FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, " +
                "Address = @Address, BirthDate = @BirthDate, Score = @Score, DepartmentID = @DepartmentID WHERE StudentID = " + studentID;
            SqlCommand updateCommand = new SqlCommand(query, sqlConnection);
            updateCommand.Parameters.AddWithValue("@FirstName", student.FirstName);
            updateCommand.Parameters.AddWithValue("@MiddleName", student.MiddleName);
            updateCommand.Parameters.AddWithValue("@LastName", student.LastName);
            updateCommand.Parameters.AddWithValue("@Address", student.Address);
            updateCommand.Parameters.AddWithValue("@BirthDate", student.BirthDate);
            updateCommand.Parameters.AddWithValue("@Score", student.Score);
            updateCommand.Parameters.AddWithValue("@DepartmentID", student.DepartmentID);
            sqlConnection.Open();
            int result = updateCommand.ExecuteNonQuery();
            if (result > 0)
            {
                return "Student updated correctly.";
            }
            else
            {
                return "Error during Student update.";
            }
        }


        public string DeleteStudent(int studentID)
        {
            sqlConnection.ConnectionString = "Data Source = DESKTOP-J4FSLUG; Initial Catalog = StudentDB; User ID = username; Password = password";
            var query = "DELETE FROM Students WHERE StudentID = " + studentID;
            SqlCommand deleteCommand = new SqlCommand(query, sqlConnection);
            sqlConnection.Open();
            int result = deleteCommand.ExecuteNonQuery();
            if (result > 0)
            {
                return "Student deleted successfully.";
            }
            else
            {
                return "Student could not be deleted.";
            }
        }


        private Student ReadStudent(DataRow studentRecord)
        {
            int StudentID = Convert.ToInt32(studentRecord["StudentID"]);
            var FirstName = studentRecord["FirstName"].ToString();
            var MiddleName = studentRecord["MiddleName"].ToString();
            var LastName = studentRecord["LastName"].ToString();
            var Address = studentRecord["Address"].ToString();
            var BirthDate = studentRecord["BirthDate"].ToString();
            var Score = studentRecord["Score"].ToString();
            int DepartmentID = Convert.ToInt32(studentRecord["DepartmentID"]);
            return new Student { StudentID = StudentID, FirstName = FirstName, MiddleName = MiddleName, LastName = LastName, Address = Address, BirthDate = BirthDate, Score = Score, DepartmentID = DepartmentID };
        }

    }
}