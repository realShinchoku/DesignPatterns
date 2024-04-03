var apple = new Product("Apple", Color.Green, Size.Small);
var tree = new Product("Tree", Color.Green, Size.Large);
var house = new Product("House", Color.Blue, Size.Large);
Product[] products = [apple, tree, house];

var pf = new ProductFilter();
Console.WriteLine("Green products (old):");
foreach (var p in pf.FilterBySize(products, Size.Large))
    Console.WriteLine($" - {p.Name} is large and green");

var bf = new BetterFilter();
Console.WriteLine("Green products (new):");
foreach (var p in bf.Filter(products, new ColorSpecification(Color.Green)))
    Console.WriteLine($" - {p.Name} is green");

Console.WriteLine("Large blue items:");
foreach (var p in bf.Filter(products,
             new AndSpecification<Product>(new ColorSpecification(Color.Blue), new SizeSpecification(Size.Large))))
    Console.WriteLine($" - {p.Name} is large and blue");

internal enum Color
{
    Read,
    Green,
    Blue
}

internal enum Size
{
    Small,
    Medium,
    Large,
    Huge
}

internal class Product(string name, Color color, Size size)
{
    public readonly string Name = name;
    public readonly Color Color = color;
    public readonly Size Size = size;
}

internal class ProductFilter
{
    public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
    {
        return products.Where(p => p.Size == size);
    }

    public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
    {
        return products.Where(p => p.Color == color);
    }

    public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color)
    {
        return products.Where(p => p.Size == size && p.Color == color);
    }
}

internal interface ISpecification<in T>
{
    bool IsSatisfied(T t);
}

internal interface IFilter<T>
{
    IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
}

internal class ColorSpecification(Color color) : ISpecification<Product>
{
    public bool IsSatisfied(Product p)
    {
        return p.Color == color;
    }
}

internal class BetterFilter : IFilter<Product>
{
    public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
    {
        return items.Where(spec.IsSatisfied);
    }
}

internal class SizeSpecification(Size size) : ISpecification<Product>
{
    public bool IsSatisfied(Product p)
    {
        return p.Size == size;
    }
}

internal class AndSpecification<T>(ISpecification<T> first, ISpecification<T> second) : ISpecification<T>
{
    public bool IsSatisfied(T t)
    {
        return first.IsSatisfied(t) && second.IsSatisfied(t);
    }
}