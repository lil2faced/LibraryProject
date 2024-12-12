using LibraryProject.Applications;
using LibraryProject.Entities.Orders;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Services
{
    public class StatusService
    {
        public static async Task<int> Add(DatabaseContext databaseContext,Status status)
        {
            var temp = await databaseContext.Statuses.Where(s => s.Name == status.Name).FirstOrDefaultAsync();
            if (temp != null)
            {
                return 1;
            }
            await databaseContext.Statuses.AddAsync(status);
            await databaseContext.SaveChangesAsync();
            return 0;
        }
        public static async Task<List<Status>> GetAll(DatabaseContext databaseContext)
        {
            return await databaseContext.Statuses.ToListAsync();
        }
        public static async Task<(int,Status?)> GetById(DatabaseContext databaseContext, int id)
        {
            var status = await databaseContext.Statuses.FindAsync(id);
            if (status == null)
            {
                return (1, null);
            }
            return (0, status);
        }
        public static async Task<int> Update(DatabaseContext databaseContext, int id, Status status)
        {
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
        public static async Task<int> Delete(DatabaseContext databaseContext, int id)
        {
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
