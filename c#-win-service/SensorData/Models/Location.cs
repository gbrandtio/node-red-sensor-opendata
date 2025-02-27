﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Interfaces;

namespace Models
{
    /// <summary>
    /// Represents a location on the map based on the longitude, latitude and contry. Based on the provided 
    /// longitude and latitude, this class will find and store the city that the specified location belongs to.
    /// Members: 
    /// - Longitude: The longitude of the specific location.
    /// - Latitude: The latitude of the location.
    /// - Country: The country of the location.
    /// - City: The city that the location belongs to.
    /// Provides also the ability to find location info based on the provided longitude and latitude via Google Geocoding API.
    /// </summary>
    public class Location : ILocation
    {
        #region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Location() {}

        /// <summary>
        /// Constructor that instantiates a location object with the passed values.
        /// </summary>
        /// <param name="longitude">The longitude value</param>
        /// <param name="latitude">Tha latitude value</param>
        /// <param name="country">The country value (2-letter code)</param>
        public Location(double longitude, double latitude, string country)
        {
            this.Longitude = longitude;
            this.Latitude = latitude;
            this.Country = country;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Longitude property.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Latitude property.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Country property.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// City property.
        /// </summary>
        public string City { get; set; }
        #endregion
    }
}
