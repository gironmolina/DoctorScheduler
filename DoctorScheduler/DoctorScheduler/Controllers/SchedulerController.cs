using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DoctorScheduler.Application.Services;

namespace DoctorScheduler.Controllers
{
    public class SchedulerController : ApiController
    {
        private readonly ISchedulerService schedulerService;

        public SchedulerController(ISchedulerService schedulerService)
        {
            this.schedulerService = schedulerService ?? throw new ArgumentNullException(nameof(schedulerService));
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("api/v1/availability")]
        public async Task<IHttpActionResult> GetWeeklyAvailability()
        {
            try
            {
                var response = await this.schedulerService.GetWeeklyAvailability().ConfigureAwait(false);
                return this.Ok(response);
            }
            catch (Exception e)
            {
                return this.InternalServerError(e);
            }
        }
    }
}