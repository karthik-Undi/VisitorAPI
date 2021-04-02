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
        public IEnumerable<Visitors> GetVisitorById(int id)
        {
            var item = _context.Visitors.Where(vis => vis.ResidentId == id).ToList();
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
                    VisitorEntryStatus = "Scheduled",
                    ResidentId=item.ResidentId,
                    EmployeeId=item.EmployeeId    
                };
                await _context.Visitors.AddAsync(visitor);
                await _context.SaveChangesAsync();
            }
            return visitor;
        }

       

        public async Task<Visitors> UpdateVisitor(int id,Visitors item)
        {
            Visitors visitor = await _context.Visitors.FindAsync(id);
            visitor.VisitEndTime = item.VisitEndTime;
            visitor.VisitStartTime = item.VisitStartTime;
            visitor.VisitorName = item.VisitorName;
            visitor.VisitorResaon = item.VisitorResaon;
            await _context.SaveChangesAsync();
            return visitor;
        }

        public async Task<Visitors> DeleteVisitor(int id)
        {
            Visitors visitor = await _context.Visitors.FindAsync(id);
            if (visitor == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                _context.Visitors.Remove(visitor);
                await _context.SaveChangesAsync();
            }
            return visitor;

            }
        }
}
