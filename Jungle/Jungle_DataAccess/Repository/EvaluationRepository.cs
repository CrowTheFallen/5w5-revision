using Jungle_DataAccess.Repository.IRepository;
using Jungle_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jungle_DataAccess.Repository
{
    public class EvaluationRepository : Repository<Evaluation>, IEvaluationRepository
    {
        private readonly JungleDbContext _db;

        public EvaluationRepository(JungleDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task UpdateAsync(Evaluation evaluation)
        {
            _db.Update(evaluation);
        }
    }
}
