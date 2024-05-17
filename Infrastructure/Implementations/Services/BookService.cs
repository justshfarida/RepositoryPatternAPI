using RepositoryPatternApi.Application.Interfaces.IRepositories;
using RepositoryPatternApi.Application.Interfaces.IServices;
using RepositoryPatternApi.Domain.Entities;
using RepositoryPatternAPI.Data.Entities;
using RepositoryPatternAPI.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternApi.Infrastructure.Implementations.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddBook(Book book)
        {
            _unitOfWork.bookRepository.Add(book);
            _unitOfWork.Commit();
        }

        public void DeleteBook(int id)
        {

            _unitOfWork.bookRepository.Delete(id);
            _unitOfWork.Commit();
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _unitOfWork.bookRepository.GetAll().ToList();

        }

        public Book GetBookById(int id)
        {
            return _unitOfWork.bookRepository.GetById(id);

        }

        public void UpdateBook(Book book)
        {
            _unitOfWork.bookRepository.Update(book);
            _unitOfWork.Commit();
        }
    }
}