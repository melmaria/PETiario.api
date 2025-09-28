

using AutoMapper;
using PETiario.PETiary.Application.Owners.Dtos.Requests;
using PETiario.PETiary.Application.Owners.Dtos.Responses;
using PETiario.PETiary.Domain.Owners.Entities;

namespace PETiario.PETiary.Application.Owners.Profiles
{
    public class OwnerProfile : Profile
    {
        public OwnerProfile()
        {
            CreateMap<Owner, OwnerResponse>();
            CreateMap<OwnerRequest, Owner>();
        }
        
    }
}