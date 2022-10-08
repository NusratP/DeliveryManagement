using System;
using System.Collections.Generic;

namespace DeliveryManagement.Model
{
    public class Product
    {
        public int productId { get; set; }
        public string name { get; set; }
        public IList<DayOfWeek> deliveryDays { get; set; }
        public ProductType productType { get; set; }
        public int daysInAdvance { get; set; }
    }

    //public enum DeliveryDays
    //{
    //    sunday = 1,
    //    monday,
    //    tuesday,
    //    wednesday,
    //    thursday,
    //    friday,
    //    saturday
    //}

    public enum ProductType
    {
       normal = 0,
       external,
       temporary
    }

  
}
