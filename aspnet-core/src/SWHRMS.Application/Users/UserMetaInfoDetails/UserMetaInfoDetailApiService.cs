using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using SWHRMS.Authorization.Users.Meta_Info;
using SWHRMS.UserMetaInfoDetails.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SWHRMS.UserMetaInfoDetails
{
    public class UserMetaInfoDetailApiService: ApplicationService, IUserMetaInfoDetailApiService
    {
        private readonly IRepository<UserMetaInfoDetail> _UserMetaInfoDetailRepository;

        public UserMetaInfoDetailApiService(
            IRepository<UserMetaInfoDetail> repository)
        {
            _UserMetaInfoDetailRepository = repository;
        }

        /// <summary>
        /// Get All UserMetaInfoDetails
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<UserMetaInfoDetailDto>> GetAll()
        {
            var UserMetaInfoDetails = await _UserMetaInfoDetailRepository.GetAllListAsync();
            return new ListResultDto<UserMetaInfoDetailDto>(ObjectMapper.Map<List<UserMetaInfoDetailDto>>(UserMetaInfoDetails));
        }

        /// <summary>
        /// Get First UserMetaInfoDetail by Id.
        /// </summary>
        /// <param name="UserMetaInfoDetailId"></param>
        /// <returns></returns>
        public async Task<UserMetaInfoDetailDto> Get(int? UserMetaInfoDetailId)
        {
            var UserMetaInfoDetail = await _UserMetaInfoDetailRepository.FirstOrDefaultAsync(x => x.Id == UserMetaInfoDetailId);

            if (UserMetaInfoDetail == null)
            {
                return null;
            }

            return ObjectMapper.Map<UserMetaInfoDetailDto>(UserMetaInfoDetail);
        }

        /// <summary>
        /// Get All UserMetaInfoDetails
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<UserMetaInfoDetailDto>> GetAllByUserId(long? UserId)
        {
            var UserMetaInfoDetails = await _UserMetaInfoDetailRepository.GetAllListAsync(x => x.UserId == UserId);
            return new ListResultDto<UserMetaInfoDetailDto>(ObjectMapper.Map<List<UserMetaInfoDetailDto>>(UserMetaInfoDetails));
        }

        /// <summary>
        /// Get UserMetaInfoDetail by user Id.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<UserMetaInfoDetailDto> GetByUserId(long? UserId)
        {
            var UserMetaInfoDetail = await _UserMetaInfoDetailRepository.FirstOrDefaultAsync(x => x.UserId == UserId);

            if (UserMetaInfoDetail == null)
            {
                return null;
            }

            return ObjectMapper.Map<UserMetaInfoDetailDto>(UserMetaInfoDetail);
        }

        protected void MapToEntity(UserMetaInfoDetailDto input, UserMetaInfoDetail user)
        {
            ObjectMapper.Map(input, user);
        }

        protected UserMetaInfoDetailDto MapToEntityDto(UserMetaInfoDetail UserMetaInfoDetail)
        {
            //var branch = _branchRepository.GetAllList().FirstOrDefault(b => user.BranchID == b.Id);
            //var userDto = base.MapToEntityDto(user);
            var UserMetaInfoDetailDto = ObjectMapper.Map<UserMetaInfoDetailDto>(UserMetaInfoDetail);
            //userDto.BranchName = (branch!= null? branch.Name: "");
            return UserMetaInfoDetailDto;
        }

        public async Task<UserMetaInfoDetailDto> Create(CreateUserMetaInfoDetailDto input)
        {
            //CheckCreatePermission();
            var UserMetaInfoDetail = ObjectMapper.Map<UserMetaInfoDetail>(input);
            await _UserMetaInfoDetailRepository.InsertAsync(UserMetaInfoDetail);
            //CurrentUnitOfWork.SaveChanges();
            return MapToEntityDto(UserMetaInfoDetail);
        }

        public async Task<UserMetaInfoDetailDto> Update(UserMetaInfoDetailDto input)
        {
            //CheckUpdatePermission();

            var UserMetaInfoDetail = await _UserMetaInfoDetailRepository.GetAsync(input.Id);

            MapToEntity(input, UserMetaInfoDetail);

            await _UserMetaInfoDetailRepository.UpdateAsync(UserMetaInfoDetail);

            //return await Get(input);
            return ObjectMapper.Map<UserMetaInfoDetailDto>(UserMetaInfoDetail);
        }

        public async Task Delete(EntityDto<int> input)
        {
            var UserMetaInfoDetail = await _UserMetaInfoDetailRepository.GetAsync(input.Id);
            await _UserMetaInfoDetailRepository.DeleteAsync(UserMetaInfoDetail);
        }

    }
}
