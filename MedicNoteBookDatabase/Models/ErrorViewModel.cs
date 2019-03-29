//Christopher Sanderson
//MedicNoteBook
using System;

namespace MedicNoteBookDatabase.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}