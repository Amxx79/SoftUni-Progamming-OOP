using System;

namespace P02.Graphic_Editor
{
    public class GraphicEditor
    {
        private IShape shape;
        public GraphicEditor(IShape shape)
        {
            this.shape = shape;
        }
        public void DrawShape()
        {
            if (shape is Circle)
            {
                Console.WriteLine("I'm Circle");
            }
            else if (shape is Rectangle)
            {
                Console.WriteLine("I'm Recangle");
            }
            else if (shape is Square)
            {
                Console.WriteLine("I'm Square");
            }
        }
    }
}
