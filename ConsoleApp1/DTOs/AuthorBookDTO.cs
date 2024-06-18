using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternApi.Application.DTOs
{
    public class AuthorBookDTO
    {
        public int ISBN { get; set; }
        public int AuthorID { get; set; }
    }
}
