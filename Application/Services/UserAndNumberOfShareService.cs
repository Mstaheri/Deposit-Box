using Application.UnitOfWork;
using Domain.Entity;
using Domain.IRepositories;
using Domain.Message;
using Domain.OperationResults;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserAndNumberOfShareService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAndNumberOfShareRepositorie _userAndNumberOfShareRepositorie;
        private readonly ILogger<UserAndNumberOfShareService> _Logger;
        public UserAndNumberOfShareService(IUnitOfWork unitOfWork,
            IUserAndNumberOfShareRepositorie userAndNumberOfShareRepositorie,
            ILogger<UserAndNumberOfShareService> Logger)

        {
            _unitOfWork = unitOfWork;
            _userAndNumberOfShareRepositorie = userAndNumberOfShareRepositorie;
            _Logger = Logger;
        }
        public async Task<OperationResult> AddAsync(UserAndNumberOfShare userAndNumberOfShare,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await _userAndNumberOfShareRepositorie.AddAsync(userAndNumberOfShare);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , userAndNumberOfShare.NameBankSafe.Value+","+ userAndNumberOfShare.UserName.Value
                        , nameof(AddAsync));
                _Logger.LogInformation(message);
                return new OperationResult(true, null);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new OperationResult(false, ex.Message);
            }
        }
        public async Task<OperationResult> UpdateAsync(UserAndNumberOfShare userAndNumberOfShare,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _userAndNumberOfShareRepositorie.GetNameBankAndUserNameAsync
                    (userAndNumberOfShare.NameBankSafe , userAndNumberOfShare.UserName);
                if (result != null)
                {
                    result.Update(userAndNumberOfShare.NumberOfShares);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    string message = string.Format(ConstMessages.Successfully
                        , userAndNumberOfShare.NameBankSafe.Value + "," + userAndNumberOfShare.UserName.Value
                        , nameof(UpdateAsync));
                    _Logger.LogInformation(message);
                    return new OperationResult(true, null);
                }
                else
                {
                    string message = string.Format(ConstMessages.NotFound
                        , userAndNumberOfShare.NameBankSafe.Value+ "," + userAndNumberOfShare.UserName.Value);
                    throw new Exception(message);
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new OperationResult(false, ex.Message);
            }
        }
        public async Task<OperationResult> DeleteAsync(string nameBankSafe, string userName ,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await _userAndNumberOfShareRepositorie.DeleteAsync(nameBankSafe, userName);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , nameBankSafe +","+ userName
                        , nameof(DeleteAsync));
                _Logger.LogInformation(message);
                return new OperationResult(true, null);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new OperationResult(false, ex.Message);
            }
        }
        public async Task<OperationResult<List<UserAndNumberOfShare>>> GetAllAsync()
        {
            try
            {
                var result = await _userAndNumberOfShareRepositorie.GetAllAsync();
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetAllAsync)
                        , "");
                _Logger.LogInformation(message);
                return new OperationResult<List<UserAndNumberOfShare>>(true, null, result);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new OperationResult<List<UserAndNumberOfShare>>(false, ex.Message, null);
            }
        }
        public async Task<OperationResult<UserAndNumberOfShare>> GetNameBankAsync(string nameBankSafe)
        {
            try
            {
                var result = await _userAndNumberOfShareRepositorie.GetNameBankAsync(nameBankSafe);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetNameBankAsync)
                        , "");
                _Logger.LogInformation(message);
                return new OperationResult<UserAndNumberOfShare>(true, null, result);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new OperationResult<UserAndNumberOfShare>(false, ex.Message, null);
            }
        }
        public async Task<OperationResult<UserAndNumberOfShare>> GetUserNameAsync(string nameBankSafe)
        {
            try
            {
                var result = await _userAndNumberOfShareRepositorie.GetUserNameAsync(nameBankSafe);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetUserNameAsync)
                        , "");
                _Logger.LogInformation(message);
                return new OperationResult<UserAndNumberOfShare>(true, null, result);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new OperationResult<UserAndNumberOfShare>(false, ex.Message, null);
            }
        }
        public async Task<OperationResult<UserAndNumberOfShare>> GetNameBankAndUserNameAsync
            (string nameBankSafe , string userName)
        {
            try
            {
                var result = await _userAndNumberOfShareRepositorie.GetNameBankAndUserNameAsync
                    (nameBankSafe , userName);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetNameBankAndUserNameAsync)
                        , "");
                _Logger.LogInformation(message);
                return new OperationResult<UserAndNumberOfShare>(true, null, result);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, ex.Message);
                return new OperationResult<UserAndNumberOfShare>(false, ex.Message, null);
            }
        }
    }

}
