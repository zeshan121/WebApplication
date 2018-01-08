using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Unity;
using System.Web.Mvc;
using Unity.Mvc5;
using MyWebApp.Web.Models.GamingMachine;
using AutoMapper;
using MyWebApp.Core.Entities;
using MyWebApp.Core.Interfaces;
using MyWebApp.Core.Services;
using MyWebApp.Infrastructure.Data;

namespace MyWebApp.Web.App_Start
{
    public static class UnityConfig
    {
        public static UnityContainer Container;

        public static void RegisterComponents()
        {
            Container = new UnityContainer();
            
            var config = new MapperConfiguration(cfg =>
            {
                //Create all maps here
                cfg.CreateMap<GamingMachineInfoModel, GamingMachine>()
                .ForMember(dest => dest.CreateDate, opt => opt.Ignore());

            });

            IMapper mapper = config.CreateMapper();            
            Container.RegisterType<IGamingMachine, GamingMachineService>();
            Container.RegisterType(typeof(IRepository<>), typeof(InMemoryRepository<>));
            Container.RegisterInstance(mapper);            
            DependencyResolver.SetResolver(new UnityDependencyResolver(Container));
        }
    }
}