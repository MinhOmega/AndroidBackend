using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using SWHRMS.Authorization.Users.Meta_Info;
using SWHRMS.UserMetaInfos.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SWHRMS.UserMetaInfos
{
    public class UserMetaInfoApiService: ApplicationService, IUserMetaInfoApiService
    {
        private readonly IRepository<UserMetaInfo> _UserMetaInfoRepository;

        public UserMetaInfoApiService(
            IRepository<UserMetaInfo> repository)
        {
            _UserMetaInfoRepository = repository;
        }

        /// <summary>
        /// Get All UserMetaInfos
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<UserMetaInfoDto>> GetAll()
        {
            var UserMetaInfos = await _UserMetaInfoRepository.GetAllListAsync();
            return new ListResultDto<UserMetaInfoDto>(ObjectMapper.Map<List<UserMetaInfoDto>>(UserMetaInfos));
        }

        /// <summary>
        /// Get First UserMetaInfo by Id.
        /// </summary>
        /// <param name="UserMetaInfoId"></param>
        /// <returns></returns>
        public async Task<UserMetaInfoDto> Get(int? UserMetaInfoId)
        {
            var UserMetaInfo = await _UserMetaInfoRepository.FirstOrDefaultAsync(x => x.Id == UserMetaInfoId);

            if (UserMetaInfo == null)
            {
                return null;
            }

            return ObjectMapper.Map<UserMetaInfoDto>(UserMetaInfo);
        }
        
        protected void MapToEntity(UserMetaInfoDto input, UserMetaInfo user)
        {
            ObjectMapper.Map(input, user);
        }

        protected UserMetaInfoDto MapToEntityDto(UserMetaInfo UserMetaInfo)
        {
            //var branch = _branchRepository.GetAllList().FirstOrDefault(b => user.BranchID == b.Id);
            //var userDto = base.MapToEntityDto(user);
            var UserMetaInfoDto = ObjectMapper.Map<UserMetaInfoDto>(UserMetaInfo);
            //userDto.BranchName = (branch!= null? branch.Name: "");
            return UserMetaInfoDto;
        }

        public async Task<UserMetaInfoDto> Create(CreateUserMetaInfoDto input)
        {
            //CheckCreatePermission();
            var UserMetaInfo = ObjectMapper.Map<UserMetaInfo>(input);
            await _UserMetaInfoRepository.InsertAsync(UserMetaInfo);
            //CurrentUnitOfWork.SaveChanges();
            return MapToEntityDto(UserMetaInfo);
        }

        public async Task<UserMetaInfoDto> Update(UserMetaInfoDto input)
        {
            //CheckUpdatePermission();

            var UserMetaInfo = await _UserMetaInfoRepository.GetAsync(input.Id);

            MapToEntity(input, UserMetaInfo);

            await _UserMetaInfoRepository.UpdateAsync(UserMetaInfo);

            //return await Get(input);
            return ObjectMapper.Map<UserMetaInfoDto>(UserMetaInfo);
        }

        public async Task Delete(EntityDto<int> input)
        {
            var UserMetaInfo = await _UserMetaInfoRepository.GetAsync(input.Id);
            await _UserMetaInfoRepository.DeleteAsync(UserMetaInfo);
        }

    }
}
