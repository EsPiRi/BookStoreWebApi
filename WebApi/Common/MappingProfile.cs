using AutoMapper;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Entities;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.GenreOperations.Commands.CreateGenre;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            /*CreateMap<UpdateBookViewModel, Book>().ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId))
                                                  .ForMember(dest => dest.PageCount, opt => opt.MapFrom(src => src.PageCount))
                                                  .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title)); */
            CreateMap<UpdateBookCommand, Book>();
            CreateMap<Genre,GenresViewModel>();
            CreateMap<Genre,GenreDetailViewModel>();
            CreateMap<CreateGenreModel,Genre>();

        }
    }
}