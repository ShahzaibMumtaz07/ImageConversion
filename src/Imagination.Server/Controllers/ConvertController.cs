using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Imagination.Server.Processor;
namespace Imagination.Controllers
{
    [ApiController]
    public class ConvertController: ControllerBase
    {
        private readonly ILogger<ConvertController> _logger;
        private readonly ImageProcessor _imageProcessor;
        
        public ConvertController(ILogger<ConvertController> logger, ImageProcessor imageProcessor)
        {
            _logger = logger;
            _imageProcessor = imageProcessor;
        }
        [HttpPost("/convert")]
        public async Task<FileStreamResult> ImageConvert(CancellationToken token)
        {
            _logger.LogInformation("Recieved image conversion request");

            Stream outStream = await _imageProcessor.ImageConversionAsync(Request.Body, token).ConfigureAwait(false);
            _logger.LogInformation("Response Sent");

            return new FileStreamResult(outStream, "image/jpeg");
        }
    }
    
}