using System.Diagnostics.CodeAnalysis;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Manifest;

namespace Umbraco.Cms.Core.PropertyEditors;

public class PropertyEditorCollection : BuilderCollectionBase<IDataEditor>
{
    public PropertyEditorCollection(DataEditorCollection dataEditors)
        : base(() => dataEditors)
    {
    }

    // note: virtual so it can be mocked
    public virtual IDataEditor? this[string? alias]
        => this.SingleOrDefault(x => x.Alias == alias);

    public virtual bool TryGet(string? alias, [MaybeNullWhen(false)] out IDataEditor editor)
    {
        editor = this.FirstOrDefault(x => x.Alias == alias);
        return editor != null;
    }
}
