using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Preview;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common.AspNetCore;
using Umbraco.Cms.Web.Common.Security;
using Umbraco.Extensions;

namespace Umbraco.Cms.Web.Common.Middleware;

/// <summary>
///     Ensures that preview pages (front-end routed) are authenticated with the back office identity appended to the
///     principal alongside any default authentication that takes place
/// </summary>
public class PreviewAuthenticationMiddleware : IMiddleware
{
    private readonly ILogger<PreviewAuthenticationMiddleware> _logger;
    private readonly IPreviewTokenGenerator _previewTokenGenerator;
    private readonly IPreviewService _previewService;


    public PreviewAuthenticationMiddleware(
        ILogger<PreviewAuthenticationMiddleware> logger,
        IPreviewTokenGenerator previewTokenGenerator,
        IPreviewService previewService)
    {
        _logger = logger;
        _previewTokenGenerator = previewTokenGenerator;
        _previewService = previewService;
    }

    /// <inheritdoc />
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        HttpRequest request = context.Request;

        // do not process if client-side request
        if (request.IsClientSideRequest())
        {
            await next(context);
            return;
        }

        try
        {
            var isPreview = request.HasPreviewCookie()
                            && !request.IsBackOfficeRequest();

            if (isPreview)
            {
                Attempt<ClaimsIdentity> backOfficeIdentityAttempt = await _previewService.TryGetPreviewClaimsIdentityAsync();

                if (backOfficeIdentityAttempt.Success)
                {
                    // Ok, we've got a real ticket, now we can add this ticket's identity to the current
                    // Principal, this means we'll have 2 identities assigned to the principal which we can
                    // use to authorize the preview and allow for a back office User.
                    context.User.AddIdentity(backOfficeIdentityAttempt.Result!);
                }
                else
                {
                    _logger.LogDebug("Could not transform previewCookie value into a claimsIdentity");
                }
            }
        }
        catch (Exception ex)
        {
            // log any errors and continue the request without preview
            _logger.LogError("Unable to perform preview authentication: {message}", ex.Message);
        }
        finally
        {
            await next(context);
        }
    }
}
