using ApplicationCore.Entities.Portfolio;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Web.Utilities;
using Web.Models;
using System.Collections.Generic;
using Web.Extensions.Filters;
using ApplicationCore;
using Infrastructure.Services;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Web.Controllers
{
    //[Authorize]
    public class PortfolioController : ApiControllerBase
    {
        private readonly IEfRepository<Project> _projectRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PortfolioController> _logger;
        private readonly ICommonHelpers _commonHelpers;
        private readonly IFormFileProcessor _formFileProcessor;
        //private readonly IdentityClaimsProfileService _identityService;

        public PortfolioController(IEfRepository<Project> projectRepository
            , ILogger<PortfolioController> logger
            , IMapper mapper
            , ICommonHelpers commonHelpers
            , IFormFileProcessor formFileProcessor
            //, IdentityClaimsProfileService identityService
            )
        {
            _projectRepository = projectRepository;
            _logger = logger;
            _mapper = mapper;
            _commonHelpers = commonHelpers;
            _formFileProcessor = formFileProcessor;
            //_identityService = identityService;
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
        [ValidateMultipleFormData]
        public async Task<IActionResult> SaveAsync([FromForm]ProjectBindingModel model)
        {
            Project entity;
            //var userId = await _identityService.GetUserIdAsync(User.Identity.Name);

            if (model.IsModified)
            {
                entity = await _projectRepository.GetByIdAsync(model.Id);
                entity.Title = model.Title;
                entity.Description = model.Description;
                entity.Languages = model.Languages;
                entity.Libraries = model.Libraries;
                entity.Tools = model.Tools;
                entity.StartedOn = model.StartedOn;
                entity.Status = model.Status;
                entity.CompletedOn = model.CompletedOn;
                //entity.UpdatedBy = userId;
                entity.UpdatedOn = DateTime.Now;
                entity.EntityState = EntityState.Modified;

                foreach (var item in entity.ProjectClients)
                    item.EntityState = EntityState.Unchanged;

                foreach (var item in entity.ProjectImages)
                    item.EntityState = EntityState.Unchanged;

                ProjectClient projectClientEntity;
                foreach(var item in model.ProjectClients)
                {
                    projectClientEntity = entity.ProjectClients.FirstOrDefault(x => x.Name == item.Name && x.Email == item.Email && x.Organization == item.Organization);
                    if(projectClientEntity == null)
                    {
                        projectClientEntity = new ProjectClient
                        {
                            Name = item.Name,
                            Email = item.Email,
                            Organization = item.Organization,
                            Description = item.Description
                        };

                        entity.ProjectClients.Add(projectClientEntity);
                    }
                    else
                    {
                        projectClientEntity.Name = item.Name;
                        projectClientEntity.Email = item.Email;
                        projectClientEntity.Organization = item.Organization;
                        projectClientEntity.Description = item.Description;
                        projectClientEntity.EntityState = EntityState.Modified;
                    }
                }

                await _projectRepository.UpdateAsync(entity);
            }
            else
            {
                entity = _mapper.Map<Project>(model);
                //entity.AddedBy = userId;

                var directory = Path.Combine(FileSavePaths.UploadPath, Path.GetRandomFileName());
                Directory.CreateDirectory(directory);

                ProjectImage projectImageEntity;
                string trustedFileNameForFileStorage;
                foreach (var item in model.ProjectImages)
                {
                    trustedFileNameForFileStorage = Path.GetRandomFileName();

                    projectImageEntity = new ProjectImage
                    {
                        Caption = WebUtility.HtmlEncode(item.Image.FileName),
                        Path = Path.Combine(directory, trustedFileNameForFileStorage),
                        IsPrimary = item.IsPrimary
                    };

                    var streamedFileContent = await _formFileProcessor.ProcessFormFile<ProjectImageBindingModel>(item.Image, ModelState, PermittedFileExtensions.ImageExtensions);
                    using var targetStream = System.IO.File.Create(projectImageEntity.Path);
                    await targetStream.WriteAsync(streamedFileContent);

                    _logger.LogInformation($"Uploaded file '{projectImageEntity.Caption}' saved to '{projectImageEntity.Path}' as {trustedFileNameForFileStorage}");

                    entity.ProjectImages.Add(projectImageEntity);
                }

                await _projectRepository.AddAsync(entity);
            }

            return Ok();
        }

        [HttpPost]
        [ValidateMultipleFormData]
        [Route("projectimage")]
        public async Task<IActionResult> PostProjectFile([FromForm]ProjectImageBindingModel model)
        {
            var trustedFileNameForFileStorage = Guid.NewGuid().ToString();
            var directory = $@"{FileSavePaths.UploadPath}/{Guid.NewGuid().ToString()}";
            Directory.CreateDirectory(directory);

            var projectImageEntity = new ProjectImage
            {
                Caption = WebUtility.HtmlEncode(model.Image.FileName),
                Path = $@"{directory}/{trustedFileNameForFileStorage}{Path.GetExtension(model.Image.FileName)}",
                IsPrimary = model.IsPrimary
            };

            var streamedFileContent = await _formFileProcessor.ProcessFormFile<ProjectImageBindingModel>(model.Image, ModelState, PermittedFileExtensions.ImageExtensions);
            using var targetStream = System.IO.File.Create(projectImageEntity.Path);
            await targetStream.WriteAsync(streamedFileContent);

            _logger.LogInformation($"Uploaded file '{projectImageEntity.Caption}' saved to '{projectImageEntity.Path}' as {trustedFileNameForFileStorage}");

            return Ok();
        }
    }
}