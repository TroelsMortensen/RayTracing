using System.Text.Json;
using Core;

namespace UnitTests;

public class PathTracerTests
{
    
    [Fact]
    public void CanDeserializeImage()
    {
        string json = File.ReadAllText(@"C:\TRMO\RiderProjects\RayTracing\Shell\CLI\Json.txt");
        Image image = JsonSerializer.Deserialize<Image>(json)!;
        Assert.NotNull(image);
        Assert.Equal(50, image.Width);
        Assert.Equal(50*28, image.Colors.Length);
    }
    
}