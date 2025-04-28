using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Resyaku.Application.Features.ServiceAreas.Queries.GetAllServiceAreas;
using Resyaku.Application.Features.ServiceAreas.Queries.GetServiceAreaById;
using Resyaku.Domain.Primitives;
using Resyaku.Web.Extensions;
using Resyaku.Web.Mapper;
using Resyaku.Web.ViewModels.ServiceAreas;

namespace Resyaku.Web.Controllers
{
    [Route("servicearea")]
    public sealed class ServiceAreaController(
        ISender sender,
        IValidator<CreateServiceAreaViewModel> createServiceAreaValidator,
        IValidator<UpdateServiceAreaViewModel> updateServiceAreaValidator)
        : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var serviceAreas = await sender.Send(new GetAllServiceAreasQuery(), cancellationToken);

            return View(serviceAreas);
        }

        [HttpGet("create")]
        public IActionResult CreateServiceArea()
        {
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateServiceArea(
            [FromForm] CreateServiceAreaViewModel viewModel,
            CancellationToken cancellationToken)
        {
            var validationResult = await createServiceAreaValidator.ValidateAsync(viewModel, cancellationToken);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return View(viewModel);
            }

            Result creationResult = await sender.Send(viewModel.ToCreateServiceAreaCommand(), cancellationToken);

            if (creationResult.IsFailure)
            {
                ViewBag.ServiceAreaCreationError = creationResult.Error.Description;
                return View(viewModel);
            }

            TempData["ServiceArea.Created"] = "Área de servicio creado correctamente.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{serviceAreaId}/update")]
        public async Task<IActionResult> UpdateServiceArea(int serviceAreaId, CancellationToken cancellationToken)
        {
            var serviceAreaResult = await sender.Send(new GetServiceAreaByIdQuery(serviceAreaId), cancellationToken);

            if (serviceAreaResult.IsFailure)
            {
                return NotFound();
            }

            var viewModel = new UpdateServiceAreaViewModel(serviceAreaResult.Value);

            return View(viewModel);
        }

        [HttpPost("{serviceAreaId}/update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateServiceArea(
            int serviceAreaId,
            [FromForm] UpdateServiceAreaViewModel viewModel,
            CancellationToken cancellationToken)
        {
            var validationResult = await updateServiceAreaValidator.ValidateAsync(viewModel, cancellationToken);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return View(viewModel);
            }

            Result updateResult = await sender.Send(
                viewModel.ToUpdateServiceAreaCommand(serviceAreaId), 
                cancellationToken);

            if (updateResult.IsFailure)
            {
                ViewBag.ServiceAreaUpdateError = updateResult.Error.Description;
                return View(viewModel);
            }

            TempData["ServiceArea.Updated"] = "Área de servicio actualizado correctamente.";

            return RedirectToAction(nameof(Index));
        }
    }
}
