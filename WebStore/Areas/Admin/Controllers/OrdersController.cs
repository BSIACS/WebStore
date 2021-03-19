using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Orders;
using WebStore.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Identity;
using WebStore.Infrastructure.Mapping;
using WebStore.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly WebStoreDB _dB;
        private readonly UserManager<User> _userManger;

        public OrdersController(IOrderService orderService, WebStoreDB dB, UserManager<User> userManager)
        {
            _orderService = orderService;
            _dB = dB;
            _userManger = userManager;
        }

        public IActionResult Index()
        {
            List<Order> orders = _dB.Orders.Include(o => o.User).ToList();

            return View(orders.ToViewModels());
        }

        public async Task<IActionResult> OrderDetail(int id)
        {
            Order order = await _orderService.GetOrderById(id);

            OrderDetailViewModel viewModel = new OrderDetailViewModel { 
                Id = order.Id,
                Address = order.Address,
                Phone = order.Phone,
                cart = order.ToCartViewModel(),
                user = (await _userManger.FindByIdAsync(order.User.Id)).ToViewModel() };

            if (order is null)
                return NotFound();

            return View(viewModel);
        }
    }
}
