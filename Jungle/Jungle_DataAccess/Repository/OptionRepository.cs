using Jungle_DataAccess.Repository.IRepository;
using Jungle_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jungle_DataAccess.Repository
{
    class OptionRepository : Repository<Option>, IOptionRepository
    {
        private readonly JungleDbContext _db;

        public OptionRepository(JungleDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task UpdateAsync(Option option)
        {
            _db.Update(option);
        }
    }
}
