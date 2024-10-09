using System.Data.SqlClient;
using WebApplicationTraining3.Entities;

namespace WebApplicationTraining3.DB
{
    public class SqlDB
    {
        private readonly string _connectionString = "Data Source=DESKTOP-5B2B64F\\SQLEXPRESS;Initial Catalog=webapitraining;Integrated Security=True;Encrypt=False";
        private readonly DBService _dbService;
        public SqlDB(DBService dbService)
        {
            _dbService = dbService;
        }
        public async Task<List<Student>> GetStudents()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();
                string sql = "SELECT * FROM students";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        Student student = new Student();
                        student.Id = reader.GetString(0);
                        student.FirstName = reader.GetString(1);
                        student.SecondName = reader.GetString(2);
                        student.LastName = reader.GetString(3);
                        student.DateOfBirth = reader.GetDateTime(4);
                        student.Gender = reader.GetString(5);
                        student.StreamId = reader.GetInt32(6);
                        student.RegistrationNumber = reader.GetString(7);
                        student.RegistrationDate = reader.GetDateTime(8);

                        students.Add(student);
                    }
                }
                sqlConnection.Close();
            }
            return students;
        }

        public async Task<bool> SaveStudent(Student student)
        {
            student.Id = GenerateStudentId().Result;
            student.RegistrationNumber = GetPrefix(student.StreamId).Result + student.Id;
            student.RegistrationDate = DateTime.Now;

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();
                string sql = "INSERT INTO students (" +
                    "Id," +
                    "FirstName, " +
                    "SecondName, " +
                    "LastName, " +
                    "DateOfBirth, " +
                    "Gender, " +
                    "StreamId, " +
                    "RegistrationNumber, " +
                    "RegistrationDate)" +
                    " VALUES (" +
                    "@Id," +
                    "@FirstName, " +
                    "@SecondName, " +
                    "@LastName, " +
                    "@DateOfBirth, " +
                    "@Gender, " +
                    "@StreamId, " +
                    "@RegistrationNumber, " +
                    "@RegistrationDate);";

                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", student.Id);
                    sqlCommand.Parameters.AddWithValue("@FirstName", student.FirstName);
                    sqlCommand.Parameters.AddWithValue("@SecondName", student.SecondName);
                    sqlCommand.Parameters.AddWithValue("@LastName", student.LastName);
                    sqlCommand.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                    sqlCommand.Parameters.AddWithValue("@Gender", student.Gender);
                    sqlCommand.Parameters.AddWithValue("@StreamId", student.StreamId);
                    sqlCommand.Parameters.AddWithValue("@RegistrationNumber", student.RegistrationNumber);
                    sqlCommand.Parameters.AddWithValue("@RegistrationDate", student.RegistrationDate);

                    int rows = await sqlCommand.ExecuteNonQueryAsync();
                    return rows > 0;
                }
            }
        }

        public async Task<string> GenerateStudentId()
        {
            string baseId = "00001";
            List<Student> students = await GetStudents();
            string currentYear = DateTime.Now.Year.ToString();

            if (students != null && students.Count > 0)
            {
                Student student = students.Last();
                string lastId = student.Id;
                string lastYr = lastId.Substring(0, 4);

                if (lastYr == currentYear)
                {
                    string lastIdNumber = lastId.Substring(4);
                    int newId = int.Parse(lastIdNumber) + 1;
                    baseId = newId.ToString().PadLeft(5, '0');
                }
                else
                {
                    baseId = "00001";
                }
            }
            string newIdNumber = $"{currentYear}{baseId}";
            return newIdNumber;
        }

        public async Task<string> GetPrefix(int id)
        {
            List<SubjectStream> subjects =  _dbService.GetAllStreamsAsync().Result;

            if (subjects != null && subjects.Count > 0)
            {
                SubjectStream? subject = subjects.Find(s => s.Id == id);
                if (subject != null)
                {
                    return subject.Prefix;
                }
            }
            return "";

        }
    }
}
