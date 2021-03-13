using OffStone.Example.Dal.Entities;
using OffStone.Example.DAL.Repositories;
using System;
using System.Collections.Generic;

namespace OffStone.Example.Dal.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        public IEnumerable<Order> SearchWithDapperSplit(string customerId, 
            DateTime? orderDate, string companyName);

        public IEnumerable<Order> SearchWithDapperMulti(string customerId,
            DateTime? orderDate, string companyName);

        public IEnumerable<Order> SearchWithEf(string customerId,
            DateTime? orderDate, string companyName);
    }
}
