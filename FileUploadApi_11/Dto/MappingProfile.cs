using AutoMapper;
using FileUploadApi_11.Models;

namespace FileUploadApi_11.Dto
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<BatchUploadDetail, BatchUploadDto>().ReverseMap();
        }
    }
}
