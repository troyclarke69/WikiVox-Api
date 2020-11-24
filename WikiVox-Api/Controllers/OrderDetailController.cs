using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wikivox_Api.Models;
using Wikivox_Api.Services;

namespace Wikivox_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly OrderDetailService _orderDetailService;
        private readonly ProductService _productService;

        public OrderDetailController(OrderDetailService service, ProductService productService)
        {
            _orderDetailService = service;
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetAll()
        {
            var OrderDetails = await _orderDetailService.GetAllAsync();
            return Ok(OrderDetails);
        }

        [HttpGet("{id:length(24)}", Name = "GetOrderDetail")]
        public async Task<ActionResult<OrderDetail>> GetById(string id)
        {
            var OrderDetail = await _orderDetailService.GetByIdAsync(id);
            if (OrderDetail == null)
            {
                return NotFound();
            }

            if (OrderDetail.ProductID != 0)
            {
                // call orderDetails method to get all detail objects
                var product = await _productService.GetByProductIdAsync(OrderDetail.ProductID);
                OrderDetail.ProductData = product;

            }

            return Ok(OrderDetail);
        }

        [Route("[action]/{orderId}")]
        [HttpGet]
        public async Task<ActionResult<OrderDetail>> GetByOrderId(int orderId)
        {
            //var OrderDetail = await _orderDetailService.GetByIdAsync(id);
            List<OrderDetail> detailList = new List<OrderDetail>();

            // flag: getProduct: when getting ALL, set this to false as to speed up query
            // for the /orders page, we do not need product detail
            Boolean getProduct = false;
            var OrderDetail = await _orderDetailService.GetAllByOrderIdAsync(orderId, getProduct);
            if (OrderDetail != null)
            {

                foreach (var _detail in OrderDetail)
                {
                    if (_detail.ProductID != 0)
                    {
                        // call orderDetails method to get all detail objects
                        var product = await _productService.GetByProductIdAsync(_detail.ProductID);
                        _detail.ProductData = product;

                    }
                    detailList.Add(_detail);
                }

            }

            return Ok(detailList);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderDetail OrderDetail)
        {
            // got all required fields?
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _orderDetailService.CreateAsync(OrderDetail);
            return Ok(OrderDetail);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, OrderDetail updatedOrderDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var queriedOrderDetail = await _orderDetailService.GetByIdAsync(id);
            if (queriedOrderDetail == null)
            {
                return NotFound();
            }
            await _orderDetailService.UpdateAsync(id, updatedOrderDetail);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var OrderDetail = await _orderDetailService.GetByIdAsync(id);
            if (OrderDetail == null)
            {
                return NotFound();
            }
            await _orderDetailService.DeleteAsync(id);
            return NoContent();
        }
    }
}
