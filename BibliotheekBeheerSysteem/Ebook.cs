namespace Bibliotheekbeheer
{
  public class Ebook : Book
  {

    //constructor
    public Ebook(string title, string author,int fileSize, int iSBN = 0, int publicationYear = 000, string genre = "Onbekend") : base(title, author, iSBN, publicationYear, genre)
    {
      FileSize = fileSize;

    }

    int FileSize
    {
      get; set;
    }

    public override void DisplayDetails()
    {
      Console.WriteLine($"Titel: {Title}\nAuteur: {Author}\nPublicatiejaar: {PublicationYear}\nGenre: {Genre}\nFileSize: {FileSize}");
    }

  }
}