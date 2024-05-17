using RepositoryPatternApi.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace RepositoryPatternAPI.Data.Entities
{
    public class Author
    {
        [Key]
        public int Id {  get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Surname { get; set; }
        public int BirthYear { get; set; }
        public virtual IList<AuthorBook> AuthorBooks { get; set; }
    }
}
