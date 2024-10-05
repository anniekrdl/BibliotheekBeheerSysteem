using System.Text.Json;

namespace Bibliotheekbeheer
{

  public class Library
  {

    //properties
    List<Book> Books = new List<Book>();
    public string LibraryName { get; set; }


    public Library(string libraryName = "")
    {
      LibraryName = libraryName;
    }



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
      }
      else
      {
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

    public List<Book> SearchBookByGenre(string genre)
    {
      List<Book> result = new List<Book>();
      foreach (Book book in Books)
      {
        if (book.isMatchGenre(genre))
        {
          result.Add(book);
        }
      }
      return result;
    }

    public List<Book> SearchBookByPublicationYear(string publicationYear)
    {
       List<Book> result = new List<Book>();
      foreach (Book book in Books)
      {
        if (book.isMatchYear(publicationYear))
        {
          result.Add(book);
        }
      }
      return result;
    }

    public List<Book> SearchBookByIsbn(string isbn)
    {
      List<Book> result = new List<Book>();
      foreach (Book book in Books)
      {
        if (book.isMatchISBN(isbn))
        {
          result.Add(book);
        }
      }
      return result;
    }


    public void SaveLibraryToFile()
    {

      if (File.Exists(LibraryName))
      {
        //load file
        LoadLibraryFromFile();


      }
      //create/update file
        string jsonString = JsonSerializer.Serialize(Books);
        File.WriteAllText(LibraryName, jsonString);

    }

    public void LoadLibraryFromFile()
    {
      if (File.Exists(LibraryName))
      {

        string jsonString = File.ReadAllText(LibraryName);
        List<Book> loadedBooks = JsonSerializer.Deserialize<List<Book>>(jsonString);
        foreach(Book book in loadedBooks)
        {
          Books.Add(book);
        }


        Console.WriteLine($"\nBibliotheek {LibraryName} geopend met {Books.Count} boek(en).\n");




      }

    }

    public void OpenLibrary(string libraryName)
    {
      //empty list
      Books = new List<Book>();
      this.LibraryName = libraryName;
      LoadLibraryFromFile();

    }

  }
}