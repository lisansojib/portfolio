using ApplicationCore.Entities.Portfolio;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Web.Utilities;
using Web.Models;
using System.Collections.Generic;

namespace Web.Controllers
{
    public class PortfolioController : ApiControllerBase
    {
        private readonly IEfRepository<Project> _projectRepository;
        private readonly IMapper _mapper;
        private readonly long _fileSizeLimit;
        private readonly ILogger<PortfolioController> _logger;
        private readonly ICommonHelpers _commonHelpers;

        private readonly string[] _permittedExtensions = { ".txt" };
        private readonly string _targetFilePath;

        // Get the default form options so that we can use them to set the default 
        // limits for request body data.
        private static readonly FormOptions _defaultFormOptions = new FormOptions();

        public PortfolioController(IEfRepository<Project> projectRepository
            , ILogger<PortfolioController> logger
            , IMapper mapper
            , IConfiguration config
            , ICommonHelpers commonHelpers)
        {
            _projectRepository = projectRepository;
            _logger = logger;
            _mapper = mapper;
            _commonHelpers = commonHelpers;

            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");

            // To save physical files to a path provided by configuration:
            _targetFilePath = config.GetValue<string>("StoredFilesPath");
        }

        [HttpGet]
        public IActionResult Get(int offset = 0, int limit = 10, string filter = null, string sort = null, string order = null)
        {
            var filterBy = _commonHelpers.GetFilterByModel(filter);
            var records = _projectRepository.ListAll(offset, limit, filterBy, sort, order, out int count);
            var data = _mapper.Map<List<ProjectViewModel>>(records);
            var response = new TableResponseModel(count, data);

            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _projectRepository.GetByIdAsync(id);
            var model = _mapper.Map<ProjectViewModel>(entity);

            return Ok(model);
        }

        [HttpPost]
        //[DisableFormValueModelBinding]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> PostAsync()
        {
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                ModelState.AddModelError("File",
                    $"The request couldn't be processed (Error 1).");
                // Log error

                return BadRequest(ModelState);
            }

            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(Request.ContentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);
            var section = await reader.ReadNextSectionAsync();

            while (section != null)
            {
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition);

                if (hasContentDispositionHeader)
                {
                    // This check assumes that there's a file
                    // present without form data. If form data
                    // is present, this method immediately fails
                    // and returns the model error.
                    if (!MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                    {
                        return BadRequest($"The request couldn't be processed (Error 2).");
                    }
                    else
                    {
                        // Don't trust the file name sent by the client. To display
                        // the file name, HTML-encode the value.
                        var trustedFileNameForDisplay = WebUtility.HtmlEncode(contentDisposition.FileName.Value);
                        var trustedFileNameForFileStorage = Path.GetRandomFileName();

                        // **WARNING!**
                        // In the following example, the file is saved without
                        // scanning the file's contents. In most production
                        // scenarios, an anti-virus/anti-malware scanner API
                        // is used on the file before making the file available
                        // for download or for use by other systems. 
                        // For more information, see the topic that accompanies 
                        // this sample.

                        var streamedFileContent = await FileHelpers.ProcessStreamedFile(section, contentDisposition, ModelState, _permittedExtensions, _fileSizeLimit);

                        using var targetStream = System.IO.File.Create(Path.Combine(_targetFilePath, trustedFileNameForFileStorage));
                        await targetStream.WriteAsync(streamedFileContent);

                        _logger.LogInformation("Uploaded file '{TrustedFileNameForDisplay}' saved to '{TargetFilePath}' as {TrustedFileNameForFileStorage}",
                            trustedFileNameForDisplay, _targetFilePath,trustedFileNameForFileStorage);
                    }
                }

                // Drain any remaining section body that hasn't been consumed and
                // read the headers for the next section.
                section = await reader.ReadNextSectionAsync();
            }

            return Created(nameof(PortfolioController), null);
        }

        [Route("projectimage")]
        [HttpPost]
        //[DisableFormValueModelBinding]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadProjectImagesAsync()
        {
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                ModelState.AddModelError("File",
                    $"The request couldn't be processed (Error 1).");
                // Log error

                return BadRequest(ModelState);
            }

            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(Request.ContentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);
            var section = await reader.ReadNextSectionAsync();

            while (section != null)
            {
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition);

                if (hasContentDispositionHeader)
                {
                    // This check assumes that there's a file
                    // present without form data. If form data
                    // is present, this method immediately fails
                    // and returns the model error.
                    if (!MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                    {
                        return BadRequest($"The request couldn't be processed (Error 2).");
                    }
                    else
                    {
                        // Don't trust the file name sent by the client. To display
                        // the file name, HTML-encode the value.
                        var trustedFileNameForDisplay = WebUtility.HtmlEncode(contentDisposition.FileName.Value);
                        var trustedFileNameForFileStorage = Path.GetRandomFileName();

                        // **WARNING!**
                        // In the following example, the file is saved without
                        // scanning the file's contents. In most production
                        // scenarios, an anti-virus/anti-malware scanner API
                        // is used on the file before making the file available
                        // for download or for use by other systems. 
                        // For more information, see the topic that accompanies 
                        // this sample.

                        var streamedFileContent = await FileHelpers.ProcessStreamedFile(section, contentDisposition, ModelState, _permittedExtensions, _fileSizeLimit);

                        using var targetStream = System.IO.File.Create(Path.Combine(_targetFilePath, trustedFileNameForFileStorage));
                        await targetStream.WriteAsync(streamedFileContent);

                        _logger.LogInformation("Uploaded file '{TrustedFileNameForDisplay}' saved to '{TargetFilePath}' as {TrustedFileNameForFileStorage}",
                            trustedFileNameForDisplay, _targetFilePath, trustedFileNameForFileStorage);
                    }
                }

                // Drain any remaining section body that hasn't been consumed and
                // read the headers for the next section.
                section = await reader.ReadNextSectionAsync();
            }

            return Created(nameof(PortfolioController), null);
        }
    }
}