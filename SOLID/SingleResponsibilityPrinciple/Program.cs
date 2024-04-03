using System.Diagnostics;

var j = new Journal();

j.AddEntry("I cried today.");
j.AddEntry("I ate a bug.");
Console.WriteLine(j.ToString());

var p = new Persistence();
const string filename = @"journal.txt";
Persistence.SaveToFile(j, filename, true);
Process.Start(filename);

internal class Journal
{
    private readonly List<string> _entries = [];
    private static int _count = 0;

    public int AddEntry(string text)
    {
        _entries.Add($"{++_count}: {text}");
        return _count; // memento pattern!
    }

    public void RemoveEntry(int index)
    {
        _entries.RemoveAt(index);
    }

    public override string ToString()
    {
        return string.Join(Environment.NewLine, _entries);
    }
}

internal class Persistence
{
    public static void SaveToFile(Journal journal, string filename, bool overwrite = false)
    {
        if (overwrite || !File.Exists(filename))
            File.WriteAllText(filename, journal.ToString());
    }
}