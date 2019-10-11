using AutoMapper;
using AutoMapper.Configuration;
using domain.repository.entities;
using MD.PersianDateTime.Core;
using shared.model.dashboard_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace presentation.dashboard.helpers {
    public class MapperConfig {
        public MapperConfiguration Init() {
            return new MapperConfiguration(config => config.AddProfile(new MappingProfile()));
        }
    }
    public class MappingProfile: Profile {
        public MappingProfile() {
            // General

            // Admin
            CreateMap<Admin_Entity, AccountPrincipal>()
                .ForMember(d => d.LastSignedin, s => s.MapFrom(f => new PersianDateTime(f.LastSignedin).ToShortDateTimeString()));

            CreateMap<Admin_Entity, Admin_DashboardModel>();
            CreateMap<Admin_DashboardModel, Admin_Entity>();

            // User
            CreateMap<User_Entity, User_DashboardModel>();
            CreateMap<User_DashboardModel, User_Entity>();
        }
    }
}
