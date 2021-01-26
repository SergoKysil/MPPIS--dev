using Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> GetById(int id, UserDto userId);

        Task<ProductDto> AddProduct(AddProductDto addProductDto);

        Task<IEnumerable<ProductDto>> GetProducts(ProductDto product, UserDto userId);

        Task<ProductDto> Remove(int productId);
    }
}
