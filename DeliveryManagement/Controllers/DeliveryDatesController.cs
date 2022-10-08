using DeliveryManagement.Model;
using DeliveryManagement.Seed;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeliveryDatesController : ControllerBase
    {
      
        private readonly ILogger<DeliveryDatesController> _logger;

        public DeliveryDatesController(ILogger<DeliveryDatesController> logger)
        {
            _logger = logger;
        }

        [HttpPost("GetAvailableDeliveryDates")]
        public IEnumerable<AvailableDates> GetAvailableDeliveryDates([FromQuery] int postalCode, [FromBody] IList<Product> productsModel)
        {
            try
            {

                var availableProductList = new SeedData().GetProducts();
                var orderedProducts = availableProductList.Where(x => productsModel.Select(pm => pm.productId).Contains(x.productId)).ToList();
                // Get weekdays which are not ok for delivery
                var nonDeliveryWeekDays = GetNonDeliveryWeekDays(orderedProducts);

                //Get max Advance days before that delivery is not possible.
                var maxDaysInAdvance = orderedProducts.Select(a => a.daysInAdvance).ToList().Max();

                //If any product is external and whole delivery should be delayed by 5 days
                if (maxDaysInAdvance < 5 && orderedProducts.Any(x => x.productType == ProductType.external))
                {
                    maxDaysInAdvance = 5;
                }

                //Get list of dates in next 14 days along with if these days are green procurement.
                int n = maxDaysInAdvance;

                DateTime deliveryDate = DateTime.Today.AddDays(maxDaysInAdvance);
                var availableDatesList = new List<AvailableDates>();
                while (n <= 14)
                {
                    if (!nonDeliveryWeekDays.Contains(deliveryDate.DayOfWeek))
                    {
                        var availableDates = new AvailableDates
                        {
                            deliveryDate = deliveryDate,
                            isGreenProcurement = CheckIsGreenProcurement(deliveryDate),
                            postalCode = postalCode,
                        };
                        availableDatesList.Add(availableDates);

                    }
                    n++;
                    deliveryDate = deliveryDate.AddDays(1);
                }

                return availableDatesList.OrderBy(x => !x.isGreenProcurement);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// Get all weekdays which are not available for delivery
        private IList<DayOfWeek> GetNonDeliveryWeekDays(IList<Product> orderedProducts)
        {
            var allweekDayList = Enum.GetValues(typeof(DayOfWeek))
                            .Cast<DayOfWeek>().ToList();
            List<DayOfWeek> dayOfWeeks = new List<DayOfWeek>();
            foreach (var product in orderedProducts)
            {
                var missingWeekDays = allweekDayList.Except(product.deliveryDays);
                dayOfWeeks.AddRange(missingWeekDays);
            }
            dayOfWeeks = dayOfWeeks.Distinct().ToList();
            return dayOfWeeks;
        }

        private bool CheckIsGreenProcurement(DateTime DeliveryDate)
        {
            if (DeliveryDate.DayOfWeek==DayOfWeek.Wednesday || DeliveryDate.Day == 5 || DeliveryDate.Day == 15 || DeliveryDate.Day == 25)
            {
                return true;
            }
            return false;
        }
    }
}
