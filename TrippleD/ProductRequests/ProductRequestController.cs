using System;
using Microsoft.AspNetCore.Mvc;
using TrippleD.ProductRequests.Dtos;
using TrippleD.Sales.Application.ProductRequests;
using TrippleD.SharedKernel;

namespace TrippleD.ProductRequests
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
            Guid customerId = Constants.CustomerIds.Customer1;
            //productRequestService.RequestProduct(productRequestDto.ProductId, customerId, productRequestDto.StartDate,
            //    productRequestDto.EndDate);

            return Ok();
        }

        [HttpPost("{requestId}/cancel")]
        public IActionResult PostRequestApprove(Guid requestId)
        {
            productRequestService.CancelRequest(requestId);

            return Ok();
        }

        [HttpPost("{requestId}/requestOffer")]
        public IActionResult PostRequestOffer(Guid requestId, [FromBody] RequestOfferDto requestOfferDto)
        {
            productRequestService.MakeRequestOffer(requestId, requestOfferDto.Price);

            return Ok();
        }
    }
}