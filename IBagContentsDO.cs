using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDataAccessLayer
{
    public interface IBagContentsDO
    {
        int PKBagContentsID { get; set; }

        int FKBugOutBagID { get; set; }

        int FKItemID { get; set; }
    }
}
