using CUDJobApiIdentity.Contracts;
using CUDJobApiIdentity.Models;
using CUDJobAPiIdentity.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Services
{
    public class NotesRepository : INotesRepository
    {
        public NotesRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public ApplicationDbContext _db { get; set; }

        public async Task<StatusesNotes> Create(StatusesNotes entity)
        {
            StatusesNotes Notes = new StatusesNotes {Notes = entity.Notes,StatusID = entity.StatusID };
            try
            {                
                await _db.StatusesNotes.AddAsync(Notes);
                var isSuccess = await Save();
                if (!isSuccess)
                {

                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine($"{Ex.Message} - {Ex.InnerException}");
                throw;
            }
            return Notes;
        }

        public Task<bool> Delete(StatusesNotes entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<StatusesNotes>> FindAll()
        {
            throw new NotImplementedException();
        }

        public Task<StatusesNotes> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<StatusesNotes> FindBystring(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> isExists(int id)
        {
            return await _db.StatusesNotes.AnyAsync(o => o.NotesID == id);
        }

        public async Task<bool> Save()
        {
            var chaanges = await _db.SaveChangesAsync();
            return chaanges > 0;
        }

        public async Task<bool> Update(StatusesNotes entity)
        {
            StatusesNotes Notes = new StatusesNotes { Notes = entity.Notes, NotesID = entity.NotesID, StatusID = entity.StatusID };
            _db.StatusesNotes.Update(Notes);
            return await Save();           
        }
    }
}
