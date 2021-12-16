﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.BL;
using static IBL.BO.EnumBL;

namespace IBL.BO
{
    public class ParcelByTransfer
    {
        public ParcelByTransfer(ParcelBL parcel)
        {
            Id = parcel.IdBL;
            IsDelivery = parcel.PickUpBL != null;
            Priority = parcel.Priority;
            Weight = parcel.Weight;
            Sender = parcel.Sender;
            Target = parcel.Target;
            IDAL.DO.CustomerDAL dalobj = DalObject.DataSource.MyCustomers.ToList().First(customer => customer.Id == parcel.Sender.Id);
            CollectionLocation = new Position(dalobj.Longitude, dalobj.Latitude);
            dalobj = DalObject.DataSource.MyCustomers.ToList().First(customer => customer.Id == parcel.Target.Id);
            DeliveryDestinationLocation = new Position(dalobj.Longitude, dalobj.Latitude);
            Distance = DistanceBetweenCoordinates.CalculateDistance(CollectionLocation, DeliveryDestinationLocation);
        }
        public override string ToString()
        {
            string status = IsDelivery ? "On the way to the destination" : "Awaiting collection";
            return $"--------------\nID: {Id}\nStatus: {status}\nPriority: {Priority}\nWeight: {Weight}\nSender: {Sender.ToString()}\nTarget: {Target.ToString()}\nCollection Location: {CollectionLocation}\nTarget Location: {DeliveryDestinationLocation}\nDistance: {Distance}\n--------------";
        }
        public int Id { get; set; }
        WeightCategoriesBL Weight { get; set; }
        PrioritiesBL Priority { get; set; }
        public bool IsDelivery { get; set; }
        Position CollectionLocation { get; set; }
        Position DeliveryDestinationLocation { get; set; }
        CustomerOnDelivery Sender { get; set; }
        CustomerOnDelivery Target { get; set; }
        double Distance { get; set; }
    }
}