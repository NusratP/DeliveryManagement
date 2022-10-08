using System;

namespace DeliveryManagement.Model
{
    public class AvailableDates
    {
        public int postalCode { get; set; }
        public DateTime deliveryDate { get; set; }
        public bool isGreenProcurement { get; set; }
    }
}
