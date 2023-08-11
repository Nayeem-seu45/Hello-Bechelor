using Bechelor.Core.Common;
using Bechelor.Core.Domin.Core;
using Bechelor.Infrastructure.Data;
using Bechelor.Services.Commons;
using Bechelor.Services.Expenses.Models;
using Bechelor.Services.MediaFiles;
using Bechelor.Services.MemberInfos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello_Bechelor.Controllers.Api
{
    [Produces("application/json")]
    //[Route("api/UploadFile")]
    [Route("api/[controller]")]
    [ApiController]

    public class UploadFileController : ControllerBase
    {
        private readonly ICommonService _commonService;
        private readonly IHostingEnvironment _env;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly BechelorContext _context;
        private readonly IMemberInfoService _memberInfo;
        private readonly IMediaFileRepository _mediaFileRepository;

     
        public UploadFileController(ICommonService commonService, IHostingEnvironment evn, UserManager<ApplicationUser> userManager, BechelorContext context, IMemberInfoService memberInfo, IMediaFileRepository mediaFileRepository)
        {
            _commonService = commonService;
            _context = context;
            _env = evn;
            _userManager = userManager;
            _memberInfo = memberInfo;
            _mediaFileRepository = mediaFileRepository;
        }

        [HttpPost]
        [Route("Upload")]
        [RequestSizeLimit(5000000)]
        public async Task<IActionResult> PostUploadFile([FromForm] List<IFormFile> files, int mediaFileId =0)
        {
            try
            {
                var fileName = await _commonService.UploadFile(files, _env);
                //try to update the user profile pict
                ApplicationUser appUser = await _userManager.GetUserAsync(User);
                MemberInfoResponse member = await _memberInfo.GetByIdAsync(appUser.MemberInformationId);
                MediaFile mediaFile = new();
                mediaFile.FileUrl = "/uploads/" + fileName;
                mediaFile.FileId = Convert.ToInt32(member.Id);
                if (member.ProfileImageId > 0)
                {
                    var dataList = await _mediaFileRepository.GetAllAsync();
                    var updadeMediaFile =  dataList.Where(x => x.FileId.Equals(mediaFile.FileId));
                    mediaFile.Id = updadeMediaFile.FirstOrDefault().Id;
                    await _mediaFileRepository.UpdateAsync(mediaFile);
                }
                else
                {
                    var filedata = await _mediaFileRepository.AddAsync(mediaFile);
                    if (filedata != null)
                    {
                        member.ProfileImageId = filedata.Id;
                        await _memberInfo.UpdateAsync(member);
                    }
                }
           
                return Ok(fileName);
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = ex.Message });
            }


        }
    }
}
