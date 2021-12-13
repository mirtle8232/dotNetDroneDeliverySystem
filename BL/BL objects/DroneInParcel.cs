﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class DroneInParcel
    {
        public DroneInParcel(DroneBL drone)
        {
            Id = drone.getIdBL();
            BatteryStatus = drone.BatteryStatus;
            CurrentPosition = drone.CurrentPosition;
        }
        public override string ToString()
        {
            return $"----------------\nID: {Id}\nBattery Status: {BatteryStatus}\nPosition - {CurrentPosition.ToString()}";
        }
        public int Id { get; set; }
        public double BatteryStatus { get; set; }
        public Position CurrentPosition { get; set; }
    }
}
