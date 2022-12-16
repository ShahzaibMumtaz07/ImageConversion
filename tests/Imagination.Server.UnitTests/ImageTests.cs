using System;
using System.IO;
using Imagination.Server.Processor;
using System.Threading;
using Xunit;

namespace Imagination.Server.UnitTests;


public class ImageTests
{    
    private const string _path = "../../../../../resources";

    private readonly FileStreamOptions _options = new()
    {
        Mode = FileMode.Open,
        Access = FileAccess.Read,
        Options = FileOptions.Asynchronous | FileOptions.SequentialScan
    };

    [Fact]
    public async Task InvalidTest()
    {
        Exception? e = null;
        try
        {
            await new ImageConverter().ImageConversionAsync(null, CancellationToken.None);
        }
        catch (Exception exception)
        {
            
            e = exception;
        }
        Assert.IsType<ArgumentNullException>(e);
    }
    [Theory]
    [InlineData($"{_path}/big.jpg")]
    [InlineData($"{_path}/jfif.jfif")]
    [InlineData($"{_path}/jpeg.jpg")]
    [InlineData($"{_path}/png.png")]
    [InlineData($"{_path}/small.jpg")]
    [InlineData($"{_path}/small.png")]
    [InlineData($"{_path}/transparent.png")]
    [InlineData($"{_path}/invalid.png")]
    public async Task ValidTest(string filename)
    {
        await using var inputStream = new FileStream(filename, _options);
        Stream output = await new ImageConverter().ImageConversionAsync(inputStream, CancellationToken.None);
        Assert.NotNull(output);
    }
}