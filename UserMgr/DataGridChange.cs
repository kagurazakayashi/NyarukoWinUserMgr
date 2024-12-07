using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinUserMgr
{
    public class DataGridChange
    {
        public object OriginalValue { get; set; }
        public object NewValue { get; set; }
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public string RowFirstColumnValue { get; set; }
        public string Title { get; set; }
    }
}
