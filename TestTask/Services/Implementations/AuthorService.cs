using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
	public class AuthorService : IAuthorService
	{
		private readonly ApplicationDbContext _applicationDbContext;

		public AuthorService(ApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}
		public Task<Author> GetAuthor()
		{
			var maxLength = _applicationDbContext.Books.Select(x => x.Title.Length).Max();
			var author = _applicationDbContext.Authors
				.Include(x => x.Books)
				.Where(x => x.Books.Any(x => x.Title.Length == maxLength))
				.OrderBy(x => x.Id)
				.FirstAsync();

			return author;
		}

		public Task<List<Author>> GetAuthors()
		{
			List<Author> authors = _applicationDbContext.Books
				.Include(x => x.Author)
				.Where(x => x.PublishDate >= new DateTime(2015, 1, 1))
				.GroupBy(x => x.Author)
				.Where(x => x.Count() % 2 == 0)
				.Select(x => x.Key)
				.ToList();

			return Task.FromResult(authors);
		}
	}
}
