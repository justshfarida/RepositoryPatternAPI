using AutoMapper;
using RepositoryPatternApi.Application.DTOs;
using RepositoryPatternApi.Domain.Entities;
using RepositoryPatternAPI.Data.Entities;
using RepositoryPatternAPI.DTOs;

namespace RepositoryPatternAPI
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        { 
            CreateMap<Author, AuthorDTO>();
            CreateMap<AuthorDTO, Author>();
            CreateMap<BookDTO, Book>().ReverseMap();
        }
    }
}
