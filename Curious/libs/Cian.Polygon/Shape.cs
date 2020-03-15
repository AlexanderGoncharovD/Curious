using System;
using System.Collections.Generic;
using System.Text;

namespace Cian.Polygon
{
    public class Shape
    {
        #region Properties

        public List<GeoPoint> Polygon { get; } = new List<GeoPoint>();

        public GeoPoint Center { get; }

        #endregion

        #region .ctor

        public Shape(GeoPoint center, double radius, int polygons = 60)
        {
            var angle = (360.0 / polygons) * (Math.PI / 180.0);
            Center = center;

            for (int i = 0; i < polygons; i++)
            {
                var point = new GeoPoint(center.Latitude + radius * Math.Cos(angle * i), center.Longitude + (radius / 1.5) * Math.Sin(angle * i));
                Polygon.Add(point);
            }
        }

        #endregion

        #region Public Methods

        public string CreatePolygonLink()
        {
            var text = String.Empty;
            foreach (var point in Polygon)
            {
                text += $"{point.ToString()}%2C";
            }
            text = text.Substring(0, text.Length-3);
            return text;
        }

        #endregion
    }
}
