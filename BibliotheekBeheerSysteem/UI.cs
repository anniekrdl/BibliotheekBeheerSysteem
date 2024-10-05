using Microsoft.VisualBasic;

namespace Bibliotheekbeheer
{
  public class UI
  {

    Library library = new Library();

    bool programRunning = true;

    //Show user menu
    public void DisplayMenu()
    {

      string question =
        "WAT WIL JE DOEN?\n" +
        "----------------\n" +
        "Een boek lezen [read]\n" +
        "Een boek toevoegen [add]\n" +
        "Een boek verwijderen [delete]\n" +
        "Een boek zoeken [search]\n" +
        "Alle boeken tonen [list]\n" +
        "Sla de library op [save]\n" +
        "Open andere bibliotheek [open]\n" +
        "Sluit de bibliotheek af [close]\n";

      string choice = GetUserInput(question);

      switch (choice.ToLower().Trim())

      {
        case "read":
          ReadBook();
          break;
        case "add":
          addBook();
          break;
        case "delete":
          deleteBook();
          break;
        case "search":
          searchBook();
          break;
        case "list":
          library.ListBooks();
          break;
        case "save":
          library.SaveLibraryToFile();
          break;
        case "open":
          OpenLibrary();
          break;
        case "close":
          programRunning = false;
          break;
        default:
          break;
      }

    }

        private void OpenLibrary()
        {
            string libraryTitle = GetUserInput("Welke bibliotheek wil je openen/maken?");
            library.OpenLibrary(libraryTitle);
        }

        private void ReadBook()
    {
      //select book
      string TitleBook = GetUserInput("Wat is de titel van het boek dat je wil lezen?");
      List<Book> foundBooks = library.SearchBook(TitleBook);
      if (foundBooks.Count > 0)
      {
        Book book = foundBooks.First();
        book.Read();
      }
      else
      {
        Console.WriteLine("Geen boeken gevonden.");
      }

    }

    private void searchBook()
    {
      string searchQuery = GetUserInput("Voer de titel of auteur in van het boek dat je zoekt: ");
      List<Book> books = library.SearchBook(searchQuery);

      if (books.Count > 0)
      {
        Console.WriteLine("\nGevonden boeken:");
        foreach (Book book in books)
        {
          book.DisplayDetails();
          Console.WriteLine();

        }

      }
      else
      {
        Console.WriteLine("\nGeen boeken gevonden");
        string extendedSearch = GetUserInput("Wil je uitgebreid zoeken? [y/n]");
        if (extendedSearch == "y")
        {
          string searchType = GetUserInput("Wil je op genre [g], publicatiejaar [p] of isbn [i] zoeken?");

          switch (searchType)
          {
            case "g":
              string genre = GetUserInput("Voer het genre in: ");
              List<Book> booksByGenre = library.SearchBookByGenre(genre);
              foreach (Book book in booksByGenre)
              {
                book.DisplayDetails();
                Console.WriteLine();

              }
              break;
            case "p":
              string publicationYear = GetUserInput("Voer het publicatiejaar in: ");
              List<Book> booksByPublicationYear = library.SearchBookByPublicationYear(publicationYear);
              foreach (Book book in booksByPublicationYear)
              {
                book.DisplayDetails();
                Console.WriteLine();

              }
              break;
            case "i":
              string isbn = GetUserInput("Voer het isbn in: ");
              List<Book> booksByIsbn = library.SearchBookByIsbn(isbn);
              foreach (Book book in booksByIsbn)
              {
                book.DisplayDetails();
                Console.WriteLine();

              }
              break;
          }

        }


      }



    }

    private void deleteBook()
    {
      string isbn = TryParseToInt("Wat is het isbn nummer van het boek dat je wil verwijderen? ");

      library.RemoveBook(int.Parse(isbn));

    }


    private void addBook()
    {
      string title = GetUserInput("Wat is de titel van het boek?");
      string author = GetUserInput("Wie is de auteur van het boek?");

      //optional fields
      int ISBN = 0;
      int publicationYear = 0000;
      string genre = "Onbekend";

      string extraInfo = GetUserInput("Wil je nog een ISBN en/of Publicatiejaar en/of Genre toevoegen? [y/n]");
      string[] options = { "ISBN", "Genre", "Publicatiejaar" };


      if (extraInfo.ToLower() == "y")
      {

        for (int i = 0; i < options.Length; i++)
        {
          string inputQ = GetUserInput($"Wil je een {options[i]} toevoegen? [y/n]");
          if (inputQ.ToLower().Trim() == "y")
          {
            // TODO evt omzetten naar Dictionary.
            switch (options[i])
            {
              case "ISBN":
                string isbnInput = TryParseToInt("ISBN: ");
                ISBN = int.Parse(isbnInput);

                break;

              case "Publicatiejaar":
                string yearInput = TryParseToInt("Publicatiejaar: ");
                publicationYear = int.Parse(yearInput);
                break;
              case "Genre":
                genre = GetUserInput("Genre: ");
                break;
            }


          }

        }

      }

      //check if ebook
      string input = GetUserInput("Is het boek een Ebook? [y/n]");
      // make ebook save as Book in list
      if (input.ToLower().Trim() == "y")
      {
        //get fileSize
        string size = TryParseToInt("Hoe groot is het ebook? (in MB's)");
        int fileSize = int.Parse(size);

        //Add ebook
        Ebook ebook = new Ebook(title, author, fileSize, ISBN, publicationYear, genre);
        library.AddBook(ebook);

      }
      else
      {

        //Add book
        Book book = new Book(title, author, ISBN, publicationYear, genre);
        library.AddBook(book);

      }


      //save/update library file
      library.SaveLibraryToFile();

    }



    public string TryParseToInt(string question)
    {
      Console.WriteLine(question);
      string input = Console.ReadLine();

      while (!int.TryParse(input, out _))
      {
        Console.WriteLine("Dit is geen geldig getal. Probeer het opnieuw.");
        Console.WriteLine(question);
        input = Console.ReadLine();

      }

      return input;
    }





    //get user input
    public string GetUserInput(string question)
    {

      string input = "";

      Console.WriteLine(question);

      while (String.IsNullOrEmpty(input))
      {
        input = Console.ReadLine().Trim();

        if (String.IsNullOrEmpty(input))
        {
          Console.WriteLine("ongeldige invoer. Probeer opnieuw: ");
        }

      }

      return input;

    }


    //runs the program
    public void Run()
    {

      string startQuestion = "Hoe heet je bibliotheek?";
      string input = GetUserInput(startQuestion);
      library.LibraryName = input.Trim();

      // check if library exist
      if (File.Exists(library.LibraryName))
      {
        library.LoadLibraryFromFile();
      }


      while (programRunning)
      {

        DisplayMenu();
      }


    }

  }
}