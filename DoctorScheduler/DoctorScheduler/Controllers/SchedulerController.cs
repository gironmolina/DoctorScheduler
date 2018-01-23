using System;
using System.Threading.Tasks;
using System.Web.Http;
using DoctorScheduler.Application.Dtos;
using DoctorScheduler.Application.Services;

namespace DoctorScheduler.API.Controllers
{
    public class SchedulerController : ApiController
    {
        private readonly ISchedulerAppService schedulerAppService;

        public SchedulerController(ISchedulerAppService schedulerAppService)
        {
            this.schedulerAppService = schedulerAppService ?? throw new ArgumentNullException(nameof(schedulerAppService));
        }

        [HttpGet]
        [Route("api/v1/availability")]
        public async Task<IHttpActionResult> GetWeeklyAvailability([FromUri] string date)
        {
            try
            {
                var response = await this.schedulerAppService.GetWeeklyAvailabilityAdapter(date).ConfigureAwait(false);
                return this.Ok(response);
            }
            catch (Exception e)
            {
                return this.InternalServerError(e);
            }
        }

        [HttpPost]
        [Route("api/v1/takeSlot")]
        public async Task<IHttpActionResult> TakeSlot([FromBody] TakeSlotDto slot)
        {
            try
            {
                var response = await this.schedulerAppService.TakeSlotAdapter(slot).ConfigureAwait(false);
                return ResponseMessage(response);
            }
            catch (Exception e)
            {
                return this.InternalServerError(e);
            }
        }
    }
}