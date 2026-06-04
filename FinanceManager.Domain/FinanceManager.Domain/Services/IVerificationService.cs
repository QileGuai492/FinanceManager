using FinanceManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Services
{
    /// <summary>二期实现：验证码登录，本期仅定义接口。</summary>
    public interface IVerificationService
    {
        Task SendVerificationCodeAsync(string target);
        Task<UserEntity> LoginWithCodeAsync(string target, string code);
    }
}
