﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct DroneDAL
    {
        public int Id { get; set; }
        public string Model { get;set;}
        public WeightCategories MaxWeight { get; set;}
        public double Battery { get; set; }
    };
}

