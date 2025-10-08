namespace KontursvetStore.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var text = new string[] { "12.jpg", "24.png", "36.jpg", "48.jpg", "64.jpg", "72.jpg" };
        
        var t2 = string.Join( ";", text );
        Assert.Pass();
    }
}