﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace WebStore.Domain.Entities
{
    public class Cart
    {
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();

        public int ItemsCount => Items.Sum(i => i.Quantity);
    }
}
