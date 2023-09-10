using NLayer.Core.DTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IProductServiceWithDto : IServiceWithDto<Product, ProductDTO>
    { 
        Task<CustomResponseDTO<List<ProductWithCategoryDTO>>> GetProductsWithCategory();
        //Overload AddAsync Method
        Task<CustomResponseDTO<ProductDTO>> AddAsync(ProductCreateDto productCreateDto);
        //Overload UpdateAsync Method
        Task<CustomResponseDTO<NoContentDTO>> UpdateAsync(ProductUpdateDTO productUpdateDto);
    }
}
