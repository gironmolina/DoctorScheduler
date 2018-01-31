using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DoctorScheduler.Application.Dtos;
using DoctorScheduler.Application.Interfaces;
using DoctorScheduler.Application.Services;
using DoctorScheduler.Infrastucture.Exceptions;
using log4net;
using Swashbuckle.Swagger.Annotations;

namespace DoctorScheduler.API.Controllers
{
    public class SchedulerController : ApiController
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SchedulerController));

        private readonly ISchedulerAppService schedulerAppService;
        
        public SchedulerController(ISchedulerAppService schedulerAppService)
        {
            this.schedulerAppService = schedulerAppService ?? throw new ArgumentNullException(nameof(schedulerAppService));
        }

        /// <summary>Get weekly availability.</summary>>
        /// <response code="200">Returns weekly availability.</response>
        /// <response code="400">API Bad request.</response>
        /// <response code="500">Server found an unexpected error.</response>
        [HttpGet]
        [Route("api/v1/availability")]
        [ResponseType(typeof(SchedulerWeekDto))]
        public async Task<IHttpActionResult> GetWeeklyAvailability([FromUri] string date)
        {
            try
            {
                var response = await this.schedulerAppService.GetWeeklyAvailabilityAdapter(date).ConfigureAwait(false);
                return this.Ok(response);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                if (e is SchedulerBadRequestException)
                {
                    return this.BadRequest();
                }

                return this.InternalServerError(e);
            }
        }

        /// <summary>Take a selected slot.</summary>>
        /// <response code="200">Slot taked successfully.</response>
        /// <response code="400">API Bad request.</response>
        /// <response code="500">Server found an unexpected error.</response>
        [HttpPost]
        [Route("api/v1/takeSlot")]
        [SwaggerResponse(HttpStatusCode.OK, "Slot taked successfully")]
        public async Task<IHttpActionResult> TakeSlot([FromBody] TakeSlotDto slot)
        {
            try
            {
                var response = await this.schedulerAppService.TakeSlotAdapter(slot).ConfigureAwait(false);
                if (!response)
                {
                    return this.BadRequest();
                }

                return this.Ok();
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                return this.InternalServerError(e);
            }
        }
    }
}