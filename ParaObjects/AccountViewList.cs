using System.Collections.Generic;

namespace ParatureAPI.ParaObjects
{
    public class AccountViewList : PagedData.PagedData
    {
        public List<AccountView> views = new List<AccountView>();

    }
}