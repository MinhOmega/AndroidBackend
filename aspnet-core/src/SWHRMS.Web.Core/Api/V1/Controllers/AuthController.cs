using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using Abp.UI;
using SWHRMS.Authentication.External;
using SWHRMS.Authentication.JwtBearer;
using SWHRMS.Authorization;
using SWHRMS.Authorization.Users;
using SWHRMS.MultiTenancy;
using SWHRMS.Controllers;
using System.Net.Http;
using Newtonsoft.Json;
using Abp.Runtime.Session;
using SWHRMS.Api.V1.Models.Auth;
using Newtonsoft.Json.Linq;
using Abp.Extensions;
using Microsoft.AspNetCore.Http;
using System.IO;
using SWHRMS.Identity;
using SWHRMS.Helpers;
using Microsoft.Web.Http;
using SWHRMS.Users;

namespace SWHRMS.Api.V1.Controllers
{
    [ApiVersion("1")]
    [Route("api/v1/[controller]/[action]")]
    public class AuthController : SWHRMSControllerBase
    {

        private static readonly HttpClient Client = new HttpClient();
        private readonly LogInManager _logInManager;
        private readonly ITenantCache _tenantCache;
        private readonly IUserApiService _userApiService;
        
        private readonly AbpLoginResultTypeHelper _abpLoginResultTypeHelper;
        private readonly TokenAuthConfiguration _configuration;
        private readonly IExternalAuthConfiguration _externalAuthConfiguration;
        private readonly IExternalAuthManager _externalAuthManager;
        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly TenantManager _tenantManager;
        private readonly UserManager _userManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAbpSession _abpSession;
        private readonly SignInManager _signInManager;

