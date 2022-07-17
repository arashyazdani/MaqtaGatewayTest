using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductsController(IGenericRepository<Product> productsRepository, IGenericRepository<ProductType> productTypeRepository, IGenericRepository<ProductBrand> productBrandRepository, IMapper mapper, IProductService productService)
        {
            _productsRepository = productsRepository;
            _productTypeRepository = productTypeRepository;
            _productBrandRepository = productBrandRepository;
            _mapper = mapper;
            _productService = productService;
        }

        //[Authorize]
        [Cached(600)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            try
            {
                var spec = new ProductsWithTypesAndBrandsSpecification(productParams);

                var countSpec = new ProductWithFiltersForCountSpecificication(productParams);

                var totalItems = await _productsRepository.CountAsync(countSpec);

                var products = await _productsRepository.ListAsync(spec);

                var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

                return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
            }
            catch (Exception ex)
            {

                return BadRequest(new ApiResponse(400, ex.Message));
            }
        }

        //[Authorize]
        [Cached(600)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            try
            {
                var spec = new ProductsWithTypesAndBrandsSpecification(id);
                var product = await _productsRepository.GetEntityWithSpec(spec);

                if (product == null) return NotFound(new ApiResponse(404));

                return _mapper.Map<Product, ProductToReturnDto>(product);
            }
            catch (Exception ex)
            {

                return BadRequest(new ApiResponse(400, ex.Message));
            }
        }

        //[Authorize]
        [Cached(600)]
        [HttpPost("addproduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductToReturnDto>> AddProduct([FromQuery] AddProductDto productDto)
        {
            try
            {
                var spec = new ProductsWithTypesAndBrandsSpecification(productDto.Name);
                var product = await _productsRepository.GetEntityWithSpec(spec);

                var productBrandId = await _productBrandRepository.GetByIdAsync(productDto.ProductBrandId);
                var productTypeId = await _productBrandRepository.GetByIdAsync(productDto.ProductTypeId);

                if (productBrandId == null) return BadRequest(new ApiResponse(404, "ProductBrandId is invalid."));
                if (productTypeId == null) return BadRequest(new ApiResponse(404, "ProductTypeId is invalid."));
                if (product != null) return BadRequest(new ApiResponse(404, "Product name is already added."));

                var addProduct = _mapper.Map<AddProductDto, Product>(productDto);

                var productReturn = await _productService.AddProductAsync(addProduct);

                return _mapper.Map<Product, ProductToReturnDto>(productReturn);
            }
            catch (Exception ex)
            {

                return BadRequest(new ApiResponse(400, ex.Message));
            }
        }

        //[Authorize]
        [Cached(600)]
        [HttpPost("updateproduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductToReturnDto>> UpdateProduct(UpdateProductDto productDto)
        {
            try
            {
                var spec = new ProductsWithTypesAndBrandsSpecification(productDto.Id);
                var product = await _productsRepository.GetEntityWithSpec(spec);

                var productBrandId = await _productBrandRepository.GetByIdAsync(productDto.ProductBrandId);
                var productTypeId = await _productBrandRepository.GetByIdAsync(productDto.ProductTypeId);

                if (productBrandId == null) return BadRequest(new ApiResponse(404, "ProductBrandId is invalid."));
                if (productTypeId == null) return BadRequest(new ApiResponse(404, "ProductTypeId is invalid."));

                var specName = new ProductsWithTypesAndBrandsSpecification(productDto.Name);
                var productNameCheck = await _productsRepository.GetEntityWithSpec(specName);
                if (productNameCheck != null) return BadRequest(new ApiResponse(404, "Product name is already added."));

                var updateProduct = _mapper.Map<UpdateProductDto, Product>(productDto);

                var productReturn = await _productService.UpdateProductAsync(updateProduct);

                return _mapper.Map<Product, ProductToReturnDto>(productReturn);
            }
            catch (Exception ex)
            {

                return BadRequest(new ApiResponse(400, ex.Message));
            }
        }

        [Cached(600)]
        [HttpPut("deleteproduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductToReturnDto>> DeleteProduct(int id)
        {
            try
            {
                var spec = new ProductsWithTypesAndBrandsSpecification(id);
                var product = await _productsRepository.GetEntityWithSpec(spec);

                if (product == null) return NotFound(new ApiResponse(404));

                var deleteProduct = await _productService.DeleteProductAsync(id);

                if (deleteProduct == null) return BadRequest(new ApiResponse(404, "Product has not been deletet."));

                return Ok("Product has been deleted.");
            }
            catch (Exception ex)
            {

                return BadRequest(new ApiResponse(400, ex.Message));
            }
        }

        //[Authorize]
        [Cached(600)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            try
            {
                return Ok(await _productBrandRepository.ListAllAsync());

            }
            catch (Exception ex)
            {

                return BadRequest(new ApiResponse(400, ex.Message));
            }
        }

        //[Authorize]
        [Cached(600)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            try
            {
                return Ok(await _productTypeRepository.ListAllAsync());

            }
            catch (Exception ex)
            {

                return BadRequest(new ApiResponse(400, ex.Message));
            }
        }
    }
}
