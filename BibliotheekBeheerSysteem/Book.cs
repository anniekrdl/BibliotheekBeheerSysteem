namespace Bibliotheekbeheer
{

  public class Book : IReadable
  {
    //properties
    public string Title { get; set; }
    public string Author { get; set; }
    public int ISBN { get; set; }
    public int PublicationYear { get; set; }
    public string Genre { get; set; }


    //constructor
    public Book(string title, string author, int iSBN = 0, int publicationYear = 000, string genre = "Onbekend")
    {
      Title = title;
      Author = author;
      ISBN = iSBN;
      PublicationYear = publicationYear;
      Genre = genre;
    }



    //Display Book Details
    public virtual void DisplayDetails()
    {
      Console.WriteLine($"Titel: {Title}\nAuteur: {Author}\nPublicatiejaar: {PublicationYear}\nGenre: {Genre}");
    }

    //check if book is match with query
    public bool isMatch(string searchQuery)
    {
      return Title.ToLower().Contains(searchQuery.ToLower()) || Author.ToLower().Contains(searchQuery.ToLower());

    }

    
    public void Read()
    {
      Console.WriteLine($"Lezen {Title} door {Author}");
      
    }
  }
}