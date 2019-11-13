using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using SWHRMS.Attendances.Dto;
using SWHRMS.Branches;
using SWHRMS.QRCodes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SWHRMS.Attendances
{
    public class AttendanceApiService: ApplicationService, IAttendanceApiService
    {
        private readonly IRepository<Attendance,long> _attendanceRepository;
        private readonly IRepository<Branch> _branchRepository;
        private readonly IRepository<QRCode, long> _QRCodeRepository;

        public AttendanceApiService(
            IRepository<Attendance,long> repository,
            IRepository<Branch> branchRepository,
            IRepository<QRCode, long> QRCoderepository)
        {
            _attendanceRepository = repository;
            _branchRepository = branchRepository;
            _QRCodeRepository = QRCoderepository;
        }

        /// <summary>
        /// Get All Attendances
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<AttendanceDto>> GetAll()
        {
            var Attendances = await _attendanceRepository.GetAllListAsync();
            return new ListResultDto<AttendanceDto>(ObjectMapper.Map<List<AttendanceDto>>(Attendances));
        }

        /// <summary>
        /// Get First Attendance by Id.
        /// </summary>
        /// <param name="AttendanceId"></param>
        /// <returns></returns>
        public async Task<AttendanceDto> Get(long? AttendanceId)
        {
            var Attendance = await _attendanceRepository.FirstOrDefaultAsync(x => x.Id == AttendanceId);

            if (Attendance == null)
            {
                return null;
            }

            return ObjectMapper.Map<AttendanceDto>(Attendance);
        }

        /// <summary>
        /// Get All Attendances
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<AttendanceDto>> GetAllByUserId(long? UserId)
        {
            var Attendances = await _attendanceRepository.GetAllListAsync(x => x.UserId == UserId);
            return new ListResultDto<AttendanceDto>(ObjectMapper.Map<List<AttendanceDto>>(Attendances));
        }

        /// <summary>
        /// Get Attendance by user Id.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<AttendanceDto> GetByUserId(long? UserId)
        {
            var Attendance = await _attendanceRepository.FirstOrDefaultAsync(x => x.UserId == UserId);

            if (Attendance == null)
            {
                return null;
            }

            return ObjectMapper.Map<AttendanceDto>(Attendance);
        }

        /// <summary>
        /// Get All Attendances
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<AttendanceDto>> GetAllByUserIdBetweenDates(long? UserId, DateTime dateFrom, DateTime dateTo)
        {
            var Attendances = await _attendanceRepository.GetAllListAsync(x => x.UserId == UserId && x.CreationTime >= dateFrom && x.CreationTime <= dateTo) ;
            return new ListResultDto<AttendanceDto>(ObjectMapper.Map<List<AttendanceDto>>(Attendances));
        }

        protected void MapToEntity(AttendanceDto input, Attendance user)
        {
            ObjectMapper.Map(input, user);
        }

        protected AttendanceDto MapToEntityDto(Attendance Attendance)
        {
            //var branch = _branchRepository.GetAllList().FirstOrDefault(b => user.BranchID == b.Id);
            //var userDto = base.MapToEntityDto(user);
            var AttendanceDto = ObjectMapper.Map<AttendanceDto>(Attendance);
            //userDto.BranchName = (branch!= null? branch.Name: "");
            return AttendanceDto;
        }

        public async Task<AttendanceDto> Create(CreateAttendanceDto input)
        {
            //CheckCreatePermission();
            var Attendance = ObjectMapper.Map<Attendance>(input);
            var attendanceId = await _attendanceRepository.InsertAndGetIdAsync(Attendance);
            Attendance = await _attendanceRepository.GetAsync(attendanceId);
            //CurrentUnitOfWork.SaveChanges();
            return MapToEntityDto(Attendance);
        }

        public async Task<AttendanceDto> CreateScan(CreateScan input)
        {
            var qrResult = new QRCode();
            try
            {
                qrResult = await _QRCodeRepository.SingleAsync(x => x.CodeString == input.CreatorQRCode);
                var queryType = qrResult.Type.Substring(qrResult.Type.LastIndexOf('_') + 1);
                var branch = await _branchRepository.SingleAsync(x => x.Name == queryType);
                var Attendance = ObjectMapper.Map<Attendance>(input);
                if (qrResult.Type.StartsWith("PunchIn"))
                {
                    Attendance.Type = 0;
                }
                else if (qrResult.Type.StartsWith("PunchOut"))
                {
                    Attendance.Type = 1;
                }
                Attendance.BranchId = branch.Id;

                var attendanceId = await _attendanceRepository.InsertAndGetIdAsync(Attendance);
                Attendance = await _attendanceRepository.GetAsync(attendanceId);
                //CurrentUnitOfWork.SaveChanges();
                return MapToEntityDto(Attendance);
            }
            catch
            {
                throw new UserFriendlyException("NoQRCodeOrBranchFound");
            }
        }

        public async Task<AttendanceDto> Update(AttendanceDto input)
        {
            //CheckUpdatePermission();

            var Attendance = await _attendanceRepository.GetAsync(input.Id);

            MapToEntity(input, Attendance);

            await _attendanceRepository.UpdateAsync(Attendance);

            //return await Get(input);
            return ObjectMapper.Map<AttendanceDto>(Attendance);
        }

        public async Task Delete(EntityDto<long> input)
        {
            var Attendance = await _attendanceRepository.GetAsync(input.Id);
            await _attendanceRepository.DeleteAsync(Attendance);
        }

    }
}
