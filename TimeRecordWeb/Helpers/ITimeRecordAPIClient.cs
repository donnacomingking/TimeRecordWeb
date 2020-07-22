using System.Collections.Generic;
using System.Threading.Tasks;
using TimeRecordWeb.Models;

namespace TimeRecordWeb.Helpers
{
    public interface ITimeRecordAPIClient
    {
        Task<TimeRecordModel> CreateTimeRecord(TimeRecordModel timeRecordEntry);
        Task<bool> DeleteTimeRecordById(int id);
        Task<IList<TimeRecordModel>> GetAllTimeRecordAsync();
        Task<IList<TimeRecordModel>> GetAllTimeRecordByUserId(int userId);
        Task<TimeRecordModel> GetTimeRecordById(int id);
        Task<TimeRecordModel> UpdateTimeRecord(int id, TimeRecordModel timeRecordEntry);
    }
}