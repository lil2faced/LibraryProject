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
        public async Task Add(Status status, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            if (status == null)
            {
                throw new ArgumentNullException();
            }
            var temp = await databaseContext.Statuses.Where(s => s.Name == status.Name).FirstOrDefaultAsync();
            if (temp != null)
            {
                throw new Exception("Такой статус уже существует");
            }
            await databaseContext.Statuses.AddAsync(status);
            await databaseContext.SaveChangesAsync();
        }
        public async Task<List<Status>> GetAll(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            return await databaseContext.Statuses.ToListAsync();
        }
        public async Task<Status> GetById(int? id, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var status = await databaseContext.Statuses.FindAsync(id);
            if (status == null)
            {
                throw new Exception("Статус не найден");
            }
            return status;
        }
        public async Task Update(int? id, Status status)
        {
            if (id == null || status == null) throw new ArgumentNullException();
            var temp = databaseContext.Statuses.Find(id);
            if (temp == null)
            {
                throw new Exception("Статус не найден");
            }
            temp.Name = status.Name;
            databaseContext.Statuses.Update(temp);
            await databaseContext.SaveChangesAsync();
        }
        public async Task Delete(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
                var temp = databaseContext.Statuses.Find(id);
            if (temp == null)
            {
                throw new Exception("Статус не найден");
            }
            databaseContext.Remove(temp);
            await databaseContext.SaveChangesAsync();
        }
    }
}
