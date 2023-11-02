
    double Length = double.Parse(Console.ReadLine());
    double Width = double.Parse(Console.ReadLine());
    double Height = double.Parse(Console.ReadLine());
try
{
    Box box = new(Length, Width, Height);

    Console.WriteLine($"Surface Area - {box.SurfaceArea():F2}");
    Console.WriteLine($"Lateral Surface Area - {box.LateralSurfaceArea():F2}");
    Console.WriteLine($"Volume - {box.Volume():F2}");
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
