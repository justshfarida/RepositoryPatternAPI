using RepositoryPatternApi.Domain.Entities;
using RepositoryPatternAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternApi.Application.Interfaces.IServices
{
    public interface IBookService
    {
        void AddBook(Book book );
        void UpdateBook(Book book);
        void DeleteBook(int id);
        Book GetBookById(int id);
        IEnumerable<Book> GetAllBooks();
    }
}
