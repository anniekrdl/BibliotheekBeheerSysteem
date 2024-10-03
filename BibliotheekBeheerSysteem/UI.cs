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
        "Sluit de bibliotheek af [close]\n";

      string choice = GetUserInput(question);

      switch (choice.ToLower().Trim())

      {
        case "read":
          Console.WriteLine("not implemented yet");
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
        case "close":
          programRunning = false;
          break;
        default:
          break;
      }

    }



    private void searchBook()
    {
      string searchQuery = GetUserInput("Voer de titel of auteur in van het boek dat je zoekt: ");
      List<Book> books = library.SearchBook(searchQuery);
      Console.WriteLine("\nGevonden boeken:");
      foreach (Book book in books)
      {
        book.DisplayDetails();
        Console.WriteLine();

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

      // is het een ebook?
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



      while (programRunning)
      {
        DisplayMenu();
      }


    }

  }
}