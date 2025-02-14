using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Resyaku.Application.Features.ServiceAreas.Queries.GetAllServiceAreas;
using Resyaku.Application.Features.Tables.Commands.CreateTable;
using Resyaku.Application.Features.Tables.Commands.UpdateTable;
using Resyaku.Application.Features.Tables.Queries.GetAllTables;
using Resyaku.Application.Features.Tables.Queries.GetAvailableTables;
using Resyaku.Application.Features.Tables.Queries.GetTableById;
using Resyaku.Domain.Primitives;
using Resyaku.Web.Extensions;
using Resyaku.Web.Mappers;
using Resyaku.Web.ViewModels.Tables;

namespace Resyaku.Web.Controllers
{
    [Route("tables")]
    public sealed class TableController(
        ISender sender,
        IValidator<CreateTableCommand> createTableValidator,
        IValidator<UpdateTableViewModel> updateTableValidator,
        IValidator<GetAvailableTablesQueryParams> availableTablesQueryValidator)
        : Controller
    {
        public async Task<IActionResult> Index(
            [FromQuery] GetAllTablesQueryParameters queryParameters,
            CancellationToken cancellationToken)
        {
            var pagedTables = await sender.Send(new GetAllTablesQuery(queryParameters), cancellationToken);
            var tablesViewModel = new GetAllTablesViewModel(queryParameters, pagedTables);

            var servicesAreas = await sender.Send(new GetAllServiceAreasQuery(), cancellationToken);
            ViewBag.ServicesAreas = servicesAreas;

            return View(tablesViewModel);
        }

        [Route("create")]
        public async Task<IActionResult> CreateTable(CancellationToken cancellationToken)
        {
            var servicesAreas = await sender.Send(new GetAllServiceAreasQuery(), cancellationToken);
            ViewBag.ServicesAreas = servicesAreas;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<IActionResult> CreateTable(
            CreateTableCommand command,
            CancellationToken cancellationToken)
        {
            var validationResult = await createTableValidator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);

                var servicesAreas = await sender.Send(new GetAllServiceAreasQuery(), cancellationToken);
                ViewBag.ServicesAreas = servicesAreas;

                return View(command);
            }

            var creationResult = await sender.Send(command, cancellationToken);

            if (creationResult.IsFailure)
            {
                ViewBag.TableCreationError = creationResult.Error.Description;
                ViewBag.ServicesAreas = await sender.Send(new GetAllServiceAreasQuery(), cancellationToken);

                return View(command);
            }

            TempData["Table.Created"] = "Mesa registrada con éxito.";

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("{tableId}/update")]
        public async Task<IActionResult> UpdateTable(int tableId, CancellationToken cancellationToken)
        {
            var query = new GetTableByIdQuery(tableId);
            Result<GetTableByIdDto> queryResult = await sender.Send(query, cancellationToken);

            if (queryResult.IsFailure)
            {
                return NotFound();
            }

            UpdateTableViewModel viewModel = queryResult.Value.ToUpdateTableViewModel();
            ViewBag.ServicesAreas = await sender.Send(new GetAllServiceAreasQuery(), cancellationToken);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{tableId}/update")]
        public async Task<IActionResult> UpdateTable(
            int tableId,
            [FromForm] UpdateTableViewModel viewModel,
            CancellationToken cancellationToken)
        {
            var validationResult = await updateTableValidator.ValidateAsync(viewModel, cancellationToken);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                ViewBag.ServicesAreas = await sender.Send(new GetAllServiceAreasQuery(), cancellationToken);

                return View(viewModel);
            }

            UpdateTableCommand command = viewModel.ToUpdateTableCommand();
            var updateResult = await sender.Send(command, cancellationToken);

            if (updateResult.IsFailure)
            {
                ViewBag.TableUpdateError = updateResult.Error.Description;
                ViewBag.ServicesAreas = await sender.Send(new GetAllServiceAreasQuery(), cancellationToken);

                return View(command);
            }

            TempData["Table.Updated"] = "Mesa actualizada correctamente.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableTables(
            [FromQuery] GetAvailableTablesQueryParams queryParams)
        {
            var validationResult = await availableTablesQueryValidator.ValidateAsync(queryParams);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }
            var query = new GetAvailableTablesQuery(
                queryParams.BookingDate,
                queryParams.BookingTime,
                queryParams.Duration);
            var resp = await sender.Send(query);
            return Ok(resp);
        }
    }
}
