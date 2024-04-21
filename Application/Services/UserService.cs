using Application.UnitOfWork;
using Domain.IRepositories;
using Domain.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Domain.Message;
using Domain.ValueObjects;
using Domain.Exceptions;

namespace Application.Services
{
    public class UserService
    {
        private readonly IUserRepositorie _userRepositorie;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;
        public UserService(IUserRepositorie userRepositorie ,
            IUnitOfWork unitOfWork 
            , ILogger<UserService> logger)
        {
            _userRepositorie = userRepositorie;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<OperationResult> AddAsync(User user ,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await _userRepositorie.AddAsync(user , cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                    , user.UserName.Value 
                    , nameof(AddAsync));
                _logger.LogInformation(message);
                return new OperationResult(true, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult(false, ex.Message);
            }
            
        }
        public async Task<OperationResult> UpdateAsync(User user,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _userRepositorie.GetAsync(user.UserName , cancellationToken);
                if (result != null)
                {
                    result.Update(user.FirstName, user.LastName, user.PhoneNumber
                     , user.NationalIDNumber, user.Password);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    string message = string.Format(ConstMessages.Successfully
                        , user.UserName.Value
                        , nameof(UpdateAsync));
                    _logger.LogInformation(message);
                    return new OperationResult(true, null);
                }
                else
                {
                    string message = string.Format(ConstMessages.NotFound, user.UserName.Value);
                    throw new Exception(message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult(false, ex.Message);
            }
        }
        public async Task<OperationResult> DeleteAsync(string UserName,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await _userRepositorie.DeleteAsync(UserName, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , UserName
                        , nameof(DeleteAsync));
                _logger.LogInformation(message);
                return new OperationResult(true, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult(false, ex.Message);
            }
        }
        public async Task<OperationResult<List<User>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _userRepositorie.GetAllAsync(cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetAllAsync)
                        , "");
                _logger.LogInformation(message);
                return new OperationResult<List<User>>(true, null, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult<List<User>>(false, ex.Message, null);
            }
            
        }
        public async Task<OperationResult<User>> GetAsync(string userName 
            , CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _userRepositorie.GetAsync(userName, cancellationToken);
                string message = string.Format(ConstMessages.Successfully
                        , nameof(GetAsync)
                        , "");
                _logger.LogInformation(message);
                return new OperationResult<User>(true, null, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult<User>(false, ex.Message, null);
            }

        }
    }
}
