using Jungle_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jungle_DataAccess.Repository.IRepository
{
    public interface IEvaluationRepository : IRepository<Evaluation>
    {
        void Update(Evaluation evaluation);
    }
}
