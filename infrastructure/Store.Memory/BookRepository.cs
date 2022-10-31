namespace Store.Memory
{
    public class BookRepository : IBookRepository
    {
        private readonly Book[] books = new Book[]
        {
            new Book(1, "ISBN 12312-31231", "D. Knuth", "Art of Programming"),
            new Book(2, "ISBN 12312-31232", "M. Fowler", "Refactoring"),
            new Book(3, "ISBN 12312-31232", "B. Kernigan, D. ritchie", "C Programming Language")
        };

        public Book[] GetAllByIsbn(string isbn)
        {
            return books.Where(book=>book.Isbn == isbn).ToArray();
        }

        public Book[] GetAllByTytleOrAuthor(string titleOrAuthor)
        {
            return books.Where(book => book.Title.Contains(titleOrAuthor) || 
                               book.Author.Contains(titleOrAuthor))
                               .ToArray();
        }
    }
}