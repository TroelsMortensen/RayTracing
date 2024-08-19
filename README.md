# Ray tracing

During the last year of my master degree I worked with ray tracing. I've forgotten all about it, and I needed a hobby project, so I'm looking into that area again.

This application is based on the pdf [Ray Tracing in a Weekend](https://raytracing.github.io/).

## A functional approach
I have been looking into functional programming recently, first in C#, and then F#. So I wanted to apply some of those ideas and principles. 
I am not all that interested in immutable data, but I feel that an FP approach often makes your code simpler to read.

## Name your expressions and prose-code
I have also attempted to apply the "Name your expressions" approach, I heard Zoran Horvat mention in one of his vidoes. 
Essentially this idea is to do a lot of "extract single expression into separate function", so that you can give that function a descriptive name. Instead of understanding what the statement does, you can read the name.
It's a kind of abstraction.\
The idea is just that your code reads more like prose, and you spend less time actually trying to understand what is going on.

Below is an example of how I export my image to a png file, first the main part:

```csharp
public static void ExportImageToPngFile(Image image, string path) =>
    CreateEmptyBitmap(image)
        .Then(MapImageOntoBitmap(image))
        .Finally(SaveBitmapToPng(path));
```

See how you can read this as:
* Create an empty bitmap
* Then map the image onto the bitmap
* And finally the save the bitmap to png.

This is then supported by a bunch of functions. For example:

```csharp
private static Bitmap CreateEmptyBitmap(Image image) =>
    new(image.Width, image.Height);

private static Color PixelToColor(Pixel pixel) =>
    Color.FromArgb(
        (int)(pixel.R * 255.999f),
        (int)(pixel.G * 255.999f),
        (int)(pixel.B * 255.999f)
    );
```

Tiny functions, where the function name should be explanatory enough to know what is going on, without really having to dig into the code.

The name-your-expressions approach also results in me having a lot of functions, which returns other functions (or actions). This is to make the main function even more readable. But a function returning a function perhaps looks weird, so it's a bit of a trade-off, I haven't entirely decided on whether I like or not.


Here's an example, with the main function, and a helper function-returning-a-function:

```csharp
public static void ExportImageToPngFile(Image image, string path) =>
    CreateEmptyBitmap(image)
        .Then(MapImageOntoBitmap(image))
        .Finally(SaveToPng(path));

private static Func<Bitmap, Bitmap> MapImageOntoBitmap(Image image) =>
    bmp => TransferPixelsFromImageToBitMap(image, bmp);
```

If I changed the `MapImageOntoBitmap` function to be a normal function, it would instead look like this

```csharp
public static void ExportImageToPngFile(Image image, string path) =>
    CreateEmptyBitmap(image)
        .Then(bmp => MapImageOntoBitmap(image, bmp))
        .Finally(SaveToPng(path));

private static Bitmap MapImageOntoBitmap(Image image, Bitmap bmp) =>
    TransferPixelsFromImageToBitMap(image, bmp);
```

Notice how the `Then(...)` now takes a lambda expression instead, and pass the input parameter to the `MapImageOntoBitmap`. 
This is probably just fine, but I feel that abstracting the lambda expression away makes the `Then(...)` part just slightly more readable.
