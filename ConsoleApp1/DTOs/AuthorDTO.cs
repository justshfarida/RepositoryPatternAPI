using System.ComponentModel.DataAnnotations;

namespace RepositoryPatternAPI.DTOs
{
    public class AuthorDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int BirthYear { get; set; }
    }
}
