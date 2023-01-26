using FWScan.EventTrackerService.Models;

namespace FWScan.EventTrackerService.Data
{
    public class DataAccess
    {
        private readonly DataContext _dataContext;

        public DataAccess(DataContext dataContext) 
        {
            _dataContext= dataContext;
        }

        public void AddScanEvents(List<ScanEvent> scanEvents)
        {
            _dataContext.AddRange(scanEvents);
            _dataContext.SaveChanges();
        }

        public void AddScanEvent(ScanEvent scanEvent)
        {
            throw new NotImplementedException();
        }

        public long GetLatestScanEventId()
        {
            var item = _dataContext.ScanEvents.OrderByDescending(f => f.EventId).FirstOrDefault();

            if(item == null)
            {
                return 0;
            }

            return item.EventId;

        }

        public List<ScanEvent> GetScanEventsFromId(long id) 
        {
            throw new NotImplementedException();
        }
    }
}
