using AutoMapper;
using Microsoft.AspNetCore.Http;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class ProductServiceWithDto : ServiceWithDto<Product, ProductDTO>, IProductServiceWithDto
    {
        private readonly IProductRepository _repository;

        public ProductServiceWithDto(IGenericRepository<Product> genericRepository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository repository) : base(genericRepository, unitOfWork, mapper)
        {
            _repository = repository;
        }

        public async Task<CustomResponseDTO<ProductDTO>> AddAsync(ProductCreateDto productCreateDto)
        {
            var newEntity = _mapper.Map<Product>(productCreateDto);
            await _repository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();
            var newDto = _mapper.Map<ProductDTO>(newEntity);
            
            return CustomResponseDTO<ProductDTO>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<CustomResponseDTO<List<ProductWithCategoryDTO>>> GetProductsWithCategory()
        {
            var products = await _repository.GetProductsWithCategory();
            var productsDto = _mapper.Map<List<ProductWithCategoryDTO>>(products);

            return CustomResponseDTO<List<ProductWithCategoryDTO>>.Success(StatusCodes.Status200OK, productsDto);
        }

        public async Task<CustomResponseDTO<NoContentDTO>> UpdateAsync(ProductUpdateDTO productUpdateDto)
        {
            var entity = _mapper.Map<Product>(productUpdateDto);
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();

            return CustomResponseDTO<NoContentDTO>.Success(StatusCodes.Status204NoContent);
        }
    }
}
