using Dapper;
using Microsoft.EntityFrameworkCore;
using OffStone.Example.Dal.Entities;
using OffStone.Example.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace OffStone.Example.Dal.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(NorthwindContext context) : base(context)
        {
        }

        public IEnumerable<Order> SearchWithEf(string customerId, DateTime? orderDate, string companyName)
        {
            return _context.Orders
                .Include(o => o.OrderDetails)
                .Include(o => o.Customer)
                .Where(o => (o.Customer.ContactName ?? companyName) == (companyName ?? o.Customer.ContactName))
                .Where(o => (o.CustomerId ?? customerId) == (customerId ?? o.CustomerId))
                .Where(o => (o.OrderDate ?? orderDate) == (orderDate ?? o.OrderDate));
        }

        public IEnumerable<Order> SearchWithDapperMulti(string customerId, DateTime? orderDate, string companyName)
        {
            var grid = _context.Connection.QueryMultiple("[dbo].[usp_SearchOrdersMulti]",
            new
            {
                CustomerId = customerId,
                OrderDate = orderDate,
                CompanyName = companyName
            },
            commandType: CommandType.StoredProcedure);

            // Capture the 3 result sets from the SP
            var orders = grid.Read<Order>();
            var customers = grid.Read<Customer>();
            var OrderDetails = grid.Read<OrderDetail>();

            // Build up the result graph into the correct structure
            foreach(var order in orders)
            {
                foreach(var customer in customers)
                {
                    if (order.CustomerId == customer.CustomerId)
                        order.Customer = customer;
                }

                foreach (var orderDetail in OrderDetails)
                    if (order.OrderId == orderDetail.OrderId)
                        order.OrderDetails.Add(orderDetail);
            }

            return orders;
        }

        public IEnumerable<Order> SearchWithDapperSplit(string customerId, DateTime? orderDate, string companyName)
        {
            var orders = _context.Connection.Query<Order, OrderDetail, Customer, Order>("[dbo].[usp_SearchOrders]",
            (o, od, c) =>
            {
                o.OrderDetails.Add(od);
                o.Customer = c;
                return o;
            },
            new
            {
                CustomerId = customerId,
                OrderDate = orderDate,
                CompanyName = companyName
            },
            splitOn: "OrderId, CustomerId",
            commandType: CommandType.StoredProcedure);
            //,commandTimeout: AppSettings.CommandTimeout.Medium);

            var returnOrders = new List<Order>();
            foreach (var order in orders)
            {
                if (!returnOrders.Any(o => o.OrderId == order.OrderId))
                    returnOrders.Add(order);

                foreach (var orderDetail in order.OrderDetails)
                    returnOrders.First(o => o.OrderId == orderDetail.OrderId)
                        .OrderDetails.Add(orderDetail);
            }
            return returnOrders;
        }
    }
}
