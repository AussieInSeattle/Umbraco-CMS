namespace Umbraco.Cms.Core.Deploy;

/// <summary>
/// Represents a deployment context.
/// </summary>
public interface IDeployContext
{
    /// <summary>
    /// Gets the unique identifier of the deployment.
    /// </summary>
    /// <value>
    /// The session identifier.
    /// </value>
    Guid SessionId { get; }

    /// <summary>
    /// Gets the file source.
    /// </summary>
    /// <value>
    /// The file source.
    /// </value>
    /// <remarks>
    /// The file source is used to obtain files from the source environment.
    /// </remarks>
    IFileSource FileSource { get; }

    /// <summary>
    /// Gets items.
    /// </summary>
    /// <value>
    /// The items.
    /// </value>
    IDictionary<string, object> Items { get; }

    /// <summary>
    /// Gets the next number in a numerical sequence.
    /// </summary>
    /// <returns>
    /// The next sequence number.
    /// </returns>
    /// <remarks>
    /// Can be used to uniquely number things during a deployment.
    /// </remarks>
    int NextSeq();

    /// <summary>
    /// Gets item.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="key">The key of the item.</param>
    /// <returns>
    /// The item with the specified key and type, if any, else null.
    /// </returns>
    T? Item<T>(string key)
        where T : class;
}
