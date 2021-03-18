using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Orders;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> GetUserOrders(string UserName);

        Task<Order> GetOrderById(int id);

        public Task CreateOrder(OrderViewModel viewModel);

        public void DeleteOrder(int id);
    }
}
