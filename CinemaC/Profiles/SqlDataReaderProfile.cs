using System;
using System.Data.SqlClient;
using System.Linq;
using AutoMapper;
using CinemaC.Models.Domain;

namespace CinemaC.Profiles
{
    public class SqlDataReaderProfile:Profile
    {
        public SqlDataReaderProfile()
        {
            CreateMap<SqlDataReader, Movie>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src["Id"]))
                .ForMember(dst => dst.Title, opt => opt.MapFrom(src => src["Title"]))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src["Description"]))
                .ForMember(dst => dst.MinAge, opt => opt.MapFrom(src => src["MinAge"]))
                .ForMember(dst => dst.Duration, opt => opt.MapFrom(src => src["Duration"]))
                .ForMember(dst => dst.Rating, opt => opt.MapFrom(src => src["Rating"]))
                .ForMember(dst => dst.ImgUrl, opt => opt.MapFrom(src => src["ImgUrl"]))
                .ForMember(dst => dst.Director, opt => opt.MapFrom(src => src["Director"]))
                .ForMember(dst => dst.ReleaseDate, opt => opt.MapFrom(src => src["ReleaseDate"]))
                .ForMember(dst => dst.Genres, opt => opt.Ignore())
                .AfterMap((src, dst) =>
                {
                    var genres = (string)src["Genres"];
                    if (!string.IsNullOrWhiteSpace(genres))
                    {
                        var parsedGenres = genres.Split(',').Select(x => (Genre) Enum.Parse(typeof(Genre), x));
                        dst.Genres = parsedGenres.ToArray();
                    }
                })
                .ForAllOtherMembers(x=>x.Ignore());
        }
    }
}