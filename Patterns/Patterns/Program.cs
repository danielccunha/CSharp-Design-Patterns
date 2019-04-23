using Patterns.SOLID;

namespace Patterns
{
    class Program
    {
        static void Main(string[] args)
        {
            var journal = new Journal();
            journal.AddEntry("Lorem ipsum");
            journal.AddEntry("Dolor sit amet");

            var persistence = new Persistence();
            var filename = @"C:\journal.txt";
            persistence.SaveToFile(journal, filename, overwrite: true);
        }
    }
}
