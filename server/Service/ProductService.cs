using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using DataAccess;

namespace Service
{
    public class ProductService : IProductService
    {
        private readonly WebShopContext _context;

        public ProductService(WebShopContext context)
        {
            _context = context;
        }

        public IEnumerable<Paper> GetProducts(string search, string filter, string sort)
        {
            var query = _context.Papers.AsQueryable();

            // Filter logic
            if (!string.IsNullOrEmpty(filter) && filter.Equals("discontinued", System.StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(p => p.Discontinued == true);
            }

            // Sort logic
            switch (sort?.ToLower())
            {
                case "name_asc":
                    query = query.OrderBy(p => p.Name);
                    break;
                case "name_desc":
                    query = query.OrderByDescending(p => p.Name);
                    break;
                default:
                    query = query.OrderBy(p => p.Name); // Default sorting by name ascending
                    break;
            }

            return query.ToList();
        }

        public void AddProduct(Paper paper)
        {
            _context.Papers.Add(paper);
            _context.SaveChanges();
        }

        public void DiscontinueProduct(int id)
        {
            var paper = _context.Papers.Find(id);
            if (paper != null)
            {
                paper.Discontinued = true;
                _context.SaveChanges();
            }
        }

        public void RestockProduct(int id, int quantity)
        {
            var paper = _context.Papers.Find(id);
            if (paper != null)
            {
                paper.Stock += quantity;
                _context.SaveChanges();
            }
        }

        public void AddCustomProperty(int paperId, string propertyName)
        {
            var paper = _context.Papers.Find(paperId);
            if (paper != null)
            {
                _context.SaveChanges();
            }
        }
    }
}