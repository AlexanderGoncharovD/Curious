using System;
using Xamarin.Essentials;

namespace Cian.Polygon
{
    /// <summary>
    ///     Точка локации, стостоит из широты и долготы
    /// </summary>
    public class GeoPoint
    {
        #region Fields

        private readonly double _longitude;
        private readonly double _latitude;

        #endregion

        #region Properties

        /// <summary>
        ///     Долгота
        /// </summary>
        public double Longitude { get => _longitude; }

        /// <summary>
        ///     Широта
        /// </summary>
        public double Latitude { get => _latitude; }


        #endregion

        #region .ctor

        public GeoPoint(double lat, double lng)
        {
            _latitude = ConvertPoint(lat);
            _longitude = ConvertPoint(lng);
        }

        public GeoPoint(Location location)
        {
            _latitude = ConvertPoint(location.Latitude);
            _longitude = ConvertPoint(location.Longitude);
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Округлить число до 4-ч знаков после запятой
        /// </summary>
        /// <param name="point">
        ///     Число подлежащее округлению
        /// </param>
        /// <returns>
        ///     Округленное число
        /// </returns>
        private double ConvertPoint(double point)
        {
            return Math.Round(point, 4);
        }

        #endregion

        public override string ToString()
        {
            var lng = Longitude.ToString().Replace(',', '.');
            var lat = Latitude.ToString().Replace(',', '.');
            return $"{lng}_{lat}";
        }
    }
}
