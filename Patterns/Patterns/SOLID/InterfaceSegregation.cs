namespace Patterns.SOLID
{
    public class Document
    {

    }

    public interface IPrinter
    {
        void Print(Document document);
    }

    public interface IScanner
    {
        void Scan(Document document);
    }

    public interface IMultiFunctionDevice : IPrinter, IScanner
    {

    }

    public class Photocopier : IMultiFunctionDevice
    {
        private readonly IPrinter _printer;
        private readonly IScanner _scanner;

        public Photocopier(IPrinter printer, IScanner scanner)
        {
            _printer = printer;
            _scanner = scanner;
        }

        public void Print(Document document)
        {
            _printer.Print(document);
        }

        public void Scan(Document document)
        {
            _scanner.Scan(document);
        }
    }
}
