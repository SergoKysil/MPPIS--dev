using Application.Dto;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.RDBMS;
using Domain.RDBMS.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Product> _productRepository;

        public ProductService(IMapper mapper, IRepository<Product> repository)
        {
            this._mapper = mapper;
            this._productRepository = repository;
        }

        public async Task<ProductDto> AddProduct(AddProductDto addProductDto)
        {
            var product = _mapper.Map<Product>(addProductDto);
            _productRepository.Add(product);
            await _productRepository.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> GetById(int id, UserDto userId)
        {
            return _mapper.Map<ProductDto>(await _productRepository.GetAll().Where(x => x.UserId == userId.Id).FirstOrDefaultAsync(x => x.Id == id));

        }

        public async Task<IEnumerable<ProductDto>> GetProducts(ProductDto product, UserDto userId)
        {
            return _mapper.Map<IEnumerable<ProductDto>>(await _productRepository.GetAll().Where(x => x.UserId == userId.Id).ToListAsync());
        }

        public async Task<ProductDto> Remove(int productId)
        {
            var product = await _productRepository.FindByIdAsync(productId);
            if (product == null)
                return null;
            _productRepository.Remove(product);
            await _productRepository.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }
    }
}
