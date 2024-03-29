using FluentAssertions;
using FluentAssertions.Execution;
using SkiaSharp;
using TeethInc.Chantry.Core.Extensions;

namespace Chantry.Tests
{
    [TestClass]
    public class SizeExtensionsTests
    {
        [TestMethod]
        [DataRow(215, 320, 128, 128)]
        [DataRow(214, 318, 128, 128)]
        [DataRow(214, 320, 128, 128)]
        public void GetSizeToFill_SourceIsPortrait_WidthShouldBeSameAsTargetWidth(int sourceWidth, int sourceHeight, int targetWidth, int targetHeight)
        {
            // arrange.
            SKSizeI sourceSize = new SKSizeI(sourceWidth, sourceHeight);
            SKSizeI targetSize = new SKSizeI(targetWidth, targetHeight);

            // act.
            var actual = sourceSize.GetSizeToFill(targetSize);

            // assert.
            actual.Width.Should().Be(targetWidth);
        }

        [TestMethod]
        [DataRow(320, 215, 128, 128)]
        [DataRow(318, 214, 128, 128)]
        [DataRow(320, 214, 128, 128)]
        public void GetSizeToFill_SourceIsLandscape_HeightShouldBeSameAsTargetHeight(int sourceWidth, int sourceHeight, int targetWidth, int targetHeight)
        {
            // arrange.
            SKSizeI sourceSize = new SKSizeI(sourceWidth, sourceHeight);
            SKSizeI targetSize = new SKSizeI(targetWidth, targetHeight);

            // act.
            var actual = sourceSize.GetSizeToFill(targetSize);

            // assert.
            actual.Height.Should().Be(targetHeight);
        }

        [TestMethod]
        [DataRow(725, 1080, 320, 320)]
        public void GetSizeToFit_SourceIsPortrait_HeightShouldBeSameAsTargetHeight(int sourceWidth, int sourceHeight, int targetWidth, int targetHeight)
        {
            // arrange.
            SKSizeI sourceSize = new SKSizeI(sourceWidth, sourceHeight);
            SKSizeI targetSize = new SKSizeI(targetWidth, targetHeight);

            // act.
            var actual = sourceSize.GetSizeToFit(targetSize);

            // assert.
            actual.Height.Should().Be(targetHeight);
        }

        [TestMethod]
        [DataRow(32, 32)]
        [DataRow(32, 64)]
        [DataRow(32, 96)]
        [DataRow(32, 128)]
        [DataRow(64, 64)]
        [DataRow(64, 96)]
        [DataRow(64, 128)]
        [DataRow(96, 96)]
        [DataRow(96, 128)]
        [DataRow(128, 128)]
        public void GetSizeToFill_ScaledSizeIsLargerThanTargetSizeForAllSizes(int targetWidth, int targetHeight)
        {
            SKSizeI targetSize = new SKSizeI(targetWidth, targetHeight);

            using (new AssertionScope())
            {
                for (int sourceWidth = 10; sourceWidth <= 1500; sourceWidth++)
                {
                    SKSizeI sourceSize = new SKSizeI(sourceWidth, 640);

                    var actual = sourceSize.GetSizeToFill(targetSize);

                    actual.Height.Should().BeGreaterThanOrEqualTo(targetHeight);
                    actual.Width.Should().BeGreaterThanOrEqualTo(targetWidth);

                    if (!(actual.Width == targetWidth || actual.Height == targetHeight))
                        Assert.Fail();
                }
            }
        }

        [TestMethod]
        [DataRow(32, 32)]
        [DataRow(32, 64)]
        [DataRow(32, 96)]
        [DataRow(32, 128)]
        [DataRow(64, 64)]
        [DataRow(64, 96)]
        [DataRow(64, 128)]
        [DataRow(96, 96)]
        [DataRow(96, 128)]
        [DataRow(128, 128)]
        public void GetSizeToFit_ScaledSizeIsSmallerThanTargetSizeForAllSizes(int targetWidth, int targetHeight)
        {
            SKSizeI targetSize = new SKSizeI(targetWidth, targetHeight);

            using (new AssertionScope())
            {
                for (int sourceWidth = 10; sourceWidth <= 1500; sourceWidth++)
                {
                    SKSizeI sourceSize = new SKSizeI(sourceWidth, 640);

                    var actual = sourceSize.GetSizeToFit(targetSize);

                    actual.Height.Should().BeLessThanOrEqualTo(targetHeight);
                    actual.Width.Should().BeLessThanOrEqualTo(targetWidth);

                    if (!(actual.Width == targetWidth || actual.Height == targetHeight))
                        Assert.Fail();
                }
            }
        }
    }
}