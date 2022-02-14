﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;
using static BL.BL;
using static BO.Exceptions;

namespace BL
{
    class Simulation
    {
        IBL BL;
        IDal Dal;
        public Simulation(IBL BL,int droneID,Action<Drone> dronedroneSimulation, Action<Parcel> parcelSimulation, Func<bool> needToStop)
        {
            int DELAY = 500;
            double SPEED = 1;
            Drone drone = DronesListBL.First(d => d.Id == droneID);
            Parcel parcel = drone.DroneStatus != BO.Enum.DroneStatusesBL.empty ? BL.GetParcel(DronesListBL.First(d => d.Id == droneID).delivery.Id) : null;
            this.BL = BL;
            while (!needToStop())
            {
                try
                {
                    switch (drone.DroneStatus)
                    {
                        case BO.Enum.DroneStatusesBL.empty:
                            try
                            {
                                BL.AssigningPackageToDrone(droneID, true);
                                parcel = BL.GetParcel(DronesListBL.First(d => d.Id == droneID).delivery.Id);
                                parcelSimulation(BL.GetParcel(parcel.Id));
                            }
                            catch
                            {
                                if (drone.BatteryStatus < 100)
                                {
                                    BL.SendDroneToCharge(droneID, true);
                                    Drone updatDrone = DronesListBL.First(d => d.Id == droneID);
                                    Station station = ConvertToBL.ConvertToStationBL(Dal.GetStationList().First(S => S.Longitude == updatDrone.CurrentPosition.Longitude && S.Latitude == updatDrone.CurrentPosition.Latitude));
                                    while(drone.CurrentPosition.Latitude != station.Position.Latitude && drone.CurrentPosition.Longitude != station.Position.Longitude && !needToStop())
                                    {
                                        drone.CurrentPosition.Longitude += drone.CurrentPosition.Longitude > station.Position.Longitude ? -1 : 1;
                                        drone.CurrentPosition.Latitude += drone.CurrentPosition.Latitude > station.Position.Latitude ? -1 : 1;
                                        drone.BatteryStatus -= 0.5;
                                        dronedroneSimulation(drone);
                                        Thread.Sleep(DELAY - 100);
                                    }
                                }
                            }
                            break;
                        case BO.Enum.DroneStatusesBL.maintenance:
                            while (drone.BatteryStatus < 100 && !needToStop()) 
                            {
                                drone.BatteryStatus += SPEED;
                                dronedroneSimulation(drone);
                                Thread.Sleep(DELAY - 100);
                            }
                            BL.ReleaseDroneFromCharging(droneID, true);
                            break;
                        case BO.Enum.DroneStatusesBL.Shipping:
                            Parcel parcelInDrone = BL.GetParcel(drone.delivery.Id);
                            if (parcelInDrone.PickUpBL != null)
                                BL.DeliveryOfAParcelByDrone(droneID, true);
                            else
                                BL.CollectionOfAParcelByDrone(droneID, true);
                            parcelSimulation(BL.GetParcel(parcel.Id));
                            break;
                    }
                }
                catch (ThereIsNotEnoughBatteryException e) {
                    BL.SendDroneToCharge(droneID, true);
                }
                drone = DronesListBL.First(d => d.Id == droneID);
                dronedroneSimulation(drone);
                if(parcel != null) parcelSimulation(BL.GetParcel(parcel.Id));
                Thread.Sleep(DELAY);
            }
        }
    }
}
