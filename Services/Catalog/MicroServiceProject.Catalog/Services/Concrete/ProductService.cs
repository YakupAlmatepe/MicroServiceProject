using AutoMapper;
using MicroServiceProject.Catalog.Dtos;
using MicroServiceProject.Catalog.Models;
using MicroServiceProject.Catalog.Services.Abstract;
using MicroServiceProject.Catalog.Settings;
using MicroServiceProject.Shared.Dtos;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceProject.Catalog.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var dataBase = client.GetDatabase(databaseSettings.DatabaseName);
            _categoryCollection = dataBase.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _productCollection = dataBase.GetCollection<Product>(databaseSettings.ProductCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<ProductDto>> CreateAsync(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            await _productCollection.InsertOneAsync(product);
            return Response<ProductDto>.Success(200);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _productCollection.DeleteOneAsync(x => x.ProductID == id);
            if (result.DeletedCount > 0)
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                return Response<NoContent>.Fail("Id Bulunamadı", 404);
            }
        }

        public async Task<Response<List<ProductDto>>> GetAllAsync()
        {
            var products = await _productCollection.Find(product => true).ToListAsync();
            return Response<List<ProductDto>>.Success(200, _mapper.Map<List<ProductDto>>(products));
        }

        public async Task<Response<ProductDto>> GetByIDAsync(string id)
        {
            var product = await _productCollection.Find<Product>(x => x.ProductID == id).FirstOrDefaultAsync();
            if (product == null)
            {
                return Response<ProductDto>.Fail("Kategori Bulunamadı", 404);
            }
            else
            {
                return Response<ProductDto>.Success(200, _mapper.Map<ProductDto>(product));
            }
        }

        public async Task<Response<NoContent>> UpdateAsync(UpdateProductDto updateProductDto)
        {
            var product = _mapper.Map<Product>(updateProductDto);
            var result = await _productCollection.FindOneAndReplaceAsync(x => x.CategoryID == updateProductDto.CategoryID, product);
            if (result == null)
            {
                return Response<NoContent>.Fail("Kategori Bulunamadı", 404);
            }
            else
            {
                return Response<NoContent>.Success(204);
            }
        }
    }
}