using GeekBurger.Ui.Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GeekBurger.Ui.Api.Controllers
{
    [Route("api/ui")]
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

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
