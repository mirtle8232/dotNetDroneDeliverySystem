﻿using DalObject;
using IBL.BO;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Exceptions;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        public void DisplayStatoin(int idS)
        {
            
        }
        public void DisplayDrone(int idD)
        {
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~ drone data ~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine(DronesListBL.First(drone => drone.getIdBL() == idD).ToString());
            ParcelBL parcel = ConvertToBL.ConvertToParcelBL(DalObj.returnParcelArray().First(parcel => parcel.DroneId == idD));
            Console.WriteLine(new ParcelByTransfer(parcel).ToString());
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }
        public void DisplayCustomer(int idC)
        {
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~ customer data ~~~~~~~~~~~~~~~~~~~~~~~");

            Console.WriteLine(ConvertToBL.ConvertToCustomrtBL(DalObj.returnCustomer(idC)).ToString());
            Console.WriteLine("Parcels sent by this customer: ");
            foreach(ParcelDAL parcel in DalObj.returnParcelArray()) {
                if(parcel.SenderId == idC) {
                    Console.WriteLine(new DeliveryAtCustomer(ConvertToBL.ConvertToParcelBL(parcel)).ToString());
                }
            }
            Console.WriteLine("Parcels that this customer receives: ");
            foreach (ParcelDAL parcel in DalObj.returnParcelArray()) {
                if (parcel.TargetId == idC) {
                    Console.WriteLine(new DeliveryAtCustomer(ConvertToBL.ConvertToParcelBL(parcel)).ToString());
                }
            }
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }
        public void DisplayParcel(int idP)
        {
            ParcelBL parcel = ConvertToBL.ConvertToParcelBL(DalObj.returnParcel(idP));
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~ parcel data ~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine(parcel.ToString());
            if (parcel.DroneIdBL != null) { Console.WriteLine("In Drone: "+parcel.DroneIdBL.ToString()); }
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }
        public void DisplayStatoinList()
        {
            
        }
        public void DisplayDroneList()
        {
            foreach(DroneBL drone in DronesListBL)
            {
                Console.WriteLine(new DroneToList(drone).ToString());
            }
        }

        public void DisplayCustomerList()
        {
            foreach (CustomerDAL customer in DalObj.returnCustomerArray())
            {
                Console.WriteLine(new CustomerToList(ConvertToBL.ConvertToCustomrtBL(customer)).ToString());
            }
        }

        public void DisplayParcelList()
        {
            foreach (ParcelDAL parcel in DalObj.returnParcelArray())
            {
                Console.WriteLine(new ParcelToList(ConvertToBL.ConvertToParcelBL(parcel)).ToString());
            }
        }

        public void DisplayParcelsThatHaveNotYetBeenAssociatedWithADrone()
        {
            foreach (ParcelBL parcel in ConvertToBL.ConvertToParcelArrayBL(DalObj.returnParcelArray().ToList()))
            {
                if (parcel.DroneIdBL == null) { Console.WriteLine(new ParcelToList(parcel).ToString()); }
            }
        }

    }
}
