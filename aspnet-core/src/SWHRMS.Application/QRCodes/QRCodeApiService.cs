using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using SWHRMS.Authorization.Users.Meta_Info;
using SWHRMS.QRCodes.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SWHRMS.QRCodes
{
    public class QRCodeApiService: ApplicationService, IQRCodeApiService
    {
        private readonly IRepository<QRCode,long> _QRCodeRepository;

        public QRCodeApiService(
            IRepository<QRCode,long> repository)
        {
            _QRCodeRepository = repository;
        }

        /// <summary>
        /// Get All QRCodes
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<QRCodeDto>> GetAll()
        {
            var QRCodes = await _QRCodeRepository.GetAllListAsync();
            return new ListResultDto<QRCodeDto>(ObjectMapper.Map<List<QRCodeDto>>(QRCodes));
        }

        /// <summary>
        /// Get First QRCode by Id.
        /// </summary>
        /// <param name="QRCodeId"></param>
        /// <returns></returns>
        public async Task<QRCodeDto> Get(long? QRCodeId)
        {
            var QRCode = await _QRCodeRepository.FirstOrDefaultAsync(x => x.Id == QRCodeId);

            if (QRCode == null)
            {
                return null;
            }

            return ObjectMapper.Map<QRCodeDto>(QRCode);
        }

        protected void MapToEntity(QRCodeDto input, QRCode user)
        {
            ObjectMapper.Map(input, user);
        }

        protected QRCodeDto MapToEntityDto(QRCode QRCode)
        {
            //var branch = _branchRepository.GetAllList().FirstOrDefault(b => user.BranchID == b.Id);
            //var userDto = base.MapToEntityDto(user);
            var QRCodeDto = ObjectMapper.Map<QRCodeDto>(QRCode);
            //userDto.BranchName = (branch!= null? branch.Name: "");
            return QRCodeDto;
        }

        public async Task<QRCodeDto> Create(CreateQRCodeDto input)
        {
            //CheckCreatePermission();
            var QRCode = ObjectMapper.Map<QRCode>(input);
            await _QRCodeRepository.InsertAsync(QRCode);
            //CurrentUnitOfWork.SaveChanges();
            return MapToEntityDto(QRCode);
        }

        public async Task<QRCodeDto> Update(QRCodeDto input)
        {
            //CheckUpdatePermission();

            var QRCode = await _QRCodeRepository.GetAsync(input.Id);

            MapToEntity(input, QRCode);

            await _QRCodeRepository.UpdateAsync(QRCode);

            //return await Get(input);
            return ObjectMapper.Map<QRCodeDto>(QRCode);
        }

        public async Task Delete(EntityDto<int> input)
        {
            var QRCode = await _QRCodeRepository.GetAsync(input.Id);
            await _QRCodeRepository.DeleteAsync(QRCode);
        }

    }
}
