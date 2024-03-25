
using Microsoft.EntityFrameworkCore;
using PeopleManager.Core;
using PeopleManager.Model;

namespace PeopleManager.Services
{
    public class AssignmentService(PeopleManagerDbContext dbContext)
    {
        public async Task<IList<Assignment>> Find()
        {
            return await dbContext.Assignments
                .Include(a => a.AssignedTo)
                .ToListAsync();
        }

        public async Task<Assignment?> Get(int id)
        {
            return await dbContext.Assignments
                .Include(a => a.AssignedTo)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Assignment?> Create(Assignment assignment)
        {
            dbContext.Assignments.Add(assignment);
            await dbContext.SaveChangesAsync();

            return assignment;
        }

        public async Task<Assignment?> Update(int id, Assignment assignment)
        {
            var dbAssignment = await dbContext.Assignments
                .FirstOrDefaultAsync(p => p.Id == id);

            if (dbAssignment is null)
            {
                return null;
            }

            dbAssignment.Name = assignment.Name;
            dbAssignment.Description = assignment.Description;
            dbAssignment.AssignedToId = assignment.AssignedToId;

            await dbContext.SaveChangesAsync();

            return assignment;
        }

        public async Task Delete(int id)
        {
            var dbAssignment = await dbContext.Assignments.FirstOrDefaultAsync(p => p.Id == id);

            if (dbAssignment is null)
            {
                return;
            }

            dbContext.Assignments.Remove(dbAssignment);

            await dbContext.SaveChangesAsync();
        }
    }
}
