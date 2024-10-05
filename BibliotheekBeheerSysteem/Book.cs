namespace Bibliotheekbeheer
{

  public class Book : IReadable
  {
    //properties
    public string Title { get; set; }
    public string Author { get; set; }
    public int? ISBN { get; set; }
    public int? PublicationYear { get; set; }
    public string Genre { get; set; }


    //constructor
    public Book(string title, string author, int? iSBN = null, int? publicationYear = null, string genre = "Onbekend")
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
      Console.WriteLine($"Titel: {Title}\nAuteur: {Author}\nPublicatiejaar: {PublicationYear}\nGenre: {Genre}\nISBN: {ISBN}");
    }

    //check if book is match with query
    public bool isMatch(string searchQuery)
    {
      return Title.ToLower().Contains(searchQuery.ToLower()) || Author.ToLower().Contains(searchQuery.ToLower());

    }

    public bool isMatchGenre(string searchQuery)
    {
      return Genre.ToLower().Contains(searchQuery.ToLower());
    }

    public bool isMatchYear(string searchQuery)
    {
      return PublicationYear == Int32.Parse(searchQuery);
    }
    public bool isMatchISBN(string searchQuery)
    {
      return ISBN == Int32.Parse(searchQuery);
    }

    
    public void Read()
    {
      Console.WriteLine($"{Title} geschreven door {Author} aan het lezen.....");
      
    }
  }
}