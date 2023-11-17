namespace P02.Graphic_Editor
{
    class Program
    {
        static void Main()
        {
            IShape form = new Circle();
            GraphicEditor editor = new GraphicEditor(form);
            editor.DrawShape();
            IShape rect = new Rectangle();
            GraphicEditor rectangle = new(rect);
            rectangle.DrawShape();
        }
    }
}