        public AuthController(
            LogInManager logInManager,
            UserManager userManager,
            SignInManager signInManager,
            ITenantCache tenantCache,
            IPasswordHasher<User> passwordHasher,
            AbpLoginResultTypeHelper abpLoginResultTypeHelper,
            TokenAuthConfiguration configuration,
            IExternalAuthConfiguration externalAuthConfiguration,
            IExternalAuthManager externalAuthManager,
            UserRegistrationManager userRegistrationManager,
            IAbpSession abpSession,
            IUserApiService userApiService,
            TenantManager tenantManager)
        {
            _logInManager = logInManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _tenantCache = tenantCache;
            _abpSession = abpSession;
            _userApiService = userApiService;
            _passwordHasher = passwordHasher;
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
            _configuration = configuration;
            _externalAuthConfiguration = externalAuthConfiguration;
            _externalAuthManager = externalAuthManager;
            _userRegistrationManager = userRegistrationManager;
            _tenantManager = tenantManager;
        }
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<LoginResultModel> Login([FromBody] LoginModel model)
        {
            var loginResult = await GetLoginResultAsync(
                model.Username,
                model.Password,
                GetTenancyNameOrNull()
            );
            //Updated DeviceToken
            loginResult.User.Devices = CheckDeviceIdAndUpdate(loginResult.User.Devices, model.DeviceId, model.DeviceToken);
            loginResult.User.Platform = SetPlatform(model.Platform);
            loginResult.User.LastLogin = DateTime.Now;
            CurrentUnitOfWork.SaveChanges();
            //Create access token
            var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));
            return new LoginResultModel
            {
                AccessToken = accessToken,
                EncryptedAccessToken = GetEncrpyedAccessToken(accessToken),
                ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds,
                UserId = loginResult.User.Id,
                TenantId = loginResult.User.TenantId
            };
        }

        /// <summary>
        /// Sign Current User out. Return success message.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<JsonResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Json(new { Msg = L("LogoutSuccessful") });
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<RegisterResultModel> Register([FromBody] RegisterModel model)
        {
            //TypeReg =  CRM
            //if (model.TypeReg != "accountKit")
            //{
            //    throw new UserFriendlyException(L("NotAccept"));
            //}
            if (!string.IsNullOrEmpty(model.PhoneNumber))
            {
                if (!CommonHelpers.IsValidPhoneNumber(model.PhoneNumber))
                {
                    throw new UserFriendlyException(L("InvalidPhoneNumer", model.PhoneNumber));
                }
            }
            var userCheckUserName = await _userManager.FindByNameAsync(model.UserName);
            if (userCheckUserName != null)
            {
                //If account Active
                if (userCheckUserName.IsActive)
                {
                    throw new UserFriendlyException(L("Identity.DuplicateUserName", model.UserName));
                }
            }
            //AccessToken of account kit
            //Check author code 
            //var authInfo = await GetFacebookToken(model.AccessTokenAKI);
            //if (authInfo == null)
            //{
            //    throw new UserFriendlyException(L("AccountKitTonkenInValidate"));
            //}
            //string accountKitId = authInfo.id;
            //string phoneAccountKit = "0" + authInfo.phone.national_number;

            ////Check AccountKitId          
            //if (model.AccountKitId != accountKitId || model.PhoneNumber != phoneAccountKit)
            //{
            //    throw new UserFriendlyException(L("AccountKitTonkenInValidate"));
            //}
            ////note : uncomment


            var user = await _userRegistrationManager.RegisterAsync(
                  model.FullName,
                  "",
                  model.EmailAddress,
                  model.UserName,
                  model.Password,
                  true // Assumed email address is always confirmed. Change this if you want to implement email confirmation.
              );
            // Getting tenant-specific settings       
            //Updated DeviceToken
            user.Devices = CheckDeviceIdAndUpdate(user.Devices, model.DeviceId, model.DeviceToken);
            user.Platform = SetPlatform(model.Platform);
            user.PhoneNumber = model.PhoneNumber;
            user.LastLogin = DateTime.Now;
            await CurrentUnitOfWork.SaveChangesAsync();

            // Directly login if possible
            var loginResult = await GetLoginResultAsync(
                model.UserName,
                model.Password,
                GetTenancyNameOrNull()
            );

            var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));
            return new RegisterResultModel
            {
                AccessToken = accessToken,
                EncryptedAccessToken = GetEncrpyedAccessToken(accessToken),
                ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds,
                UserId = user.Id
            };
        }

        // Disable show url in swagger
        //[ApiExplorerSettings(IgnoreApi = true)]
        /// <summary>
        /// Forgot Password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<ForgotPasswordResultModel> ForgotPassword([FromBody] ForgotPasswordModel model)
        {
            // FindbyPhone or username
            try
            {
                var user = await _userManager.FindByNameOrEmailAsync(
                    model.UserNameOrEmail
                );
                return new ForgotPasswordResultModel
                {
                    TokenVerify = await _userManager.GeneratePasswordResetTokenAsync(user)
                };
                //var authInfo = await GetFacebookToken(model.AccessTokenAKI);
                //if (authInfo == null)
                //{
                //    throw new UserFriendlyException(L("AccountKitTonkenInValidate"));
                //}
                //string accountKitId = authInfo.id;
                ////string phoneAccountKit = "0" + authInfo.name;

                ////Check AccountKitId          
                //if (model.AccountKitId != accountKitId)
                //{
                //    throw new UserFriendlyException(L("AccountKitTonkenInValidate"));
                //}

                //user.RequestPasswordHash = Guid.NewGuid().ToString();
                ////Expired 30 min.
                //var requestExpired = new DateTimeOffset(DateTime.UtcNow.AddMinutes(35)).ToUnixTimeSeconds();
                //user.RequestExpired = requestExpired.ToString();
                //await _userManager.UpdateAsync(user);
                //CurrentUnitOfWork.SaveChanges();
                //return new ForgotPasswordResultModel
                //{
                //    TokenVerify = user.RequestPasswordHash
                //};
            }
            catch
            {
                throw new UserFriendlyException(L("UserNotFound"));
            }
            //Check author code
        }
        /// <summary>
        /// Reset Password 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<JsonResult> ResetPassword([FromBody] ResetPasswordModel model)
        {

            var user = await _userManager.FindByIdAsync(model.UserId);
            var result = await _userManager.ResetPasswordAsync(user, model.TokenVerify, model.NewPassword);
            //// Check hash
            //if (user.RequestPasswordHash != model.TokenVerify)
            //{
            //    throw new UserFriendlyException(L("InvalidateToken"));
            //}
            //var now = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            //var expired = Int32.Parse(user.RequestExpired);
            //if (now - expired > 0)
            //{
            //    throw new UserFriendlyException(L("TokenExpired"));
            //}

            //if (user != null)
            //{
            //    //updated password
            //    user.Password = _passwordHasher.HashPassword(user, model.NewPassword);
            //    user.IsChangePassword = true;
            //    user.IsActive = true;
            //    //update hash
            //    user.RequestPasswordHash = Guid.NewGuid().ToString();
            //    CurrentUnitOfWork.SaveChanges();
            //}

            switch (result.Succeeded)
            {
                case true:
                    {
                        return Json(new { Msg = L("ResetPasswordSuccessful") });
                    }
                case false:
                    {
                        throw new UserFriendlyException(L("ResetPasswordFailed"));
                    }
                default:
                    return Json(new { Msg = L("ResetPasswordSuccessful") });
            }
        }

        /// <summary>
        /// /Social login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<RegisterResultModel> SocialLogin([FromBody]SocialAuthViewModel model)
        {

            var platform = SetPlatform(model.Platform);
            //Confirm access token facebook or google
            var socialInfo = await SocialAuthProviderApi(model);
            var socialId = socialInfo.SocialId;
            var email = socialInfo.Email;
            //Name of user
            var name = socialInfo.Name;

            if (socialId == "" || socialId == null)
            {
                throw new UserFriendlyException(L("CannotLogin"));
            }
            //Try login
            var loginResult = await _logInManager.LoginAsync(new UserLoginInfo(model.LoginType, socialId, model.LoginType), GetTenancyNameOrNull());

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    {
                        var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));
                        return new RegisterResultModel
                        {
                            AccessToken = accessToken,
                            EncryptedAccessToken = GetEncrpyedAccessToken(accessToken),
                            ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds,
                            UserId = loginResult.User.Id,
                            PhoneNumber = loginResult.User.PhoneNumber
                        };
                    }
                case AbpLoginResultType.UnknownExternalLogin:
                    {

                        //Check phone need update.
                        if (!CommonHelpers.IsValidPhoneNumber(model.PhoneNumber))
                        {
                            throw new UserFriendlyException(L("InvalidPhoneNumer"));
                        }
                        //Check phone verify AccountKit yet?
                        var authInfo = await GetFacebookToken(model.AccessTokenAKI);
                        if (authInfo == null)
                        {
                            throw new UserFriendlyException(2, L("AccountKitTonkenInValidate"));
                        }
                        string accountKitId = authInfo.id;
                        string phoneAccountKit = "0" + authInfo.phone.national_number;
                        //Check AccountKitId  And phone     
                        if (model.AccountKitId != accountKitId || model.PhoneNumber != phoneAccountKit)
                        {
                            throw new UserFriendlyException(2, L("AccountKitTonkenInValidate"));
                        }
                        //Check phone exits in system
                        var userCheckPhone = await _userApiService.FindByPhoneNumber(model.PhoneNumber);
                        if (userCheckPhone != null)
                        {
                            // Exits. => It's me.
                            userCheckPhone.Logins = new List<UserLogin>
                            {
                                new UserLogin
                                {
                                    LoginProvider = model.LoginType,
                                    ProviderKey = socialId,
                                    TenantId = userCheckPhone.TenantId
                                }
                            };
                            //Update login info
                            userCheckPhone.LastLogin = DateTime.Now;
                            userCheckPhone.Platform = platform;
                            //updated DeviceToken
                            userCheckPhone.Devices = CheckDeviceIdAndUpdate(userCheckPhone.Devices, model.DeviceId, model.DeviceToken);

                            await CurrentUnitOfWork.SaveChangesAsync();
                            //Login now


                        }
                        else
                        {
                            //Email exits..
                            var checkEmail = await _userManager.FindByNameOrEmailAsync(email);
                            if (checkEmail != null || string.IsNullOrEmpty(email))
                            {
                                email = SetDefaultEmail(socialId);
                                //email double
                            }

                            //Find Account by Phone in CRM


                            //Create new user => phone is Key.
                            var externalUser = new ExternalAuthUserInfo
                            {
                                Name = name,

                                EmailAddress = email,
                                PhoneNumber = model.PhoneNumber,
                                Surname = ".",
                                Provider = model.LoginType,
                                ProviderKey = socialId,
                                Platform = SetPlatform(model.Platform),
                                Devices = CheckDeviceIdAndUpdate("", model.DeviceId, model.DeviceToken)
                            };
                            await RegisterExternalUserAsync(externalUser);
                        }
                        //Update phone.
                        loginResult = await _logInManager.LoginAsync(new UserLoginInfo(model.LoginType, socialId, model.LoginType), GetTenancyNameOrNull());
                        if (loginResult.Result != AbpLoginResultType.Success)
                        {
                            throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(
                                loginResult.Result,
                                socialId,
                                GetTenancyNameOrNull()
                            );
                        }
                        var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));
                        return new RegisterResultModel
                        {
                            AccessToken = accessToken,
                            EncryptedAccessToken = GetEncrpyedAccessToken(accessToken),
                            ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds,
                            UserId = loginResult.User.Id,
                            PhoneNumber = loginResult.User.PhoneNumber
                        };

                    }
                default:
                    {
                        throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(
                            loginResult.Result,
                            socialId,
                            GetTenancyNameOrNull()
                        );
                    }
            }
        }
        /// <summary>
        /// Get information from facebook or google
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<SocialInfo> SocialAuthProviderApi(SocialAuthViewModel model)
        {

            var url = model.LoginType == "facebook" ? AppConsts.FacebookGetProfileUrl : AppConsts.GoogleGetProfileUrl;
            try
            {
                var userInfoResponse = await Client.GetStringAsync($"{url}{model.AccessToken}");
                var userInfo = JsonConvert.DeserializeObject<SocialUserData>(userInfoResponse);
                if (userInfo != null)
                {
                    return new SocialInfo
                    {
                        Email = string.IsNullOrEmpty(userInfo.Email) ? SetDefaultEmail(userInfo.Id) : userInfo.Email,
                        SocialId = userInfo.Id,
                        Name = userInfo.Name
                    };
                }
                else
                {
                    throw new UserFriendlyException(L("LoginSocialFailed", model.LoginType));
                }
            }
            catch
            {
                throw new UserFriendlyException(L("LoginSocialFailed", model.LoginType));
            }
        }
        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpAuthorize]
        [IgnoreAntiforgeryToken]
        public async Task<JsonResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException(L("InAuthention"));
            }
            if (model.ConfirmNewPassword != model.NewPassword)
            {
                throw new UserFriendlyException(L("PasswordNoMatch"));
            }
            var user = await _userManager.FindByIdAsync(_abpSession.UserId.Value.ToString());
            if (user == null)
            {
                throw new UserFriendlyException(L("UserNotFound"));
            }
            if (model.CurrentPassword.IsNullOrEmpty())
            {
                throw new UserFriendlyException(L("CurrentPasswordRequired"));
            }
            var loginResult = await _logInManager.LoginAsync(user.UserName, model.CurrentPassword, GetTenancyNameOrNull(), shouldLockout: false);
            if (loginResult.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException(L("PasswordDidNotMatch"));
            }
            //if (user.IsChangePassword)
            //{
            //    if (model.CurrentPassword.IsNullOrEmpty())
            //    {
            //        throw new UserFriendlyException(L("CurrentPasswordRequired"));
            //    }
            //    var loginResult = await _logInManager.LoginAsync(user.UserName, model.CurrentPassword, GetTenancyNameOrNull(), shouldLockout: false);
            //    if (loginResult.Result != AbpLoginResultType.Success)
            //    {
            //        throw new UserFriendlyException(L("PasswordDidNotMatch"));
            //    }
            //}

            //var tokenVerify = await _userManager.GeneratePasswordResetTokenAsync(user);
            var changePassword = await _userManager.ChangePasswordAsync(user, model.NewPassword);
            //user.Password = _passwordHasher.HashPassword(user, model.NewPassword);
            //user.IsChangePassword = true;
            CurrentUnitOfWork.SaveChanges();
            return Json(new { Msg = L("ChangePasswordSuccessful") });
        }


        /// <summary>
        /// Check phone duplicate
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<JsonResult> CheckDuplicatePhoneNumber([FromBody] PhoneNumberModel model)
        {
            if (!CommonHelpers.IsValidPhoneNumber(model.PhoneNumber))
            {
                throw new UserFriendlyException(L("InvalidPhoneNumer", model.PhoneNumber));
            }
            var accessVerify = AppConsts.HashSecret + model.PhoneNumber;
            var swSecureHash = CommonHelpers.CreateMD5(accessVerify);
            if (model.SecureHash.ToLower() != swSecureHash.ToLower())
            {
                throw new UserFriendlyException(L("InvalidateToken"));
            }
            var userCheckPhone = await _userApiService.FindByPhoneNumber(model.PhoneNumber);
            if (userCheckPhone != null)
            {
                //account active
                return Json(new { IsExist = true });
            }
            else
            {
                return Json(new { IsExist = false });
            }
        }

        /// <summary>
        /// Update phone number
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpAuthorize]
        [IgnoreAntiforgeryToken]
        public async Task<JsonResult> UpdatePhoneNumber([FromBody] UpdatePhoneNumberModel model)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException(L("PleaseLogin"));
            }
            long userId = _abpSession.UserId.Value;

            if (!CommonHelpers.IsValidPhoneNumber(model.PhoneNumber))
            {
                throw new UserFriendlyException(L("InvalidPhoneNumer", model.PhoneNumber));
            }
            //Check phone verify AccountKit yet?
            var authInfo = await GetFacebookToken(model.AccessTokenAKI);
            if (authInfo == null)
            {
                throw new UserFriendlyException(L("AccountKitTonkenInValidate"));
            }
            string accountKitId = authInfo.id;
            string phoneAccountKit = "0" + authInfo.phone.national_number;
            //Check AccountKitId  And phone     
            if (model.AccountKitId != accountKitId)
            {
                throw new UserFriendlyException(L("AccountKitTonkenInValidate"));
            }

            //Get current user
            var user = await _userManager.GetUserByIdAsync(userId);
            //Check phoneNumber exit
            var userCheckPhone = await _userApiService.FindByPhoneNumber(model.PhoneNumber);
            if (userCheckPhone != null)
            {
                //account active
                if (user.Id != userCheckPhone.Id)
                {
                    throw new UserFriendlyException(L("Identity.DuplicateUserName", model.PhoneNumber));
                }
            }
            //Check UserName exit
            var userCheckUserName = await _userManager.FindByNameAsync(model.PhoneNumber);
            if (userCheckUserName != null)
            {
                //account active
                if (user.Id != userCheckUserName.Id)
                {
                    throw new UserFriendlyException(L("Identity.DuplicateUserName", model.PhoneNumber));
                }
            }
            user.PhoneNumber = model.PhoneNumber;
            CheckErrors(await _userManager.UpdateAsync(user));
            return Json(new { Msg = L("UpdatedPhoneNumberSuccessful") });
        }
        /// <summary>
        /// update profile
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AbpAuthorize]
        [IgnoreAntiforgeryToken]
        public async Task<JsonResult> UpdateProfile([FromBody] UpdateProfileModel model)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException(L("InAuthention"));
            }
            long userId = _abpSession.UserId.Value;
            var user = await _userManager.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new UserFriendlyException(L("InAuthention"));
            }
            if (!model.EmailAddress.IsNullOrEmpty() && model.EmailAddress != user.EmailAddress)
            {
                var userCheckEmail = await _userManager.FindByEmailAsync(model.EmailAddress);
                if (userCheckEmail != null)
                {
                    throw new UserFriendlyException(L("Identity.DuplicateEmail", model.EmailAddress));
                }
                user.EmailAddress = model.EmailAddress;
            }
            user.Name = model.Name;
            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;
            CheckErrors(await _userManager.UpdateAsync(user));
            //Call CRM update profile

            return Json(new { Msg = L("UpdatedProfileSuccessful") });
        }

        /// <summary>
        /// 1: Male 2: Female 0:Unknow
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        private string SetGender(string gender)
        {
            if (gender == "1" || gender == "2")
            {
                return gender;
            }
            else
            {
                return "0";
            }
        }

        /// <summary>
        /// Set platform
        /// </summary>
        /// <param name="platform"></param>
        /// <returns></returns>
        private string SetPlatform(string platform)
        {
            if (platform == "ios" || platform == "android")
            {
                return platform;
            }
            else
            {
                return "web";
            }

        }
        /// <summary>
        /// Set email if email = null
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private string SetDefaultEmail(string key = "")
        {
            if (string.IsNullOrEmpty(key))
            {
                key = Guid.NewGuid().ToString("N").Truncate(10);
            }
            return $"tnfsw{key}@mydomain.com";
        }
        /// <summary>
        /// Set email => hide email by system create
        /// </summary>
        /// <returns></returns>
        private string GetDefaultEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                if (email.StartsWith("tnfsw"))
                {
                    email = "";
                }
                if (!email.Contains("@"))
                {
                    email = "";
                }
            }
            return email;
        }

        [HttpGet]
        [AbpAuthorize]
        public async Task<GetProfileModel> GetProfile()
        {

            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Unauthenticated");
            }
            try
            {
                long userId = _abpSession.UserId.Value;
                var user = await _userManager.GetUserByIdAsync(userId);

                var Profile = new GetProfileModel
                {
                    Name = user.Name,
                    IsChangePassword = user.IsChangePassword,
                   
                    PhoneNumber = user.PhoneNumber,
                    Fullname = user.FullName,
                    Address = user.Address,
                    EmailAddress = GetDefaultEmail(user.EmailAddress),
              

                };
                return Profile;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex.Message.ToString());
                throw new UserFriendlyException(ex.Message.ToString());
            }
        }
        /// <summary>
        /// Get user info from access_token facebook
        /// </summary>
        /// <param name="authorization_code"></param>
        /// <returns></returns>
        private async Task<dynamic> GetFacebookToken(string authorization_code)
        {
            var url = AppConsts.FacebookAccessTokenUrl + authorization_code;
            try
            {
                var response = await Client.GetAsync(url);
                string jsonString = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<object>(jsonString);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                // Handle exception.
                return null;
            }
        }


        /// <summary>
        /// Create user from account from social
        /// </summary>
        /// <param name="externalUser"></param>
        /// <returns></returns>
        private async Task<bool> RegisterExternalUserAsync(ExternalAuthUserInfo externalUser)
        {
            var user = await _userRegistrationManager.RegisterAsync(
                externalUser.Name,
                externalUser.Surname,
                externalUser.EmailAddress,
                externalUser.PhoneNumber,
                Authorization.Users.User.CreateRandomPassword(),
                true
            );
            user.PhoneNumber = externalUser.PhoneNumber;
            user.Platform = SetPlatform(externalUser.Platform);
            user.Devices = externalUser.Devices;
            user.Logins = new List<UserLogin>
            {
                new UserLogin
                {
                    LoginProvider = externalUser.Provider,
                    ProviderKey = externalUser.ProviderKey,
                    TenantId = user.TenantId
                }
            };
            await CurrentUnitOfWork.SaveChangesAsync();
            return true;
        }

        private async Task<ExternalAuthUserInfo> GetExternalUserInfo(ExternalAuthenticateModel model)
        {
            var userInfo = await _externalAuthManager.GetUserInfo(model.AuthProvider, model.ProviderAccessCode);
            if (userInfo.ProviderKey != model.ProviderKey)
            {
                throw new UserFriendlyException(L("CouldNotValidateExternalUser"));
            }

            return userInfo;
        }

        private string GetTenancyNameOrNull()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }

            return _tenantCache.GetOrNull(AbpSession.TenantId.Value)?.TenancyName;
        }

        private string GetTenancyNameOrDefault()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return "Default";
            }

            return _tenantCache.GetOrNull(AbpSession.TenantId.Value)?.TenancyName;
        }

        private async Task<Tenant> GetActiveTenantAsync(int tenantId)
        {
            var tenant = await _tenantManager.FindByIdAsync(tenantId);
            if (tenant == null)
            {
                throw new UserFriendlyException(L("UnknownTenantId{0}", tenantId));
            }

            if (!tenant.IsActive)
            {
                throw new UserFriendlyException(L("TenantIdIsNotActive{0}", tenantId));
            }

            return tenant;
        }

        private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
            }
        }

        private string CreateAccessToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
        {
            var now = DateTime.UtcNow;

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(expiration ?? _configuration.Expiration),
                signingCredentials: _configuration.SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        private static List<Claim> CreateJwtClaims(ClaimsIdentity identity)
        {
            var claims = identity.Claims.ToList();
            var nameIdClaim = claims.First(c => c.Type == ClaimTypes.NameIdentifier);

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, nameIdClaim.Value),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            });

            return claims;
        }

        private string GetEncrpyedAccessToken(string accessToken)
        {
            return SimpleStringCipher.Instance.Encrypt(accessToken, AppConsts.DefaultPassPhrase);
        }
   

       
        /// <summary>
        /// Update Devices for User
        ///Ex: var jsonString = "[{ \"deviceId\":\"xxxx\",\"deviceToken\":\"zxy\"},{\"deviceId\":\"xxxx1\",\"deviceToken\":\"zxy1\"}]";
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="deviceId"></param>
        /// <param name="deviceToken"></param>
        /// <returns></returns>
        private string CheckDeviceIdAndUpdate(string jsonString, string deviceId, string deviceToken)
        {
            if (deviceId == "" || deviceToken == "")
            {
                return jsonString;
            }

            try
            {
                dynamic dynJson = JsonConvert.DeserializeObject(jsonString);
                bool find = false;
                var jsonToOutput = "";
                foreach (var item in dynJson)
                {
                    if (item.deviceId == deviceId)
                    {
                        find = true;
                        item.deviceToken = deviceToken;
                    }
                }
                if (find)
                {
                    jsonToOutput = JsonConvert.SerializeObject(dynJson);
                }
                else
                {
                    var array = JArray.Parse(jsonString);
                    var itemToAdd = new JObject
                    {
                        ["deviceId"] = deviceId,
                        ["deviceToken"] = deviceToken
                    };
                    array.Add(itemToAdd);
                    jsonToOutput = JsonConvert.SerializeObject(array);
                }

                return jsonToOutput;
            }
            catch
            {
                var array = JArray.Parse("[]");
                var itemToAdd = new JObject
                {
                    ["deviceId"] = deviceId,
                    ["deviceToken"] = deviceToken
                };
                array.Add(itemToAdd);
                return JsonConvert.SerializeObject(array);

            }

        }

    }
}
