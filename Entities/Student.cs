using System.IO;

namespace WebApplicationTraining3.Entities
{
    public class Student
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public int StreamId { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime RegistrationDate { get; set; }

    }
}
