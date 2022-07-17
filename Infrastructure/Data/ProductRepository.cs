using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly MaqtaGatewayStoreDbContext _context;

        public ProductRepository(MaqtaGatewayStoreDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var oldProduct = await _context.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).FirstOrDefaultAsync(p => p.Id == product.Id);
            if (oldProduct != null)
            {
                oldProduct.Id = product.Id;
                oldProduct.Name = product.Name;
                oldProduct.Description = product.Description;
                oldProduct.PictureUrl = product.PictureUrl;
                oldProduct.Price = product.Price;
                oldProduct.ProductBrandId = product.ProductBrandId;
                oldProduct.ProductTypeId = product.ProductTypeId;

                _context.Products.Update(oldProduct);
                await _context.SaveChangesAsync();
                return oldProduct;
            }

            return null;
        }
    }
}
