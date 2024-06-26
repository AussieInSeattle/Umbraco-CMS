﻿namespace Umbraco.Cms.Infrastructure.Security;

public interface IBackOfficeApplicationManager
{
    Task EnsureBackOfficeApplicationAsync(Uri backOfficeUrl, CancellationToken cancellationToken = default);
}
