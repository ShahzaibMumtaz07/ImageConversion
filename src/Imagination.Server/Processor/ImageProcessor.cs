using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Imagination.Server.Processor
{
    public interface ImageProcessor
    {
        Task<Stream> ImageConversionAsync(Stream inputStream, CancellationToken cancelToken);
    }
}