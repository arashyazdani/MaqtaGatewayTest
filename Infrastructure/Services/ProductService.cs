using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;

        public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            _unitOfWork.Repository<Product>().Add(product);

            // save to db
            var result = await _unitOfWork.Complete();
            if (result <= 0) return null;

            // return product
            return product;
        }

        public async Task<string> DeleteProductAsync(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var existingProduct = await _unitOfWork.Repository<Product>().GetEntityWithSpec(spec);

            if (existingProduct != null)
            {
                _unitOfWork.Repository<Product>().Delete(existingProduct);

                var result = await _unitOfWork.Complete();
                if (result <= 0) return null;

                // return product
                return "Ok";
            }

            return null;
        }

        public Task<IEnumerable<Product>> GetProductsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            var result = await _productRepository.UpdateProduct(product);

            // save to db
            //var result = await _unitOfWork.Complete();
            if (result == null) return null;

            // return product
            return product;
        }
    }
}
