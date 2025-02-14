using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Resyaku.Application.Features.ServiceAreas.Commands.CreateServiceArea;
using Resyaku.Application.Features.ServiceAreas.Commands.UpdateServiceArea;
using Resyaku.Application.Features.ServiceAreas.Queries.GetAllServiceAreas;
using Resyaku.Application.Features.ServiceAreas.Queries.GetServiceAreaById;
using Resyaku.Domain.Primitives;
using Resyaku.Web.Extensions;
using Resyaku.Web.Mappers;
using Resyaku.Web.ViewModels.ServiceAreas;

namespace Resyaku.Web.Controllers
{
    [Route("servicearea")]
    public sealed class ServiceAreaController(
        ISender sender,
        IValidator<CreateServiceAreaCommand> createServiceAreaValidator,
        IValidator<UpdateServiceAreaViewModel> updateServiceAreaValidator)
        : Controller
    {
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            List<GetAllServiceAreasDto> serviceAreas = await sender.Send(new GetAllServiceAreasQuery(), cancellationToken);

            return View(serviceAreas);
        }

        [HttpGet]
        [Route("create")]
        public IActionResult CreateServiceArea()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<IActionResult> CreateServiceArea(CreateServiceAreaCommand command)
        {
            var validationResult = await createServiceAreaValidator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return View(command);
            }

            var creationResult = await sender.Send(command);

            if (creationResult.IsFailure)
            {
                ViewBag.ServiceAreaCreationError = creationResult.Error.Description;
                return View(command);
            }

            TempData["ServiceArea.Created"] = "Área de servicio creado correctamente.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("{serviceAreaId}/update")]
        public async Task<IActionResult> UpdateServiceArea(int serviceAreaId, CancellationToken cancellationToken)
        {
            var query = new GetServiceAreaByIdQuery(serviceAreaId);
            Result<GetServiceAreaByIdDto> queryResult = await sender.Send(query, cancellationToken);

            if (queryResult.IsFailure)
            {
                return NotFound();
            }

            UpdateServiceAreaViewModel viewModel = queryResult.Value.ToUpdateServiceAreaViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{serviceAreaId}/update")]
        public async Task<IActionResult> UpdateServiceArea(
            int serviceAreaId,
            [FromForm] UpdateServiceAreaViewModel viewModel,
            CancellationToken cancellationToken)
        {
            var validationResult = await updateServiceAreaValidator.ValidateAsync(viewModel);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return View(viewModel);
            }

            UpdateServiceAreaCommand command = viewModel.ToUpdateServiceAreaCommand();
            var updateResult = await sender.Send(command, cancellationToken);

            if (updateResult.IsFailure)
            {
                ViewBag.ServiceAreaUpdateError = updateResult.Error.Description;
                return View(command);
            }

            TempData["ServiceArea.Updated"] = "Área de servicio actualizado correctamente.";

            return RedirectToAction(nameof(Index));
        }
    }
}
