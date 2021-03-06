﻿using GeekBurger.Ui.Application.ServiceBus;
using GeekBurger.Ui.Contracts.Messages;
using GeekBurger.Ui.Contracts.Request;
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
        private readonly IUserRetrievedReceiveMessageService _userRetrievedReceiveMessageService;
        private readonly IUserService _userService;
        private readonly IUIServiceBus _serviceBus;

        public UiController(IOrderService orderService, IUserService userService, IUIServiceBus serviceBus, IUserRetrievedReceiveMessageService userRetrievedReceiveMessageService)
        {
            this._orderService = orderService;
            this._userService = userService;
            this._serviceBus = serviceBus;
            this._userRetrievedReceiveMessageService = userRetrievedReceiveMessageService;
        }

        [HttpPost]
        [Route("face")]
        public async Task<IActionResult> PostFace([FromBody]PostFaceRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _userRetrievedReceiveMessageService.RequesterId = request.RequesterId;
                await _userService.PostUser(request.Face);
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
        public IActionResult PostOrder([FromBody]CreateOrderRequest request, CancellationToken cancellationToken)
        {
            try
            {                
                _serviceBus.AddToMessageList(request.ConvertToNewOrderMessage(), "8048e9ec-80fe-4bad-bc2a-e4f4a75c834e");
                _serviceBus.SendMessagesAsync("NewOrder");
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
