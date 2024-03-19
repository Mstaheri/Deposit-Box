using Application.UnitOfWork;
using Domain.IRepositories;
using Application.Models;
using Domain.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

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
                await _userRepositorie.AddAsync(user);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                _logger.LogInformation($"{user.UserName} is Created Successfully");
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
                var result = await _userRepositorie.GetAsync(user.UserName);
                if (result != null)
                {
                    result.Update(user.FirstName, user.LastName, user.PhoneNumber
                     , user.NationalIDNumber, user.Email, user.Password, user.Roll);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    _logger.LogInformation($"{user.UserName} is Update Successfully");
                    return new OperationResult(true, null);
                }
                else
                {
                    return new OperationResult(false, "User Update was not successful");
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
                await _userRepositorie.DeleteAsync(UserName);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                _logger.LogInformation($"{UserName} is Delete Successfully");
                return new OperationResult(true, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult(false, ex.Message);
            }
        }
        public async Task<OperationResult<List<User>>> GetAllAsync()
        {
            try
            {
                var result = await _userRepositorie.GetAllAsync();
                _logger.LogInformation("GetAll is Successfully");
                return new OperationResult<List<User>>(true, null, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult<List<User>>(true, ex.Message, null);
            }
            
        }
        public async Task<OperationResult<User>> GetAsync(string userName)
        {
            try
            {
                var result = await _userRepositorie.GetAsync(userName);
                _logger.LogInformation("GetAll is Successfully");
                return new OperationResult<User>(true, null, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult<User>(true, ex.Message, null);
            }

        }
    }
}
