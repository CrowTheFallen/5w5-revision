using Jungle_DataAccess.Repository.IRepository;
using Jungle_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jungle_DataAccess.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly JungleDbContext _db;

        public CustomerRepository(JungleDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task UpdateAsync(Customer customer)
        {
            _db.Update(customer);
        }
    }
}
