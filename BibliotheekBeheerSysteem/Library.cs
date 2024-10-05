using System.Text.Json;

namespace Bibliotheekbeheer
{

  public class Library
  {

    //properties
    List<Book> Books = new List<Book>();
    public string LibraryName { get; set; }

    private static string libraryFolderPath = "Libraryfiles";


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
        //update file
        SaveLibraryToFile();
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

    //search book by genre
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

    // search book in library by publicationYear
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

    //search book in library by isbn
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

    //save library to the file with libraryName
    public void SaveLibraryToFile()
    {
      //create/update file
      
      string jsonString = JsonSerializer.Serialize(Books);
      string filePath = Path.Combine(libraryFolderPath, LibraryName + ".json");
      File.WriteAllText(filePath, jsonString);

    }

    //load library from file with libraryName
    public void LoadLibraryFromFile()
    {
      string filePath = Path.Combine(libraryFolderPath, LibraryName + ".json");

      if (File.Exists(filePath))
      {
        string jsonString = File.ReadAllText(filePath);

        List<Book>? loadedBooks = JsonSerializer.Deserialize<List<Book>>(jsonString);
        if (loadedBooks != null)
        {
          foreach (Book book in loadedBooks)
          {
            Books.Add(book);
          }


          Console.WriteLine($"\nBibliotheek {LibraryName} geopend met {Books.Count} boek(en).\n");

        }
        else
        {
          Console.WriteLine($"Geen bibliotheek gevonden bij {LibraryName}.\n");
        }


      }

    }

    //Open exting library
    public void OpenLibrary(string libraryName)
    {
      //empty list
      Books = new List<Book>();
      this.LibraryName = libraryName;
      LoadLibraryFromFile();

    }

    //show all existing libraries
    public static void ShowAllLibraries()
    {
      //find all json files in folder
      string[] files = Directory.GetFiles(libraryFolderPath, "*.json");

      if (files.Length > 0)
      {
        Console.WriteLine("Huidige bibliotheken: ");
        foreach (string file in files)
        {
          Console.WriteLine(Path.GetFileNameWithoutExtension(file));
        }
        Console.WriteLine();

      }
      else
      {
        Console.WriteLine("Geen bibliotheken gevonden.\n");
      }

    }

    //check if library exist
    public static bool LibraryExist(string fileName)
    {

      string filePath = Path.Combine(libraryFolderPath, fileName + ".json");
      //check if file exist
      if (File.Exists(filePath))
      {
        //load library
        return true;
        

      }

      return false;

    }

  }

  
}