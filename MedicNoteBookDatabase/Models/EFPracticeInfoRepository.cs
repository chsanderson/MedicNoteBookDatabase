using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    public class EFPracticeInfoRepository : IPracticeInfoRepository
    {
        private ApplicationDBContext context;

        public EFPracticeInfoRepository(ApplicationDBContext applicationDBContext)
        {
            context = applicationDBContext;
        }

        public IQueryable<PracticeInfo> PracticeInfo => context.PracticeInfo;
    }
}
