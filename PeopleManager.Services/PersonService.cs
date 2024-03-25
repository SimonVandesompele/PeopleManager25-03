using Microsoft.EntityFrameworkCore;
using PeopleManager.Core;
using PeopleManager.Model;

namespace PeopleManager.Services
{
    public class PersonService(PeopleManagerDbContext dbContext)
    {
        public async Task<IList<Person>> Find()
        {
            return await dbContext.People
                .ToListAsync();
        }

        public async Task<Person?> Get(int id)
        {
            return await dbContext.People
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Person?> Create(Person person)
        {
            dbContext.People.Add(person);
            await dbContext.SaveChangesAsync();

            return person;
        }

        public async Task<Person?> Update(int id, Person person)
        {
            var dbPerson = await dbContext.People.FirstOrDefaultAsync(p => p.Id == id);

            if (dbPerson is null)
            {
                return null;
            }

            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;
            dbPerson.Email = person.Email;

            await dbContext.SaveChangesAsync();

            return person;
        }

        public async Task Delete(int id)
        {
            var dbPerson = await dbContext.People.FirstOrDefaultAsync(p => p.Id == id);

            if (dbPerson is null)
            {
                return;
            }

            dbContext.People.Remove(dbPerson);

            await dbContext.SaveChangesAsync();
        }
    }
}
