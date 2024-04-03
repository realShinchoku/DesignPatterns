Console.WriteLine("Hello, World!");

class Document
{
}

interface IMachine
{
    void Print(Document d);
    void Scan(Document d);
    void Fax(Document d);
}

class MultiFunctionPrinter : IMachine
{
    public void Print(Document d)
    {
        //
    }

    public void Scan(Document d)
    {
        //
    }

    public void Fax(Document d)
    {
        //
    }
}

class OldFashionedPrinter : IMachine
{
    public void Print(Document d)
    {
        //
    }

    public void Scan(Document d)
    {
        throw new System.NotImplementedException();
    }

    public void Fax(Document d)
    {
        throw new System.NotImplementedException();
    }
}

interface IPrinter
{
    void Print(Document d);
}

interface IScanner
{
    void Scan(Document d);
}

interface IFax
{
    void Fax(Document d);
}

class Photocopier : IPrinter, IScanner
{
    public void Print(Document d)
    {
        //
    }

    public void Scan(Document d)
    {
        //
    }
}

interface IMultiFunctionDevice : IPrinter, IScanner
{
}

class MultiFunctionMachine(IPrinter printer, IScanner scanner) : IMultiFunctionDevice
{
    public void Print(Document d)
    {
        printer.Print(d);
    }

    public void Scan(Document d)
    {
        scanner.Scan(d);
    }
}