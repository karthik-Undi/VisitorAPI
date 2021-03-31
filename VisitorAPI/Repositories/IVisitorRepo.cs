using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorAPI.Models;

namespace VisitorAPI.Repositories
{
    public interface IVisitorRepo
    {
        Task<Visitors> PostVisitor(Visitors item);

        IEnumerable<Visitors> GetAllVisitors();
        Visitors GetVisitorById(int id);
        Task<Visitors> UpdateVisitor(Visitors item, int id);
    }
}
