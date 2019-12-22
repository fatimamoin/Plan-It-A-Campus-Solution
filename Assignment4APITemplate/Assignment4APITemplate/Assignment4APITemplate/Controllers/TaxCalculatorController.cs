using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment4APITemplate.Controllers
{
    public class TaxCalculatorController : ApiController
    {
        
        [HttpGet]
        public int RemainingSeats(int remaining)
        {

            int seats = remaining - 1;
            if (seats > -1)
            {
                return seats;
            }
            return -1;
        }
    }
}


