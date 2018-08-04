using GeekBurger.Ui.Application.Options;
using GeekBurger.Ui.Contracts.Request;
using GeekBurger.Ui.Domain.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GeekBurger.Ui.Api.Controllers
{
    [Route("api/")]
    public class UiController : Controller
    {
        private Guid? STORE_ID;

        private readonly IOrderService _orderService;
        private readonly IStoreCatalogService _storeCatalogService;
        private readonly IUserService _userService;
        private readonly ServiceBusOptions _options;

        public UiController(IOrderService orderService, IStoreCatalogService storeCatalogService, IUserService userService, IOptions<ServiceBusOptions> options)
        {
            this._orderService = orderService;
            this._storeCatalogService = storeCatalogService;
            this._userService = userService;
            this._options = options.Value;
            //if not ready, id is null
            STORE_ID = this._storeCatalogService.GetStoreCatalog().Result;
        }

        [HttpPost]
        [Route("face")]
        public async Task<IActionResult> PostFace([FromBody]PostFaceRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await this._userService.PostUser(request.Face);
                return Ok();
            }
            catch (Exception ex)
            {
                //log
                return StatusCode(StatusCodes.Status400BadRequest, string.Format("Error trying to post face: {0}", ex.Message));
            }
        }

        [HttpPost]
        [Route("foodRestrictions")]
        public async Task<IActionResult> PostFoodRestrictions([FromBody]FoodRestrictionsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await this._userService.PostFoodRestrictions(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                //log
                return StatusCode(StatusCodes.Status400BadRequest, string.Format("Error trying to post food restrictions: {0}", ex.Message));
            }
        }

        [HttpPost]
        [Route("order")]
        public async Task<IActionResult> PostOrder([FromBody]CreateOrderRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await this._orderService.CreateOrder();
                return Ok("Order posted");
            }
            catch (Exception ex)
            {
                //log
                return StatusCode(StatusCodes.Status400BadRequest, string.Format("Error trying to post order: {0}", ex.Message));
            }
        }
    }
}
