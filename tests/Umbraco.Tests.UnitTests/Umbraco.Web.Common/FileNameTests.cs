// Copyright (c) Umbraco.
// See LICENSE for more details.

using AutoFixture.NUnit3;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Moq;
using NUnit.Framework;
using Umbraco.Cms.Api.Management.Controllers.Security;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Hosting;
using Umbraco.Cms.Tests.UnitTests.AutoFixture;

namespace Umbraco.Cms.Tests.UnitTests.Umbraco.Web.Common;

[TestFixture]
internal class FileNameTests
{
    private string GetViewName(ViewResult viewResult, string separator = "/")
    {
        var sections = viewResult.ViewName.Split(separator);
        return sections[^1];
    }

    private IEnumerable<string> GetUiFiles(IEnumerable<string> pathFromNetCore)
    {
        var root = TestContext.CurrentContext.TestDirectory.Split("tests")[0];
        var pathToFiles = Path.Combine(root, "src", "Umbraco.Cms.StaticAssets");
        foreach (var pathSection in pathFromNetCore)
        {
            pathToFiles = Path.Combine(pathToFiles, pathSection);
        }

        return new DirectoryInfo(pathToFiles).GetFiles().Select(f => f.Name).ToArray();
    }

    [Test]
    [AutoMoqData]
    public void BackOfficeDefaultExists(BackOfficeDefaultController sut)
    {
        var viewResult = sut.DefaultView();
        var fileName = GetViewName(viewResult);
        var views = GetUiFiles(new[] { "umbraco", "UmbracoBackOffice" });

        Assert.True(views.Contains(fileName), $"Expected {fileName} to exist, but it didn't");
    }

    [Test]
    public void LanguageFilesAreLowerCase()
    {
        var languageProvider = new EmbeddedFileProvider(
            typeof(IAssemblyProvider).Assembly,
            "Umbraco.Cms.Core.EmbeddedResources.Lang");
        var files = languageProvider.GetDirectoryContents(string.Empty)
            .Where(x => !x.IsDirectory && x.Name.EndsWith(".xml"))
            .Select(x => x.Name);

        foreach (var fileName in files)
        {
            Assert.AreEqual(
                fileName.ToLower(),
                fileName,
                $"Language files must be all lowercase but {fileName} is not lowercase.");
        }
    }
}
