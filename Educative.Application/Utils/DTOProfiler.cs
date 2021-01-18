
using System.Linq;
using AutoMapper;
using Educative.Domain.DTO;
using Educative.Domain.Entity;

namespace Educative.Application.Utils
{
    public class DTOProfiler : Profile
    {
        public DTOProfiler()
        {
            CreateMap<Track, TrackCollection>();
            CreateMap<Track, TrackDetails>();
            CreateMap<TrackRequest, Track>();
            CreateMap<Course, CourseCollection>();
            CreateMap<Course, CourseDetails>();
            CreateMap<Tutorial, TutorialCollection>();
            CreateMap<Tutorial, TutorialDetails>();
            CreateMap<MediaObject, MediaObjectDetails>();
        }
    }
}
