﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Models
{
    public class Humidity : IMeasurement
    {
        #region Constructor
        public Humidity() { }
        public Humidity(double measurement, ILocation location)
        {
            this.Measurement = measurement;
            this.Location = location;
        }
        #endregion

        #region Properties
        public string Name { get { return typeof(Humidity).Name; } }
        public double Measurement { get; set; }
        public ILocation Location { get; set; }
        #endregion
    }
}
