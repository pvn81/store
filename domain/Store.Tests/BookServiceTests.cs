using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Store.Tests
{
    
    public class BookServiceTests
    {
        [Fact]
        public void GetAllByQuery_WithIsbn_CallsGetAllByIsbn()
        {
            //Заглушка к книжному репозиторию moq
            var bookRepositoryStub = new Mock<IBookRepository>();

            //Если будет вызываться метод GetAllByIsbn с какой либо строкой вернуть массив
            bookRepositoryStub.Setup(x => x.GetAllByIsbn(It.IsAny<string>()))
                              .Returns(new[] { new Book(1, "", "", "") });

            //Если будет вызываться метод GetAllByTytleOrAuthorn с какой либо строкой вернуть массив
            bookRepositoryStub.Setup(x => x.GetAllByTytleOrAuthor(It.IsAny<string>()))
                              .Returns(new[] { new Book(2, "", "", "") });

            //Теперь создать BookService и в качестве параметра передать заглушук bookRepositoryStub
            var bookService = new BookService(bookRepositoryStub.Object);

            //Теперь представляем актуальное значение
            //Если мы передали правильный ISBN и вызвали GetAllByQuery, он обратиться к нашему BookRepository
            //и он должен вызвать метод x => x.GetAllByIsbn(It.IsAny<string>())
            //книжка ктороая вернется будет иметь идентификато 1
            var actual = bookService.GetAllByQuery("ISBN 12345-67890");

            //Проверяем идентификатор
            Assert.Collection(actual, book=>Assert.Equal(1, book.Id));

        }

        //ниже тест на парный случай. Если приходит строка с Автором или названием книги
        [Fact]
        public void GetAllByQuery_WithAuthorOrTitle_CallsGetAllByTytleOrAuthor()
        {
            var bookRepositoryStub = new Mock<IBookRepository>();

            bookRepositoryStub.Setup(x => x.GetAllByIsbn(It.IsAny<string>()))
                              .Returns(new[] { new Book(1, "", "", "") });

            bookRepositoryStub.Setup(x => x.GetAllByTytleOrAuthor(It.IsAny<string>()))
                              .Returns(new[] { new Book(2, "", "", "") });

            var bookService = new BookService(bookRepositoryStub.Object);

            var actual = bookService.GetAllByQuery("AuthorOrTitle");

            Assert.Collection(actual, book => Assert.Equal(2, book.Id));

        }
    }
}
