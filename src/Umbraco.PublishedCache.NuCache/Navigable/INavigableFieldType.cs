namespace Umbraco.Cms.Infrastructure.PublishedCache.Navigable;

/// <summary>
///     Represents the type of a field of a content that can be navigated via XPath.
/// </summary>
/// <remarks>A field can be an attribute or a property.</remarks>
[Obsolete("The current implementation of XPath is suboptimal and will be removed entirely in a future version. Scheduled for removal in v15. Still needed for NuCache")]
internal interface INavigableFieldType
{
    /// <summary>
    ///     Gets the name of the field type.
    /// </summary>
    string Name { get; }

    /// <summary>
    ///     Gets a method to convert the field value to a string.
    /// </summary>
    /// <remarks>
    ///     This is for built-in properties, ie attributes. User-defined properties have their
    ///     own way to convert their value for XPath.
    /// </remarks>
    Func<object, string>? XmlStringConverter { get; }
}
