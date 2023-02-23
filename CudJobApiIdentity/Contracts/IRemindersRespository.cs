using CUDJobApiIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Contracts
{
    interface IRemindersRespository
    {
        public Task<bool> updateReminderConfig(ReminderConfig data);
    }
}
