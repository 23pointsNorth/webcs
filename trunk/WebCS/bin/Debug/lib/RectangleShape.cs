using System;
using System.Drawing;


    public class RectangleShape
    {
        private Color rectangleColour;
        private Size rectangleSize;
        private MouseClickPoint rectangleUpperLeftAngle;
        private MouseClickPoint mouseClickStartPoint;
        private MouseClickPoint mouseClickEndPoint;
        
        
        public struct MouseClickPoint
        {
            private int x;
            private int y;

            public MouseClickPoint(int xCoordinate,int yCoordinate)
            {
                this.x = xCoordinate;
                this.y = yCoordinate;
                if (xCoordinate < 0)
                {
                    this.X = 0;
                }
                if (xCoordinate > Constants.IMAGE_WIDTH)
                {
                    this.X = Constants.IMAGE_WIDTH;
                }
                if (yCoordinate < 0)
                {
                    this.Y = 0;
                }
                if (yCoordinate > Constants.IMAGE_HEIGHT)
                {
                    this.Y = Constants.IMAGE_HEIGHT;
                }
            }

            public int X
            {
                set { x = value; }
                get { return x; }
            }
            public int Y
            {
                set { y = value; }
                get { return y; }
            }
        }

        public Color RectangleColour
        {
            set { this.rectangleColour = value; }
            get { return this.rectangleColour; }
        }

        public Size RectangleSize
        {
            set { this.rectangleSize = value; }
            get { return this.rectangleSize; }
        }

        public MouseClickPoint RectangleUpperLeftAngle
        {
            set { this.rectangleUpperLeftAngle = value; }
            get { return this.rectangleUpperLeftAngle; }
        }

        public MouseClickPoint MouseClickStartPoint
        {
            set { this.mouseClickStartPoint = value; }
            get { return this.mouseClickStartPoint; }
        }

        public MouseClickPoint MouseClickEndPoint
        {
            set { this.mouseClickEndPoint = value; }
            get { return this.mouseClickEndPoint; }
        }

        public RectangleShape(
            MouseClickPoint rectUpperLeftAngle,Size rectSize, Color rectColour)
        {
            rectangleUpperLeftAngle = rectUpperLeftAngle;
            rectangleSize = rectSize;
            rectangleColour = rectColour;
        }

        public RectangleShape(MouseClickPoint mouseClickStart, MouseClickPoint mouseClickEnd, Color drawColour)
        {
            this.MouseClickStartPoint = mouseClickStart;
            this.MouseClickEndPoint = mouseClickEnd;
            this.RectangleColour = drawColour;
            this.CalculateRetangle();
        }

        public Bitmap DrawOnImage(Bitmap image)
        {
            //draw a rectangle;
            lock (image)
            {
                using (Graphics graphicsOnImage = Graphics.FromImage(image))
                using (Pen rectanglePen = new Pen(RectangleColour))
                {
                    graphicsOnImage.DrawRectangle(
                        rectanglePen,
                        RectangleUpperLeftAngle.X, RectangleUpperLeftAngle.Y,
                        RectangleSize.Width, RectangleSize.Height);
                }
                return image;
            }

        }

        private void CalculateRetangle()
        {
            rectangleSize = new Size(
                Math.Abs(mouseClickStartPoint.X - mouseClickEndPoint.X),
                Math.Abs(mouseClickStartPoint.Y - mouseClickEndPoint.Y));

            rectangleUpperLeftAngle = new MouseClickPoint(
                Math.Min(mouseClickStartPoint.X, mouseClickEndPoint.X),
                Math.Min(mouseClickStartPoint.Y, mouseClickEndPoint.Y));

            rectangleUpperLeftAngle = new MouseClickPoint(
                Math.Min(rectangleUpperLeftAngle.X, Constants.IMAGE_WIDTH),
                Math.Min(rectangleUpperLeftAngle.Y, Constants.IMAGE_HEIGHT));

            rectangleUpperLeftAngle = new MouseClickPoint(
                Math.Max(rectangleUpperLeftAngle.X, 0),
                Math.Max(rectangleUpperLeftAngle.Y, 0));
        }

        public static Bitmap DrawFilledRectangle(int width, int height,  Color fillColour)
        {
            Bitmap filledBitmap = new Bitmap(width, height);

            using (Graphics graphicsOnImage = Graphics.FromImage(filledBitmap))
            using (SolidBrush brush = new SolidBrush(fillColour))
            {
                graphicsOnImage.FillRectangle(brush, 0, 0, width, height);
            }
            return filledBitmap;
        }
    }
