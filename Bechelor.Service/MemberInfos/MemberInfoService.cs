using Bechelor.Core.Domin.Core;
using Bechelor.Infrastructure.Data;
using Bechelor.Services.Expenses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bechelor.Services.MemberInfos
{
    public class MemberInfoService : IMemberInfoService
    {
        private readonly IEntityRepository<MemberInformation> _memberInfoRepository;

        public MemberInfoService(IEntityRepository<MemberInformation> memberInfoRepository)
        {
            _memberInfoRepository = memberInfoRepository;
        }
       
        public async Task<MemberInformation> AddAsync(MemberInfoResponse model)
        {
            MemberInformation member = AddMappingProperties(ref model);
            return await _memberInfoRepository.AddAsync(member);
        }

        public async Task<IEnumerable<MemberInfoResponse>> GetAllAsync()
        {
            var memberlist = await _memberInfoRepository.GetAllAsync();
            List<MemberInfoResponse> memberInfos = new();
            foreach(var item in memberlist)
            {
                MemberInfoResponse member = new()
                {
                    Id = item.Id,
                    Gender = item.Gender,
                    JoiningDate = item.JoiningDate,
                    IsManager = item.IsManager,
                    PermanentAddress = item.PermanentAddress,
                    EmergencyContactNumber = item.EmergencyContactNumber,
                    NIDNumber = item.NIDNumber,
                    ProfileImageId = item.ProfileImageId
                };
                memberInfos.Add(member);

            }
            return memberInfos;
        }

        public async Task<MemberInfoResponse> GetByIdAsync(int id)
        {
           var member =  await _memberInfoRepository.GetByIdAsync(id);
            return GetMappingProperties(ref member);
        }

        public Task<bool> SoftDeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> TotalCountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(MemberInfoResponse model)
        {
            MemberInformation member = UpdateMappingProperties(ref model);

            return await _memberInfoRepository.UpdateAsync(member);
        }

        private MemberInformation AddMappingProperties(ref MemberInfoResponse model)
        {
            MemberInformation member = new()
            {
                Gender = model.Gender,
                Occupation = model.Occupation,
                JoiningDate = model.JoiningDate,
                PermanentAddress = model.PermanentAddress,
                IsActive = true,
                IsManager = false,
                CreatedOn = DateTime.Now
            };
            return member;
        }

        private MemberInfoResponse GetMappingProperties(ref MemberInformation member)
        {
            MemberInfoResponse model = new()
            {
                Id = member.Id,
                Occupation = member.Occupation,
                JoiningDate = member.JoiningDate,
                PermanentAddress = member.PermanentAddress,
                IsActive = member.IsActive,
                IsManager = member.IsManager,
                NIDNumber = member.NIDNumber,
                ProfileImageId = member.ProfileImageId,
                EmergencyContactNumber =  member.EmergencyContactNumber

            };

            return model;

        }

        private MemberInformation UpdateMappingProperties(ref MemberInfoResponse member)
        {
            MemberInformation memberdata = new ()
            {
                Id = member.Id,
                Occupation = member.Occupation,
                JoiningDate = member.JoiningDate,
                PermanentAddress = member.PermanentAddress,
                IsActive = member.IsActive,
                IsManager = member.IsManager,
                NIDNumber = member.NIDNumber,
                ProfileImageId = member.ProfileImageId,
                EmergencyContactNumber = member.EmergencyContactNumber
            };
            return memberdata;
        }
    }
}
