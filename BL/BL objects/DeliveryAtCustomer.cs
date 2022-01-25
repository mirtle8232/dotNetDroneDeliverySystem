﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.BL;
using static BO.Enum;

namespace BO
{
    public class DeliveryAtCustomer
    { 
        public DeliveryAtCustomer(DalApi.IDal dalOBG,Parcel parcel, int myId)
        {
            Id = parcel.IdBL;
            Weight = parcel.Weight;
            Priority = parcel.Priority;
            Status = parcel.DeliveredBL != null ? Enum.DeliveryStatus.provided :
                     parcel.PickUpBL != null ? Enum.DeliveryStatus.collected :
                     parcel.ScheduledBL != null ? Enum.DeliveryStatus.associated :
                     Enum.DeliveryStatus.created;
            int idSecondCustomer = parcel.Sender.Id == myId ? parcel.Target.Id : parcel.Sender.Id;
            DO.Customer customer = idSecondCustomer!=0 ?  dalOBG.returnCustomerArray().ToList().First(customer => customer.Id == idSecondCustomer) : new DO.Customer();
            Customer = customer .isActive ? new CustomerOnDelivery(customer.Id, customer.Name) : new CustomerOnDelivery(customer.Id, customer.Name, false);
        }
        public override string ToString()
        {
            return $"----------------\nID: {Id}\nWeight: {Weight}\nPriotity: {Priority}\nStatus: {Status}\nAnother Customer in Parcel: {Customer.ToString()}\n----------------";
        }
        public int Id { get; set; }
        WeightCategoriesBL Weight { get; set; }
        PrioritiesBL Priority { get; set; }
        DeliveryStatus Status { get; set; }     
        CustomerOnDelivery Customer { get; set; }
    }
}
