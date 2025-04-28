using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Resyaku.Application.Features.ServiceAreas.Queries.GetAllServiceAreas;
using Resyaku.Application.Features.Tables.Commands.UpdateTable;
using Resyaku.Application.Features.Tables.Queries.GetAllTables;
using Resyaku.Application.Features.Tables.Queries.GetAvailableTables;
using Resyaku.Application.Features.Tables.Queries.GetTableById;
using Resyaku.Domain.Primitives;
using Resyaku.Web.Extensions;
using Resyaku.Web.Mapper;
using Resyaku.Web.ViewModels.Tables;

namespace Resyaku.Web.Controllers
{
    [Route("tables")]
    public sealed class TableController(
        ISender sender,
        IValidator<CreateTableViewModel> createTableValidator,
        IValidator<UpdateTableViewModel> updateTableValidator,
        IValidator<GetAvailableTablesQueryParams> availableTablesQueryValidator)
        : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(
            [FromQuery] GetAllTablesQueryParams queryParameters,
            CancellationToken cancellationToken)
        {
            var pagedTables = await sender.Send(new GetAllTablesQuery(queryParameters), cancellationToken);
            var servicesAreas = await sender.Send(new GetAllServiceAreasQuery(), cancellationToken);
            
            var viewModel = new GetAllTablesViewModel(queryParameters, pagedTables, servicesAreas);

            return View(viewModel);
        }

        [HttpGet("create")]
        public async Task<IActionResult> CreateTable(CancellationToken cancellationToken)
        {
            var servicesAreas = await sender.Send(new GetAllServiceAreasQuery(), cancellationToken);
            var viewModel = new CreateTableViewModel(servicesAreas);

            return View(viewModel);
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTable(
            CreateTableViewModel viewModel,
            CancellationToken cancellationToken)
        {
            var validationResult = await createTableValidator.ValidateAsync(viewModel, cancellationToken);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);

                var serviceAreas = await sender.Send(new GetAllServiceAreasQuery(), cancellationToken);
                viewModel.AvailableServiceAreas = serviceAreas;

                return View(viewModel);
            }

            var creationResult = await sender.Send(viewModel.ToCreateTableCommand(), cancellationToken);

            if (creationResult.IsFailure)
            {
                var serviceAreas = await sender.Send(new GetAllServiceAreasQuery(), cancellationToken);
                
                ViewBag.TableCreationError = creationResult.Error.Description;
                viewModel.AvailableServiceAreas = serviceAreas;

                return View(viewModel);
            }

            TempData["Table.Created"] = "Mesa registrada correctamente.";

            return RedirectToAction("Index");
        }

        [HttpGet("{tableId}/update")]
        public async Task<IActionResult> UpdateTable(int tableId, CancellationToken cancellationToken)
        {
            var tableResult = await sender.Send(new GetTableByIdQuery(tableId), cancellationToken);

            if (tableResult.IsFailure)
            {
                return NotFound();
            }

            var serviceAreas = await sender.Send(new GetAllServiceAreasQuery(), cancellationToken);
            var viewModel = new UpdateTableViewModel(tableResult.Value, serviceAreas);

            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost("{tableId}/update")]
        public async Task<IActionResult> UpdateTable(
            int tableId,
            [FromForm] UpdateTableViewModel viewModel,
            CancellationToken cancellationToken)
        {
            var validationResult = await updateTableValidator.ValidateAsync(viewModel, cancellationToken);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);

                viewModel.AvailableServiceAreas = await sender.Send(
                    new GetAllServiceAreasQuery(), 
                    cancellationToken);

                return View(viewModel);
            }

            Result updateResult = await sender.Send(viewModel.ToUpdateTableCommand(tableId), cancellationToken);

            if (updateResult.IsFailure)
            {
                ViewBag.TableUpdateError = updateResult.Error.Description;
                
                viewModel.AvailableServiceAreas = await sender.Send(
                    new GetAllServiceAreasQuery(), 
                    cancellationToken);

                return View(viewModel);
            }

            TempData["Table.Updated"] = "Mesa actualizada correctamente.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableTables(
            [FromQuery] GetAvailableTablesQueryParams queryParams,
            CancellationToken cancellationToken)
        {
            var validationResult = await availableTablesQueryValidator.ValidateAsync(queryParams, cancellationToken);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }

            var query = new GetAvailableTablesQuery(
                queryParams.BookingDate,
                queryParams.BookingTime,
                queryParams.Duration);

            var availableTables = await sender.Send(query, cancellationToken);
            
            return Ok(availableTables);
        }
    }
}
