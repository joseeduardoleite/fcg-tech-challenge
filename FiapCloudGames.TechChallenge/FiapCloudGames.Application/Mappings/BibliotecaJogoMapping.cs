using AutoMapper;
using FiapCloudGames.Application.Dtos;
using FiapCloudGames.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Application.Mappings;

[ExcludeFromCodeCoverage]
public class BibliotecaJogoMapping : Profile
{
    public BibliotecaJogoMapping()
        => CreateMap<BibliotecaJogo, BibliotecaJogoDto>()
            .ReverseMap();
}