using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse_Logistics_Dashboard
{
    public class Shipment
    {
        public int TrackingId { get; set; }
        public string City { get; set; }
        public double Weight { get; set; }
        public bool IsFragile { get; set; }

        public Shipment(int trackingId, string city, double weight, bool isFragile)
        {
            this.TrackingId = trackingId;
            this.City = city;
            this.Weight = weight;
            this.IsFragile = isFragile;
        }
    }
}
