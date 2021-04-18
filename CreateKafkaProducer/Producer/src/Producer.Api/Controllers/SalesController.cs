using Microsoft.AspNetCore.Mvc;
using Producer.Api.Model.Request;
using Producer.Api.Model.Response;
using Producer.Business.Entities;
using Producer.Business.Repositories;
using Producer.Business.Services;
using Producer.WarmUp.Abstractions;
using System;
using System.Threading.Tasks;

namespace Producer.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class SalesController : ControllerBase, IWarmUpCommand
    {
        private readonly ISaleService _service;
        private readonly IProductRepository _products;

        public SalesController(
            ISaleService service,
            IProductRepository products)
        {
            _service = service;
            _products = products;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var sample = await _service.GetSaleAsync(id.Value);

            if (sample is null)
            {
                return NotFound();
            }

            return Ok(SaleResponse.From(sample));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(SaleRequest request)
        {
            var sale = await BuildEntity(request);

            var registeredSale = await _service.RegisterSale(sale);

            var response = SaleResponse.From(registeredSale);

            return Ok(response);
        }

        async Task IWarmUpCommand.Execute()
        {
            await GetAsync(Guid.Empty);
        }

        private async Task<SaleEntity> BuildEntity(SaleRequest request)
        {
            var sale = new SaleEntity(
                request.Id,
                request.CustomerIdentity,
                request.Number,
                request.Date);

            foreach (var item in request.Items)
            {
                var product = await _products.GetById(item.ProductId);

                var saleItem = new SaleItemEntity(product, item.Quantity, item.Value);

                sale.AddItem(saleItem);
            }

            return sale;
        }
    }
}
