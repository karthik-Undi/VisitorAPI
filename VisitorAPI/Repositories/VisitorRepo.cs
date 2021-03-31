using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorAPI.Models;

namespace VisitorAPI.Repositories
{
    public class VisitorRepo: IVisitorRepo
    {
        private readonly CommunityGateDatabaseContext _context;

        public VisitorRepo()
        {

        }
        public VisitorRepo(CommunityGateDatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Visitors> GetAllVisitors()
        {
            return _context.Visitors.ToList();
        }
        public Visitors GetVisitorById(int id)
        {
            Visitors item = _context.Visitors.Find(id);
            return item;
        }

        public async Task<Visitors> PostVisitor(Visitors item)
        {
            Visitors visitor = null;
            if (item == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                visitor = new Visitors() {
                    VisitorName = item.VisitorName,
                    VisitStartTime = item.VisitStartTime,
                    VisitEndTime = item.VisitEndTime,
                    VisitorResaon = item.VisitorResaon,
                    VisitorEntryStatus = item.VisitorEntryStatus,
                    ResidentId=item.ResidentId,
                    EmployeeId=item.EmployeeId    
                };
                await _context.Visitors.AddAsync(visitor);
                await _context.SaveChangesAsync();
            }
            return visitor;
        }

       

        public async Task<Visitors> UpdateVisitor(Visitors item, int id)
        {
            Visitors visitor = await _context.Visitors.FindAsync(id);
            visitor.VisitEndTime = item.VisitEndTime;
            visitor.VisitStartTime = item.VisitStartTime;
            visitor.VisitorName = item.VisitorName;
            visitor.VisitorResaon = item.VisitorResaon;
            await _context.SaveChangesAsync();
            return visitor;
        }

    }
}
