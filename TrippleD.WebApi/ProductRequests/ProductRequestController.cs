using System;
using Microsoft.AspNetCore.Mvc;
using TrippleD.Sales.Application.ProductRequests;
using TrippleD.Sales.Application.ProductRequests.Dtos;

namespace TrippleD.Sales.WebApi.ProductRequests
{
    [Route("api/productRequests")]
    public class ProductRequestController : Controller
    {
        private readonly IProductRequestService productRequestService;

        public ProductRequestController(IProductRequestService productRequestService)
        {
            this.productRequestService = productRequestService;
        }

        [HttpPost]
        public IActionResult PostProductRequest([FromBody] ProductRequestDto productRequestDto)
        {
            productRequestService.RequestProduct(productRequestDto);

            return Ok();
        }

        [HttpPost("{requestId}/cancel")]
        public IActionResult PostCancelRequest(Guid requestId)
        {
            productRequestService.CancelRequest(requestId);

            return Ok();
        }

        [HttpPost("{requestId}/approve")]
        public IActionResult PostApproveRequest(Guid requestId)
        {
            productRequestService.ApproveRequest(requestId);

            return Ok();
        }

        [HttpPost("{requestId}/requestOffer")]
        public IActionResult PostRequestOffer(Guid requestId, [FromBody] RequestOfferDto requestOfferDto)
        {
            productRequestService.MakeRequestOffer(requestId, requestOfferDto);

            return Ok();
        }
    }
}