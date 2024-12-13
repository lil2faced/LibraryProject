using LibraryProject.Applications;
using LibraryProject.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using LibraryProject.Interfaces;

namespace LibraryProject.Services
{
    public class StatusService : IStatusService
    {
        private readonly DatabaseContext databaseContext;
        public StatusService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public async Task<int> Add(Status status)
        {
            if (status == null)
            {
                throw new ArgumentNullException();
            }
            var temp = await databaseContext.Statuses.Where(s => s.Name == status.Name).FirstOrDefaultAsync();
            if (temp != null)
            {
                return 1;
            }
            await databaseContext.Statuses.AddAsync(status);
            await databaseContext.SaveChangesAsync();
            return 0;
        }
        public async Task<List<Status>> GetAll()
        {
            return await databaseContext.Statuses.ToListAsync();
        }
        public async Task<(int,Status?)> GetById(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var status = await databaseContext.Statuses.FindAsync(id);
            if (status == null)
            {
                return (1, null);
            }
            return (0, status);
        }
        public async Task<int> Update(int? id, Status status)
        {
            if (id == null || status == null) throw new ArgumentNullException();
            var temp = databaseContext.Statuses.Find(id);
            if (temp == null)
            {
                return 1;
            }
            temp.Name = status.Name;
            databaseContext.Statuses.Update(temp);
            await databaseContext.SaveChangesAsync();
            return 0;
        }
        public async Task<int> Delete(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
                var temp = databaseContext.Statuses.Find(id);
            if (temp == null)
            {
                return 1;
            }
            databaseContext.Remove(temp);
            await databaseContext.SaveChangesAsync();
            return 0;
        }
    }
}
