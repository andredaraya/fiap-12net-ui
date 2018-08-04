﻿using GeekBurger.Ui.Contracts.Request;
using GeekBurger.Ui.Domain.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GeekBurger.Ui.Api.Controllers
{
    [Route("api/")]
    public class UiController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IStoreCatalogService _storeCatalogService;
        private readonly IUserService _userService;

        public UiController(IOrderService orderService, IStoreCatalogService storeCatalogService, IUserService userService)
        {
            this._orderService = orderService;
            this._storeCatalogService = storeCatalogService;
            this._userService = userService;
        }

        [HttpPost]
        [Route("face")]
        public async Task<IActionResult> PostFace([FromBody]string value, CancellationToken cancellationToken)
        {
            try
            {
                await this._userService.PostUser();
                return Ok("Face under process");
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
        public async Task<IActionResult> PostOrder([FromBody]string value, CancellationToken cancellationToken)
        {
            try
            {
                await this._orderService.CreateOrder();
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
