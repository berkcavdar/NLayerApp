using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class ServiceWithDto<Entity, Dto> : IServiceWithDto<Entity, Dto> where Entity : BaseEntity where Dto : class
    {
        private readonly IGenericRepository<Entity> _genericRepository;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public ServiceWithDto(IGenericRepository<Entity> genericRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomResponseDTO<Dto>> AddAsync(Dto dto)
        {
            Entity newEntity = _mapper.Map<Entity>(dto);
            await _genericRepository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();
            var newDto = _mapper.Map<Dto>(newEntity);

            return CustomResponseDTO<Dto>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<CustomResponseDTO<IEnumerable<Dto>>> AddRangeAsync(IEnumerable<Dto> dtoList)
        {
            var newEntites = _mapper.Map<IEnumerable<Entity>>(dtoList);
            await _genericRepository.AddRangeAsync(newEntites);
            await _unitOfWork.CommitAsync();
            var newDtoList = _mapper.Map<IEnumerable<Dto>>(newEntites);

            return CustomResponseDTO<IEnumerable<Dto>>.Success(StatusCodes.Status200OK, newDtoList);
        }

        public async Task<CustomResponseDTO<bool>> AnyAsync(Expression<Func<Entity, bool>> expression)
        {
            var anyEntity = await _genericRepository.AnyAsync(expression);

            return CustomResponseDTO<bool>.Success(StatusCodes.Status200OK, anyEntity);
        }

        public async Task<CustomResponseDTO<IEnumerable<Dto>>> GetAllAsync()
        {
            var entities = await _genericRepository.GetAll().ToListAsync();
            var dtoList = _mapper.Map<IEnumerable<Dto>>(entities);

            return CustomResponseDTO<IEnumerable<Dto>>.Success(StatusCodes.Status200OK, dtoList);
        }

        public async Task<CustomResponseDTO<Dto>> GetByIdAsync(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            var dto = _mapper.Map<Dto>(entity);

            return CustomResponseDTO<Dto>.Success(StatusCodes.Status200OK, dto);
        }   

        public async Task<CustomResponseDTO<NoContentDTO>> RemoveAsync(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            _genericRepository.Remove(entity);
            await _unitOfWork.CommitAsync();

            return CustomResponseDTO<NoContentDTO>.Success(StatusCodes.Status204NoContent); 
        }

        public async Task<CustomResponseDTO<NoContentDTO>> RemoveRangeAsync(IEnumerable<int> ids)
        {
            var entities = await _genericRepository.Where(x => ids.Contains(x.Id)).ToListAsync();
            _genericRepository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();

            return CustomResponseDTO<NoContentDTO>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDTO<NoContentDTO>> UpdateAsync(Dto dto)
        {
            var entity = _mapper.Map<Entity>(dto);
            _genericRepository.Update(entity);
            await _unitOfWork.CommitAsync();

            return CustomResponseDTO<NoContentDTO>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDTO<IEnumerable<Dto>>> Where(Expression<Func<Entity, bool>> expression)
        {
            var entities = await _genericRepository.Where(expression).ToListAsync();
            var dtoList = _mapper.Map<IEnumerable<Dto>>(entities);

            return CustomResponseDTO<IEnumerable<Dto>>.Success(StatusCodes.Status200OK, dtoList);
        }
    }
}
