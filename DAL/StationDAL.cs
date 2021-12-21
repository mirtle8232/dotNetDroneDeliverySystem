﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DO
{
   public struct StationDAL
    {
        public int Id { get;set;}
        public string Name { get;set;}
        public int EmptyChargeSlots { get;set;}
        public double Longitude { get;set;}
        public double Latitude { get;set;}
        public int DronesInCharging { get; set; }
    };
    
}
