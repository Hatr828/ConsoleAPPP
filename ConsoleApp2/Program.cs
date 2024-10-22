using Shop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;

namespace BankTransactionExample
{
    class Program
    {
        static void Main(string[] args)
        {
            SeedData.Initialize(context);

            var linqQueries = new LinqQueries(context);

            var count = linqQueries.GetBooksCountByGenre(1);
            var minPrice = linqQueries.GetMinPriceByAuthor(1);
            var avgPrice = linqQueries.GetAveragePriceByGenre(2);
            var totalPrice = linqQueries.GetTotalPriceByAuthor(1);
            var groupedBooks = linqQueries.GroupBooksByGenre();
            var titles = linqQueries.GetTitlesByGenre(1);
            var allExceptGenre = linqQueries.GetAllExceptGenre(2);
            var unionBooks = linqQueries.UnionBooksByTwoAuthors(1, 2);
            var top5Books = linqQueries.GetTop5ExpensiveBooks();
            var skippedBooks = linqQueries.Skip10Take5();

        }
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public decimal Price { get; set; }

        public Author Author { get; set; }
        public Genre Genre { get; set; }
    }

    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }

    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }

    public class HW
    {
        private readonly AppDbContext _context;

        public LinqQueries(AppDbContext context)
        {
            _context = context;
        }

        public int GetBooksCountByGenre(int genreId)
        {
            return _context.Books.Count(b => b.GenreId == genreId);
        }

        public decimal GetMinPriceByAuthor(int authorId)
        {
            return _context.Books.Where(b => b.AuthorId == authorId).Min(b => b.Price);
        }

        public decimal GetAveragePriceByGenre(int genreId)
        {
            return _context.Books.Where(b => b.GenreId == genreId).Average(b => b.Price);
        }

        public decimal GetTotalPriceByAuthor(int authorId)
        {
            return _context.Books.Where(b => b.AuthorId == authorId).Sum(b => b.Price);
        }

        public IEnumerable<IGrouping<int, Book>> GroupBooksByGenre()
        {
            return _context.Books.GroupBy(b => b.GenreId);
        }

        public IEnumerable<string> GetTitlesByGenre(int genreId)
        {
            return _context.Books.Where(b => b.GenreId == genreId).Select(b => b.Title);
        }

        public IEnumerable<Book> GetAllExceptGenre(int genreId)
        {
            var excludedBooks = _context.Books.Where(b => b.GenreId == genreId);
            return _context.Books.Except(excludedBooks);
        }

        public IEnumerable<Book> UnionBooksByTwoAuthors(int authorId1, int authorId2)
        {
            var books1 = _context.Books.Where(b => b.AuthorId == authorId1);
            var books2 = _context.Books.Where(b => b.AuthorId == authorId2);
            return books1.Union(books2);
        }

        public IEnumerable<Book> GetTop5ExpensiveBooks()
        {
            return _context.Books.OrderByDescending(b => b.Price).Take(5);
        }

        public IEnumerable<Book> Skip10Take5()
        {
            return _context.Books.Skip(10).Take(5);
        }
    }

}
