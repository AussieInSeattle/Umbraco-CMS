// Copyright (c) Umbraco.
// See LICENSE for more details.

using NUnit.Framework;
using Umbraco.Cms.Tests.Common.Builders;
using Umbraco.Cms.Tests.Common.Builders.Extensions;

namespace Umbraco.Cms.Tests.UnitTests.Umbraco.Tests.Common.Builders;

[TestFixture]
public class DocumentEntitySlimBuilderTests
{
    [Test]
    public void Is_Built_Correctly()
    {
        // Arrange
        const int testId = 11;
        const string testName = "Test";
        const int testCreatorId = 22;
        const int testLevel = 3;
        const string testPath = "-1,23";
        const int testParentId = 5;
        const int testSortOrder = 6;
        const bool testHasChildren = true;
        const bool testPublished = true;
        const string testContentTypeAlias = "test1";
        const string testContentTypeIcon = "icon";
        const string testContentTypeThumbnail = "thumb";
        var testKey = Guid.NewGuid();
        var testCreateDate = DateTime.Now.AddHours(-1);
        var testUpdateDate = DateTime.Now;

        var builder = new DocumentEntitySlimBuilder();

        // Act
        var item = builder
            .WithId(testId)
            .WithKey(testKey)
            .WithCreatorId(testCreatorId)
            .WithCreateDate(testCreateDate)
            .WithUpdateDate(testUpdateDate)
            .WithName(testName)
            .WithParentId(testParentId)
            .WithSortOrder(testSortOrder)
            .WithLevel(testLevel)
            .WithPath(testPath)
            .WithContentTypeAlias(testContentTypeAlias)
            .WithContentTypeIcon(testContentTypeIcon)
            .WithContentTypeThumbnail(testContentTypeThumbnail)
            .WithHasChildren(testHasChildren)
            .WithPublished(testPublished)
            .Build();

        // Assert
        Assert.AreEqual(testId, item.Id);
        Assert.AreEqual(testKey, item.Key);
        Assert.AreEqual(testName, item.Name);
        Assert.AreEqual(testCreateDate, item.CreateDate);
        Assert.AreEqual(testUpdateDate, item.UpdateDate);
        Assert.AreEqual(testCreatorId, item.CreatorId);
        Assert.AreEqual(testParentId, item.ParentId);
        Assert.AreEqual(testSortOrder, item.SortOrder);
        Assert.AreEqual(testPath, item.Path);
        Assert.AreEqual(testLevel, item.Level);
        Assert.AreEqual(testContentTypeAlias, item.ContentTypeAlias);
        Assert.AreEqual(testContentTypeIcon, item.ContentTypeIcon);
        Assert.AreEqual(testContentTypeThumbnail, item.ContentTypeThumbnail);
        Assert.AreEqual(testHasChildren, item.HasChildren);
        Assert.AreEqual(testPublished, item.Published);
    }
}
