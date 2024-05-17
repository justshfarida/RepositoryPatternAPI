using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternApi.Domain.Entities
{
    public class Book
    {
        [Key]
        public int ISBN { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        public virtual IList<AuthorBook> AuthorBooks { get; set;}

    }
}
