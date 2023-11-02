    public class Box
    {
        private double length;
        private double width;
        private double height;


        public Box(double length, double width, double height)
        {
            Length = length;
            Widht = width;
            Heigth = height;
        }

        public double Length
        {
            get
            {
                return length;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Length cannot be zero or negative.");
                }
                length = value;
            }
        }

        public double Widht
        {
            get
            {
                return width;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Width cannot be zero or negative.");
                }
                width = value;
            }
        }

        public double Heigth
        {
            get
            {
                return height;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Heigth cannot be zero or negative.");
                }
                height = value;
            }
        }

        public double SurfaceArea() => (2 * Length * Widht) + (2 * Length * Heigth) + (2 * Widht * Heigth);
        public double LateralSurfaceArea() => (2 * Length * Heigth) + (2 * Widht * Heigth);
        public double Volume() => Length * Widht * Heigth;
    }