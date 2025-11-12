using System;

namespace MerchPlusMAUI.Services
{
    /// <summary>
    /// The distance type to return the results in.
    /// </summary>
    public enum DistanceType { Miles, Kilometers };

    /// <summary>
    /// Specifies a Latitude / Longitude point.
    /// </summary>
    public struct Position
    {
        public double Latitude;
        public double Longitude;
    }

    /// <summary>
    /// Utility class for calculating distances between GPS coordinates using Haversine formula
    /// </summary>
    public class HaversineUtility
    {
        /// <summary>
        /// Returns the distance in miles or kilometers of any two
        /// latitude / longitude points.
        /// </summary>
        /// <param name="pos1">First position</param>
        /// <param name="pos2">Second position</param>
        /// <param name="type">Distance type (Miles or Kilometers)</param>
        /// <returns>Distance between the two points</returns>
        public double Distance(Position pos1, Position pos2, DistanceType type)
        {
            double R = (type == DistanceType.Miles) ? 3960 : 6371;

            double dLat = this.toRadian(pos2.Latitude - pos1.Latitude);
            double dLon = this.toRadian(pos2.Longitude - pos1.Longitude);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(this.toRadian(pos1.Latitude)) * Math.Cos(this.toRadian(pos2.Latitude)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
            double d = R * c;

            return d;
        }

        /// <summary>
        /// Convert to Radians.
        /// </summary>
        /// <param name="val">Value in degrees</param>
        /// <returns>Value in radians</returns>
        private double toRadian(double val)
        {
            return (Math.PI / 180) * val;
        }

        /// <summary>
        /// Check if the current position is within the specified distance from the target position
        /// </summary>
        /// <param name="currentPosition">Current GPS position</param>
        /// <param name="targetPosition">Target GPS position</param>
        /// <param name="maxDistanceMeters">Maximum allowed distance in meters</param>
        /// <returns>True if within distance, false otherwise</returns>
        public bool IsWithinDistance(Position currentPosition, Position targetPosition, double maxDistanceMeters)
        {
            var distanceKm = Distance(currentPosition, targetPosition, DistanceType.Kilometers);
            var distanceMeters = distanceKm * 1000;
            return distanceMeters <= maxDistanceMeters;
        }

        /// <summary>
        /// Get distance in meters between two positions
        /// </summary>
        /// <param name="pos1">First position</param>
        /// <param name="pos2">Second position</param>
        /// <returns>Distance in meters</returns>
        public double GetDistanceInMeters(Position pos1, Position pos2)
        {
            var distanceKm = Distance(pos1, pos2, DistanceType.Kilometers);
            return distanceKm * 1000;
        }
    }
}
