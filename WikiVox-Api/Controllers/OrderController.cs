using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wikivox_Api.Dtos;
using Wikivox_Api.Models;
using Wikivox_Api.Services;

namespace Wikivox_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly OrderDetailService _orderDetailService;
        private readonly CustomerService _customerService;
        private IMapper _mapper;

        public OrderController(OrderService service, OrderDetailService orderDetailService,
            ProductService productService, CustomerService customerService, IMapper mapper)
        {
            _orderService = service;
            _orderDetailService = orderDetailService;
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetAll([FromQuery] OrderParams orderParams)
        {
            var orders = await _orderService.GetAllAsync(orderParams);
            var meta = new
            {
                orders.TotalCount,
                orders.PageSize,
                orders.CurrentPage,
                orders.TotalPages,
                orders.HasNext,
                orders.HasPrevious
            };

            Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(meta));

            return Ok(_mapper.Map<IEnumerable<OrderReadDto>>(orders));
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Order>>> GetAll([FromQuery] OrderParams orderParams)
        //{
        //    var Orders = await _orderService.GetAllAsync(orderParams);
        //    var meta = new
        //    {
        //        Orders.TotalCount,
        //        Orders.PageSize,
        //        Orders.CurrentPage,
        //        Orders.TotalPages,
        //        Orders.HasNext,
        //        Orders.HasPrevious
        //    };

        //    Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(meta));
        //    return Ok(Orders);
        //}

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Order>>> GetAll([FromQuery] OrderParams orderParams)
        //{
        //    var Orders = await _orderService.GetAllAsync(orderParams);
        //    return Ok(Orders);
        //}

        [Route("[action]/{customerId}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrdersByCustomerId(string customerId)
        {
            var Orders = await _orderService.GetByCustomerIdAsync(customerId);
            List<Order> OrderList = new List<Order>();

            // flag: getProduct - set true if need to bring back product data
            // here we do not want to
            Boolean getProduct = false;

            foreach (var _order in Orders)
            {
                _order.SubTotal = await _orderService.GetOrderTotal(_order.OrderID);
                _order.OrderTotal = _order.SubTotal + _order.Freight;

                if (_order.CustomerID != null)
                {
                    var customer = await _customerService.GetByCustomerIdAsync(_order.CustomerID);
                    _order.CustomerData = customer;
                }

                if (_order.OrderID != 0)
                {
                    var detailList = new List<OrderDetail>();
                    detailList = await _orderDetailService.GetAllByOrderIdAsync(_order.OrderID, getProduct);
                    _order.DetailData = detailList;
                }
                OrderList.Add(_order);
            }

            return Ok(OrderList);
        }


        // NOTES: ISSUE: Returns one blank record ... ???

        //[Route("[action]/{customerId}")]
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetAllOrdersByCustomerId(string customerId)
        //{
        //    var orders = await _orderService.GetByCustomerIdAsync(customerId);
        //    List<Order> orderList = new List<Order>();

        //    // flag: getProduct - set true if need to bring back product data
        //    // here we do not want to
        //    Boolean getProduct = false;

        //    foreach (var _order in orders)
        //    {
        //        _order.SubTotal = await _orderService.GetOrderTotal(_order.OrderID);
        //        _order.OrderTotal = _order.SubTotal + _order.Freight;

        //        if (_order.CustomerID != null)
        //        {
        //            var customer = await _customerService.GetByCustomerIdAsync(_order.CustomerID);
        //            _order.CustomerData = customer;
        //        }

        //        if (_order.OrderID != 0)
        //        {
        //            var detailList = new List<OrderDetail>();
        //            detailList = await _orderDetailService.GetAllByOrderIdAsync(_order.OrderID, getProduct);
        //            _order.DetailData = detailList;
        //        }
        //        orderList.Add(_order);
        //    }

        //    //return Ok(OrderList);
        //    return Ok(_mapper.Map<OrderReadDto>(orderList));
        //}


        [HttpGet("{id:length(24)}", Name = "GetOrder")]
        public async Task<ActionResult<Order>> GetById(string id)
        {
            var Order = await _orderService.GetByIdAsync(id);
            if (Order == null)
            {
                return NotFound();
            }
            return Ok(Order);
        }

        //[Route("[action]/{orderId}")]
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Order>>> GetOrderByOrderId(Int32 orderId)
        //{
        //    var order = await _orderService.GetByOrderIdAsync(orderId);
        //    //Order orderWithDetails = new Order();

        //    // flag: getProduct - set true to get product details
        //    Boolean getProduct = true;

        //    if (order.CustomerID != null)
        //    {
        //        var customer = await _customerService.GetByCustomerIdAsync(order.CustomerID);
        //        order.CustomerData = customer;
        //    }

        //    if (order.OrderID != 0)
        //    {
        //        var detailList = new List<OrderDetail>();
        //        detailList = await _orderDetailService.GetAllByOrderIdAsync(order.OrderID, getProduct);
        //        order.DetailData = detailList;
        //    }

        //    order.SubTotal = await _orderService.GetOrderTotal(order.OrderID);
        //    // must add order.freight to get grand total
        //    order.OrderTotal = order.SubTotal + order.Freight;

        //    return Ok(order);
        //}

        [Route("[action]/{orderId}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetOrderByOrderId(Int32 orderId)
        {
            var order = await _orderService.GetByOrderIdAsync(orderId);

            // flag: getProduct - set true to get product details
            Boolean getProduct = true;

            if (order.CustomerID != null)
            {
                var customer = await _customerService.GetByCustomerIdAsync(order.CustomerID);
                order.CustomerData = customer;
            }

            if (order.OrderID != 0)
            {
                var detailList = new List<OrderDetail>();
                detailList = await _orderDetailService.GetAllByOrderIdAsync(order.OrderID, getProduct);
                order.DetailData = detailList;
            }

            order.SubTotal = await _orderService.GetOrderTotal(order.OrderID);
            // must add order.freight to get grand total
            order.OrderTotal = order.SubTotal + order.Freight;

            //return Ok(order);
            return Ok(_mapper.Map<OrderReadDto>(order));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order Order)
        {
            // got all required fields?
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _orderService.CreateAsync(Order);
            return Ok(Order);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Order updatedOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var queriedOrder = await _orderService.GetByIdAsync(id);
            if (queriedOrder == null)
            {
                return NotFound();
            }
            await _orderService.UpdateAsync(id, updatedOrder);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Order = await _orderService.GetByIdAsync(id);
            if (Order == null)
            {
                return NotFound();
            }
            await _orderService.DeleteAsync(id);
            return NoContent();
        }
    }
}
