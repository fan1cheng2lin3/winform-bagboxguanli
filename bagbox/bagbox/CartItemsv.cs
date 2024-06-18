using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace bagbox
{
    public class CartItemsv
    {
        DataClasses1DataContext db = new DataClasses1DataContext();

        public CartItem Add(int customerId, int productID, int qty)
        {
            CartItem cartItem = null;

            Product_Table product = (from p in db.Product_Table where p.Goods_ID == productID select p).First();


            cartItem = new CartItem();
            cartItem.Customerld = customerId;
            cartItem.Proid = product.Goods_ID;
            cartItem.ProName = product.Goods_Name;
            cartItem.ListPrice = product.Price.Value;
            cartItem.Unprice = product.Unit_Price.Value;
            cartItem.Qty = qty;


            int ExistCartItemCount = (from c in db.CartItem
                                      where c.Customerld == customerId && c.Proid == productID
                                      select c).Count();

            if (ExistCartItemCount > 0)
            {
                CartItem existCartItem = (from c in db.CartItem
                                          where c.Customerld == customerId && c.Proid == productID
                                          select c).First();

                existCartItem.Qty += qty;

            }
            else
            {
                db.CartItem.InsertOnSubmit(cartItem);
            }

            db.SubmitChanges();
            return cartItem;
        }


        public (decimal, decimal) GetTotalPriceByCustomerId(int customerId)
        {
            List<CartItem> list = (from c in db.CartItem
                                   where c.Customerld == customerId
                                   select c).ToList();

            return ((decimal)list.Sum(c => c.ListPrice * c.Qty), (decimal)list.Sum(c => (c.ListPrice - c.Unprice) * c.Qty));

        }
    }
}