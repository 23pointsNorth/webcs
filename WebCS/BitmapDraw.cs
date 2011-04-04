using System.Collections.Generic;
using System.Drawing;

static class BitmapDraw
{
    public static Bitmap FilledRectangle(int width, int height, Color fillColour)
    {
        Bitmap filledBitmap = new Bitmap(width, height);

        using (Graphics graphicsOnImage = Graphics.FromImage(filledBitmap))
        using (SolidBrush brush = new SolidBrush(fillColour))
        {
            graphicsOnImage.FillRectangle(brush, 0, 0, width, height);
        }
        return filledBitmap;
    }

    public static Bitmap Rectangle(Bitmap image, Rectangle rect, Pen pen)
    {
        using (Graphics g = Graphics.FromImage(image))
        {
            g.DrawRectangle(pen, rect);
        }
        return image;
    }

    public static Bitmap Rectangle(Bitmap image, Rectangle[] rect, Pen pen)
    {
        using (Graphics g = Graphics.FromImage(image))
        {
            foreach (var r in rect)
            {
                g.DrawRectangle(pen, r);
            }
        }
        return image;
    }

    public static Bitmap Rectangle(Bitmap image, Rectangle[] rect, Pen[] pen)
    {
        if (rect.Length == pen.Length)
        {
            using (Graphics g = Graphics.FromImage(image))
            {
                for (int i = 0; i < rect.Length; i++)
                {
                    g.DrawRectangle(pen[i], rect[i]);
                }
            }
        }
        return image;
    }


    public static Bitmap Rectangle(Bitmap image, Rectangle[] rect, Color[] color)
    {
        if (rect.Length == color.Length)
        {
            using (Graphics g = Graphics.FromImage(image))
            {
                for (int i = 0; i < rect.Length; i++)
                {
                    g.DrawRectangle(new Pen(color[i], 2), rect[i]);
                }
            }
        }
        return image;
    }

    public static Bitmap Rectangle(Bitmap image, Dictionary<Rectangle,Color> rectangles)
    {
        using (Graphics g = Graphics.FromImage(image))
        {
            foreach (var rect in rectangles)
            {
                g.DrawRectangle(new Pen(rect.Value, 2), rect.Key);
            }
        }
        return image;
    }

    public static Bitmap Resize(Bitmap toResize, int newWidth, int newHeight)
    {
        Bitmap result = new Bitmap(newWidth, newHeight);
        using (Graphics graphic = Graphics.FromImage((System.Drawing.Image)result))
            graphic.DrawImage(toResize, 0, 0, newWidth, newHeight);
        return result;
    }

    public static Bitmap WriteString(Bitmap layer,string text)
    {
        Bitmap emptyBitmap = (Bitmap)layer.Clone();
        using (Graphics g = Graphics.FromImage(emptyBitmap))
        {
            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Center;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.DrawString(text, new Font("Arial", 20), Brushes.Black,
                new RectangleF(0, 0, 352, 288), strFormat);
        }
        return emptyBitmap;
    }
}
