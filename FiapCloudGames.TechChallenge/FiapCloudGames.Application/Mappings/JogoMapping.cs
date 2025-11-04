using AutoMapper;
using FiapCloudGames.Application.Dtos;
using FiapCloudGames.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Application.Mappings;

[ExcludeFromCodeCoverage]
public sealed class JogoMapping : Profile
{
    public JogoMapping()
        => CreateMap<Jogo, JogoDto>()
            .ReverseMap();
}