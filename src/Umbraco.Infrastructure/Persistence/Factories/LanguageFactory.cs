using System.Globalization;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Infrastructure.Persistence.Dtos;

namespace Umbraco.Cms.Infrastructure.Persistence.Factories;

internal static class LanguageFactory
{
    public static ILanguage BuildEntity(LanguageDto dto, string? fallbackIsoCode)
    {
        ArgumentNullException.ThrowIfNull(dto);
        if (dto.IsoCode is null)
        {
            throw new InvalidOperationException("Language ISO code can't be null.");
        }

        dto.CultureName ??= CultureInfo.GetCultureInfo(dto.IsoCode).EnglishName;

        var lang = new Language(dto.IsoCode, dto.CultureName)
        {
            Id = dto.Id,
            IsDefault = dto.IsDefault,
            IsMandatory = dto.IsMandatory,
            FallbackIsoCode = fallbackIsoCode
        };

        // Reset dirty initial properties
        lang.ResetDirtyProperties(false);

        return lang;
    }

    public static LanguageDto BuildDto(ILanguage entity, int? fallbackLanguageId)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var dto = new LanguageDto
        {
            IsoCode = entity.IsoCode,
            CultureName = entity.CultureName,
            IsDefault = entity.IsDefault,
            IsMandatory = entity.IsMandatory,
            FallbackLanguageId = fallbackLanguageId
        };

        if (entity.HasIdentity)
        {
            dto.Id = (short)entity.Id;
        }

        return dto;
    }
}
