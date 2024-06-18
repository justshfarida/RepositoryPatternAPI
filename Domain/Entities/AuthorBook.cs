using RepositoryPatternAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternApi.Domain.Entities
{
    public class AuthorBook
    {
        public int ISBN { get; set; }
        public virtual Book book { get; set; }
        public int Id { get; set; }
        public virtual Author author { get; set; }
    }
}
