using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.BUS
{
    public class Compoment
    {
        public static Image ResizeImage(Image img, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, 0, 0, width, height);
            }
            return bmp;
        }


        public static void ExportToXML(DataTable dt, string filePath)
        {
            dt.WriteXml(filePath, XmlWriteMode.WriteSchema);
        }
        public static DataTable ImportFromXML(string filePath)
        {
            DataTable dt = new DataTable();
            dt.ReadXml(filePath);
            return dt;
        }


    }
}
