var parent = new Person { Name = "John" };
var child1 = new Person { Name = "Chris" };
var child2 = new Person { Name = "Matt" };

var relationships = new Relationships();
relationships.AddParentAndChild(parent, child1);
relationships.AddParentAndChild(parent, child2);

var research = new Research(relationships);

public class Research
{
    // public Research(Relationships relationships)
    // {
    //     var relations = relationships.Relations;
    //     foreach (var r in relations.Where(
    //                  x => x.Item1.Name == "John" && x.Item2 == RelationShip.Parent
    //              ))
    //     {
    //         Console.WriteLine($"John has a child called {r.Item3.Name}");
    //     }
    // }

    public Research(IRelationshipBrowser browser)
    {
        foreach (var p in browser.FindAllChildrenOf("John"))
            Console.WriteLine($"John has a child called {p.Name}");
    }
}

public enum RelationShip
{
    Parent,
    Child,
    Sibling
}

public class Person
{
    public string Name { get; set; }
    // public DateTime DateOfBirth;
}

public interface IRelationshipBrowser
{
    IEnumerable<Person> FindAllChildrenOf(string name);
}

// Low-level module

public class Relationships : IRelationshipBrowser
{
    private readonly List<(Person, RelationShip, Person)> _relations = [];

    public void AddParentAndChild(Person parent, Person child)
    {
        _relations.Add((parent, RelationShip.Parent, child));
        _relations.Add((child, RelationShip.Child, parent));
    }

    // public List<(Person, RelationShip, Person)> Relations => _relations;
    public IEnumerable<Person> FindAllChildrenOf(string name)
    {
        return _relations.Where(
            x => x.Item1.Name == name && x.Item2 == RelationShip.Parent
        ).Select(r => r.Item3);
    }
}