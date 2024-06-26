using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Umbraco.Cms.Core.PropertyEditors.ValueConverters;

[DefaultPropertyValueConverter]
public class DatePickerValueConverter : PropertyValueConverterBase
{
    public override bool IsConverter(IPublishedPropertyType propertyType)
        => propertyType.EditorAlias.InvariantEquals(Constants.PropertyEditors.Aliases.DateTime);

    public override Type GetPropertyValueType(IPublishedPropertyType propertyType)
        => typeof(DateTime);

    public override PropertyCacheLevel GetPropertyCacheLevel(IPublishedPropertyType propertyType)
        => PropertyCacheLevel.Element;

    public override object ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object? source, bool preview)
        => ParseDateTimeValue(source);

    internal static DateTime ParseDateTimeValue(object? source)
    {
        if (source == null)
        {
            return DateTime.MinValue;
        }

        // in XML a DateTime is: string - format "yyyy-MM-ddTHH:mm:ss"
        // Actually, not always sometimes it is formatted in UTC style with 'Z' suffixed on the end but that is due to this bug:
        // http://issues.umbraco.org/issue/U4-4145, http://issues.umbraco.org/issue/U4-3894
        // We should just be using TryConvertTo instead.
        if (source is string sourceString)
        {
            Attempt<DateTime> attempt = sourceString.TryConvertTo<DateTime>();
            return attempt.Success == false ? DateTime.MinValue : attempt.Result;
        }

        // in the database a DateTime is: DateTime
        // default value is: DateTime.MinValue
        return source is DateTime dateTimeValue ? dateTimeValue : DateTime.MinValue;
    }
}
