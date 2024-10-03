namespace Bibliotheekbeheer
{

  public class Library
  {

    //properties
    List<Book> Books = new List<Book>();

    //Add book to Library
    public void AddBook(Book book)
    {
      Books.Add(book);
    }


    //remove book with isbn
    public void RemoveBook(int isbn)
    {

      Book? book = Books.FirstOrDefault(b => b.ISBN == isbn);

      if (book != null)
      {
        Books.Remove(book);
      } else{
        Console.WriteLine("Boek is niet bekend in de bibliotheek.");
      }
                  


    }

    //search book in Library
    public List<Book> SearchBook(string query)
    {
      List<Book> result = new List<Book>();
      foreach (Book book in Books)
      {
        if (book.isMatch(query))
        {
          result.Add(book);
        }
      }
      return result;
    }

    //List of all Books in Library
    public void ListBooks()
    {

      //sort by Title
      var BooksSortedByTitle = Books.OrderBy(book => book.Title);

      foreach (Book book in BooksSortedByTitle)
      {
        book.DisplayDetails();
        Console.WriteLine();
      }
    }



  }
}