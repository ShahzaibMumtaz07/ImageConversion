using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Imagination.Server.Exceptions;
using SkiaSharp;

namespace Imagination.Server.Processor
{
    public class ImageConverter : ImageProcessor
    {
        private const string DecodeError = "Failed reading the input image";
        private const string EncodeError = "Failed converting the input image";

        private readonly MemoryStream stream_obj = new MemoryStream();


        public async Task<Stream> ImageConversionAsync(Stream stream_input, CancellationToken cancelToken)
        {
            ArgumentNullException.ThrowIfNull(stream_input);

            SKBitmap img_obj_raw;
            
            try
            {
                await stream_input.CopyToAsync(stream_obj, cancelToken);
                
                stream_obj.Position = 0;

                img_obj_raw = SKBitmap.Decode(stream_obj);
            }
            catch(ArgumentNullException)
            {
                throw new FailureException(DecodeError);
            }

            if (img_obj_raw == null)
            {
                throw new FailureException(DecodeError);
            }

            cancelToken.ThrowIfCancellationRequested();
            var outStream = new MemoryStream();
            if (!img_obj_raw.Encode(outStream, SKEncodedImageFormat.Jpeg, 80))
            {
                throw new FailureException(EncodeError);
            }

            outStream.Position = 0;

            return outStream;
        }
    }
}