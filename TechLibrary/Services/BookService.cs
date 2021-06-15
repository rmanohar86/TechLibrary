using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechLibrary.Data;
using TechLibrary.Domain;
using TechLibrary.Models;

namespace TechLibrary.Services
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<List<Book>> GetBooksForSinglePageAsync(int pageNumber);
        Task<List<Book>> GetBooksByTitleAsync(string title);
        Task<Book> GetBookByIdAsync(int bookid);
        Task<Book> UpdateBookDescByIdAsync(int bookid, string desc);
        Task<Book> AddNewBookAsync(Book _book);
    }

    public class BookService : IBookService
    {
        private readonly DataContext _dataContext;

        public BookService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            var queryable = _dataContext.Books.AsQueryable();
            return await queryable.ToListAsync();
        }

        public async Task<List<Book>> GetBooksForSinglePageAsync(int pageNumber)
        {
            var queryable = _dataContext.Books
                        .Skip((pageNumber-1) * 10)
                        .Take(10);
            return await queryable.ToListAsync();
        }

        public async Task<List<Book>> GetBooksByTitleAsync(string title)
        {
            var queryable = from e in _dataContext.Books where e.Title.Contains(title) || e.ShortDescr.Contains(title) select e; ;
            return await queryable.ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int bookid)
        {
            return await _dataContext.Books.SingleOrDefaultAsync(x => x.BookId == bookid);
        }

        public async Task<Book> UpdateBookDescByIdAsync(int bookid, string desc)
        {
            var _book = _dataContext.Books.FirstOrDefault(x => x.BookId == bookid);
            if (_book != null)
            {
                _book.ShortDescr = desc;
                _dataContext.SaveChanges();                
            }
            return await _dataContext.Books.SingleOrDefaultAsync(x => x.BookId == bookid);
        }

        public async Task<Book> AddNewBookAsync(Book book)
        {
            book.BookId = _dataContext.Books.Max(x => x.BookId) + 1;
            _dataContext.Add(book);
            await _dataContext.SaveChangesAsync();
            return await _dataContext.Books.SingleOrDefaultAsync(x => x.BookId == book.BookId);
        }
    }
}
