using System;

namespace MepAirlines.BusinessLogic
{
    public interface IDistanceCalculator
    {
        float GetDistanceInKm(float lat1, float lon1, float lat2, float lon2);
    }

    public sealed class DistanceCalculator : IDistanceCalculator
    {
        public const float EarthRadiusInMeter = 6371000;

        public float GetDistanceInKm(float lat1, float lon1, float lat2, float lon2)
        {
            var φ1 = ToRadian(lat1);
            var φ2 = ToRadian(lat2);
            var Δφ = ToRadian(lat2 - lat1);
            var Δλ = ToRadian(lon2 - lon1);

            var a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2) +
                    Math.Cos(φ1) * Math.Cos(φ2) *
                    Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var d = EarthRadiusInMeter * c;

            return (float)d / 1000f;
        }

        private float ToRadian(float degree) => (float)(Math.PI * degree / 180.0);

    }
}
