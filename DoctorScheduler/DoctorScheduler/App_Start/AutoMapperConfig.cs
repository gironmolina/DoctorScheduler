using System;
using AutoMapper;
using DoctorScheduler.Application.Dtos;
using DoctorScheduler.Entities;

namespace DoctorScheduler.API
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // TODO Swagger, Tests, Validations

            Mapper.Initialize(cfg => {
                cfg.CreateMap<SchedulerEntity, SchedulerDto>();
                cfg.CreateMap<TakeSlotEntity, TakeSlotDto>();
            });
        }
    }
}