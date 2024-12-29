using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
	public class BookService : IBookService
	{
		private readonly ApplicationDbContext _applicationDbContext;
		public BookService(ApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}
		public async Task<Book> GetBook()
		{
			Book book = await _applicationDbContext.Books.OrderByDescending(x => x.Price).FirstAsync();
			return book;
		}

		public async Task<List<Book>> GetBooks()
		{
			List<Book> books = await _applicationDbContext.Books.Where(x => x.Title.Contains("Red") && x.PublishDate > new DateTime(2012, 5, 25)).ToListAsync();
			return books;
		}
	}
}
