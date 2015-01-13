using System.Collections.Generic;

namespace ParatureAPI.ParaObjects
{
    public class TicketStatusList : PagedData.PagedData
    {
        public List<TicketStatus> TicketStatuses = new List<TicketStatus>();
        public TicketStatusList()
        {
        }
        public TicketStatusList(TicketStatusList ticketstatuslist)
            : base(ticketstatuslist)
        {
            TicketStatuses = new List<TicketStatus>(ticketstatuslist.TicketStatuses);
        }

    }
}