using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using SWHRMS.Positions.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SWHRMS.Positions
{
    public class PositionApiService: ApplicationService, IPositionApiService
    {
        private readonly IRepository<Position> _PositionRepository;

        public PositionApiService(
            IRepository<Position> repository)
        {
            _PositionRepository = repository;
        }

        /// <summary>
        /// Get All Positions
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<PositionDto>> GetAll()
        {
            var Positions = await _PositionRepository.GetAllListAsync();
            return new ListResultDto<PositionDto>(ObjectMapper.Map<List<PositionDto>>(Positions));
        }

        /// <summary>
        /// Get First Position by Id.
        /// </summary>
        /// <param name="PositionId"></param>
        /// <returns></returns>
        public async Task<PositionDto> Get(int? PositionId)
        {
            var Position = await _PositionRepository.FirstOrDefaultAsync(x => x.Id == PositionId);

            if (Position == null)
            {
                return null;
            }

            return ObjectMapper.Map<PositionDto>(Position);
        }

        protected void MapToEntity(PositionDto input, Position user)
        {
            ObjectMapper.Map(input, user);
        }

        protected PositionDto MapToEntityDto(Position Position)
        {
            //var branch = _branchRepository.GetAllList().FirstOrDefault(b => user.BranchID == b.Id);
            //var userDto = base.MapToEntityDto(user);
            var PositionDto = ObjectMapper.Map<PositionDto>(Position);
            //userDto.BranchName = (branch!= null? branch.Name: "");
            return PositionDto;
        }

        public async Task<PositionDto> Create(CreatePositionDto input)
        {
            //CheckCreatePermission();
            var Position = ObjectMapper.Map<Position>(input);
            await _PositionRepository.InsertAsync(Position);
            //CurrentUnitOfWork.SaveChanges();
            return MapToEntityDto(Position);
        }

        public async Task<PositionDto> Update(PositionDto input)
        {
            //CheckUpdatePermission();

            var Position = await _PositionRepository.GetAsync(input.Id);

            MapToEntity(input, Position);

            await _PositionRepository.UpdateAsync(Position);

            //return await Get(input);
            return ObjectMapper.Map<PositionDto>(Position);
        }

        public async Task Delete(EntityDto<int> input)
        {
            var Position = await _PositionRepository.GetAsync(input.Id);
            await _PositionRepository.DeleteAsync(Position);
        }

    }
}
