using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public UserViewModel user { get; set; }

        public CartViewModel cart { get; set; }
    }
}
