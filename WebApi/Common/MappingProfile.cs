using AutoMapper;
using WebApi.Application.BookOperations.CreateBook;
using WebApi.Application.BookOperations.GetBooks;
using WebApi.Application.BookOperations.UpdateBook;
using WebApi.Entities;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
            /*CreateMap<UpdateBookViewModel, Book>().ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId))
                                                  .ForMember(dest => dest.PageCount, opt => opt.MapFrom(src => src.PageCount))
                                                  .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title)); */
            CreateMap<UpdateBookCommand, Book>();

        }
    }
}