using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using AzureWepAppWithDb.Models;

namespace Sqlapp.Services
{
    public class CourseService
    {
       
        private SqlConnection GetConnection(string ConnectionString)
        {
            // Here we are creating the SQL connection
            return new SqlConnection(ConnectionString);
        }

        public IEnumerable<Course> GetCourses(string ConnectionString)
        {
            List<Course> _lst = new List<Course>();
            string _statement = "SELECT CourseID,CourseName,rating from Course";
            SqlConnection _connection = GetConnection(ConnectionString);
            // Let's open the connection
            _connection.Open();
            // We then construct the statement of getting the data from the Course table
            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);
            // Using the SqlDataReader class , we will read all the data from the Course table
            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    Course _course = new Course()
                    {
                        CourseId = _reader.GetInt32(0),
                        CourseName = _reader.GetString(1),
                        rating = _reader.GetDecimal(2)
                    };

                    _lst.Add(_course);
                }
            }
            _connection.Close();
            return _lst;
        }

    }
}


