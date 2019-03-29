//Christopher Sanderson
//MedicNoteBook
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicNoteBookDatabase.Models
{
    public interface IPracticeInfoRepository
    {
        IQueryable<PracticeInfo> PracticeInfo { get; }
    }
}