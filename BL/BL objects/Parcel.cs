﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.BL;
using static BO.Enum;

namespace BO
{
    public class Parcel
    {
        DalApi.IDal DalObj;
        public Parcel( DalApi.IDal dalOB, int idSender, int idTarget, int weight, int priority, bool isActive = true, int id = -1, DateTime? requested = null, DateTime? scheduled = null, DateTime? pickUp = null, DateTime? delivered = null, int droneID = -1 )
        {
            DalObj = dalOB;
            Id = id > 0 ? id : DalObj.GetNewParcelId();
            Weight = (WeightCategoriesBL)weight;
            Priority = (PrioritiesBL)priority;
            ScheduledBL = scheduled;
            PickUpBL = pickUp;
            DeliveredBL = delivered;
            RequestedBL = requested == null? DateTime.Now : requested;
            DroneIdBL = droneID != -1 ? new DroneInParcel(DronesListBL.First(drone => drone.Id == droneID)) : null;
            DO.Customer customer = DalObj.returnCustomerArray().ToList().First(customer => customer.Id == idSender);
            Sender = customer.isActive ? new CustomerOnDelivery(customer.Id, customer.Name) : new CustomerOnDelivery(customer.Id, customer.Name,false);
            customer = DalObj.returnCustomerArray().ToList().First(customer => customer.Id == idTarget);
            Target = customer.isActive ? new CustomerOnDelivery(customer.Id, customer.Name): new CustomerOnDelivery(customer.Id, customer.Name,false);
            this.IsActive = isActive;
        }
        public override string ToString()
        {
            Console.WriteLine($"ID: {Id}\nSender: {Sender.ToString()}\nTarget: {Target.ToString()}\nWeight: {Weight}\nPriority: {Priority}\nRequested Time: {RequestedBL}");
            if (ScheduledBL != null) { Console.WriteLine($"Scheduled Time: {ScheduledBL}"); }
            if (PickUpBL != null) { Console.WriteLine($"PickUp Time: {PickUpBL}"); }
            if (DeliveredBL != null) { Console.WriteLine($"Delivered Time: {DeliveredBL}"); }
            return "";
        }

      
        public int Id { get; set; }
        public WeightCategoriesBL Weight { get; set; }
        public PrioritiesBL Priority { get; set; }
        public DroneInParcel DroneIdBL { get; set; }
        public DateTime? RequestedBL { get; set; }
        public DateTime? ScheduledBL { get; set; }
        public DateTime? PickUpBL { get; set; }
        public DateTime? DeliveredBL { get; set; }
        public CustomerOnDelivery Sender { get; set; }
        public CustomerOnDelivery Target { get; set; }
        public bool IsActive { get; set; }
    }
}
