using System;

namespace Bibliotheekbeheer{
  class Program
{
   static void Main(string[] args)
   {
    
    Book book1 = new Book("title1", "author1");
    Book book2 = new Book("title2", "author2");
    Book book3 = new Book("title3", "author3");

    Library library = new Library();

    library.AddBook(book1);
    library.AddBook(book2);
    library.AddBook(book3);

    library.ListBooks();
   }
}

}

