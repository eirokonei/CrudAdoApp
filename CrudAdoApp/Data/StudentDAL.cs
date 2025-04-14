using System.Data;
using Microsoft.Data.SqlClient;
using CrudAdoApp.Models;

namespace CrudAdoApp.Data
{
    public class StudentDAL
    {
        private readonly string _connectionString;

        public StudentDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Student> GetAllStudents()
        {
            var list = new List<Student>();
            using SqlConnection con = new(_connectionString);
            SqlCommand cmd = new("sp_GetAllStudents", con) { CommandType = CommandType.StoredProcedure };
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new Student
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = dr["Name"].ToString(),
                    Email = dr["Email"].ToString(),
                    Age = Convert.ToInt32(dr["Age"])
                });
            }
            return list;
        }

        public Student GetStudentById(int id)
        {
            Student student = new();
            using SqlConnection con = new(_connectionString);
            SqlCommand cmd = new("sp_GetStudentById", con) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                student.Id = Convert.ToInt32(dr["Id"]);
                student.Name = dr["Name"].ToString();
                student.Email = dr["Email"].ToString();
                student.Age = Convert.ToInt32(dr["Age"]);
            }
            return student;
        }

        public void InsertStudent(Student student)
        {
            using SqlConnection con = new(_connectionString);
            SqlCommand cmd = new("sp_InsertStudent", con) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@Name", student.Name);
            cmd.Parameters.AddWithValue("@Email", student.Email);
            cmd.Parameters.AddWithValue("@Age", student.Age);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public void UpdateStudent(Student student)
        {
            using SqlConnection con = new(_connectionString);
            SqlCommand cmd = new("sp_UpdateStudent", con) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@Id", student.Id);
            cmd.Parameters.AddWithValue("@Name", student.Name);
            cmd.Parameters.AddWithValue("@Email", student.Email);
            cmd.Parameters.AddWithValue("@Age", student.Age);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public void DeleteStudent(int id)
        {
            using SqlConnection con = new(_connectionString);
            SqlCommand cmd = new("sp_DeleteStudent", con) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
