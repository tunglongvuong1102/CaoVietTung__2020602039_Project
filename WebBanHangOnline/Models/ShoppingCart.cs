using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models
{
    public class ShoppingCart
    {
        public List<ShoppingCartItem> Items { get; set; }

        public ShoppingCart()
        {
            this.Items = new List<ShoppingCartItem>();
        }

        public void AddToCart(ShoppingCartItem item, int Quantity)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            var checkExits = Items.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (checkExits != null)
            {
                checkExits.Quantity += Quantity;
                checkExits.TotalPrice = checkExits.Price * checkExits.Quantity;
            }
            else
            {
                item.Quantity = Quantity;
                item.TotalPrice = item.Price * item.Quantity;
                Items.Add(item);
            }
        }

        public void Remove(int id)
        {
            var checkExits = Items.FirstOrDefault(x => x.ProductId == id);
            if (checkExits != null)
            {
                Items.Remove(checkExits);
            }
        }

        public void UpdateQuantity(int id, int quantity)
        {
            var checkExits = Items.FirstOrDefault(x => x.ProductId == id);
            if (checkExits != null)
            {
                if (quantity <= 0)
                {
                    Remove(id); // Xóa sản phẩm nếu số lượng <= 0
                }
                else
                {
                    checkExits.Quantity = quantity;
                    checkExits.TotalPrice = checkExits.Price * checkExits.Quantity;
                }
            }
        }

        public decimal GetTotalPrice()
        {
            return Items.Sum(x => x.TotalPrice);
        }

        public int GetTotalQuantity()
        {
            return Items.Sum(x => x.Quantity);
        }

        public void ClearCart()
        {
            Items.Clear();
        }
    }

    public class ShoppingCartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Alias { get; set; }
        public string CategoryName { get; set; }
        public string ProductImg { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}