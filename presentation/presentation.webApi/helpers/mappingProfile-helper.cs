using AutoMapper;
using domain.repository.models;
using domain.repository.schemas;
using shared.model.bindingModels;
using shared.model.viewModels;
using shared.utility._app;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace presentation.webApi.helpers {
    public class MapperConfig {
        public MapperConfiguration Init() {
            return new MapperConfiguration(config => config.AddProfile(new MappingProfile()));
        }
    }

    public class MappingProfile: Profile {
        public MappingProfile() {
            // General
            CreateMap<FullHeader_BindingModel, Void_Schema>();

            // Customer
            CreateMap<Customer_Model, Customer_ViewModel>();
            CreateMap<Customer_GetById_Model, Customer_GetById_ViewModel>();
            CreateMap<Customer_GetById_BindingModel, Customer_GetPaging_Schema>();

            // User
            CreateMap<User_SignIn_BindingModel, User_SignIn_Schema>();
            CreateMap<User_Model, User_SignUp_ViewModel>();
            CreateMap<UserProperty_Model, UserProperty_ViewModel>();
            CreateMap<User_TwoFactorAuthentication_BindingModel, User_EnableTwoFactorAuthentication_Schema>();
            CreateMap<User_TwoFactorAuthentication_BindingModel, User_DisableTwoFactorAuthentication_Schema>();
            CreateMap<User_Verify_BindingModel, User_SetVerificationCode_Schema>();
            CreateMap<User_Verify_BindingModel, User_Verify_Schema>();
            CreateMap<User_Update_BindingModel, User_Update_Schema>()
                .ForMember(d => d.BirthDate, s => s.MapFrom(f => f.BirthDate.ToDateTime(null)));
            CreateMap<User_DisableMe_BindingModel, User_DisableMe_Schema>();

            // SendMessageQueue
            CreateMap<SendMessageQueue_GetPaging_BindingModel, SendMessageQueue_GetPaging_Schema>();

            // Business
            CreateMap<Business_GetByLocation_BindingModel, Business_GetByLocation_Schema>();
            CreateMap<Business_GetPaging_BindingModel, Business_GetPaging_Schema>();
            CreateMap<Business_New_BindingModel, Business_New_Schema>();
            CreateMap<Business_Edit_BindingModel, Business_Edit_Schema>();
            CreateMap<Business_Model, Business_ViewModel>();

            // Image
            CreateMap<Image_BindingModel, Image_Entity>()
                .ForMember(d => d.Path, s => s.MapFrom(f => Path.GetDirectoryName(f.FullName)))
                .ForMember(d => d.Name, s => s.MapFrom(f => Path.GetFileNameWithoutExtension(f.FullName)))
                .ForMember(d => d.Extension, s => s.MapFrom(f => Path.GetExtension(f.FullName)));
            CreateMap<Image_Model, Image_ViewModel>()
                .ForMember(d => d.FullName, s => s.MapFrom(f => $"{f.Path}{f.Name}{f.Extension}"));
        }
    }
}
