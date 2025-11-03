using AutoMapper;
using FiapCloudGames.Application.Dtos;
using FiapCloudGames.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Application.Mappings;

[ExcludeFromCodeCoverage]
public sealed class UsuarioMapping : Profile
{
    public UsuarioMapping()
        => CreateMap<Usuario, UsuarioDto>().ReverseMap();
}