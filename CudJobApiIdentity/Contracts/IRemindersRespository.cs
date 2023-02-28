using CUDJobApiIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Contracts
{
    public interface IRemindersRespository
    {
        Task<bool> UpdateReminderConfig(ReminderConfig data);
        Task<List<ReminderConfig>> ReminderConfigList();
        Task<List<Reminders>> ReminderList();
        Task<bool> UpdateReminders(Reminders data);
        Task<bool> Save();
    }
}
