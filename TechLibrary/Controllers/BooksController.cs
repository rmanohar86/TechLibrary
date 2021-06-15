using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TechLibrary.Domain;
using TechLibrary.Models;
using TechLibrary.Services;
using TechLibrary.Wrappers;
using System;

namespace TechLibrary.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Route("[controller]/[action]")]
    public class BooksController : Controller
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(ILogger<BooksController> logger, IBookService bookService, IMapper mapper)
        {
            _logger = logger;
            _bookService = bookService;
            _mapper = mapper;
        }

        /// <summary>
        /// This method is to get the books for the given page number
        /// </summary>
        [HttpGet("getallbooks")]
        public async Task<IActionResult> GetAllBooks([FromQuery] int pageNumber)
        {
            _logger.LogInformation($"GetAllBooks for the pageNumber : {pageNumber}");
            try
            {
                var books = await _bookService.GetBooksForSinglePageAsync(pageNumber);
                var getAllBooks = await _bookService.GetAllBooksAsync();
                var bookResponse = _mapper.Map<List<BookResponse>>(books);
                return Ok(new Response<List<BookResponse>>(bookResponse, pageNumber, getAllBooks.Count));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in getting all book details for the page number {pageNumber} : {ex.Message} ");
                return NotFound();
            }
        }

        /// <summary>
        /// This method is to search the books for the given search string (title or description)
        /// </summary>
        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks([FromQuery] string searchString)
        {
            _logger.LogInformation($"SearchBooks : {searchString}");
            try
            {
                var books = await _bookService.GetBooksByTitleAsync(searchString);
                var bookResponse = _mapper.Map<List<BookResponse>>(books);
                return Ok(bookResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in searching book details for the search string  {searchString} : {ex.Message} ");
                return NotFound();
            }
        }

        /// <summary>
        /// This method is to get the single book details for the given ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation($"GetById : {id}");
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                if (book != null)
                {
                    var bookResponse = _mapper.Map<BookResponse>(book);
                    return Ok(bookResponse);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in getting the details for the Book ID {id} : {ex.Message} ");
                return NotFound();
            }
        }

        /// <summary>
        /// This method is to get the update the description details for the given ID.
        /// </summary>
        [HttpPost("update")]
        public async Task<IActionResult> UpdateDescById(Book _book)
        {
            _logger.LogInformation($"UpdateDescById : {_book.BookId}");
            try
            {                
                var book = await _bookService.UpdateBookDescByIdAsync(_book.BookId, _book.ShortDescr);
                var bookResponse = _mapper.Map<BookResponse>(book);
                return Ok(bookResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in updating the description for the Book ID {_book.BookId} : {ex.Message} ");
                return BadRequest();
            }
        }

        /// <summary>
        /// This method is to add the new book to the database. Book ID will be generated on the fly based on the last ID available.
        /// </summary>
        [HttpPost("add")]
        public async Task<IActionResult> AddNewBook(Book _book)
        {
            _logger.LogInformation($"AddNewBook : {_book.Title}");
            try
            {
                var book = await _bookService.AddNewBookAsync(_book);
                var bookResponse = _mapper.Map<BookResponse>(book);
                return Ok(bookResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in adding New book : {ex.Message} ");
                return BadRequest();
            }
        }
    }
}

