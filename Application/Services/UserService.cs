using Application.IRepositories;
using Application.Models;
using Domain.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<OperationResult> Add(User user ,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await _userRepositorie.AddAsync(user);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                _logger.LogInformation($"{user.UserName} is Created Successfully");
                return new OperationResult
                {
                    Success = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
            
        }
        public async Task<OperationResult> Edit(User user,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _userRepositorie.GetAsync(user.UserName);
                if (result != null)
                {
                    result.Update(user.FirstName, user.LastName, user.PhoneNumber
                     , user.NationalIDNumber, user.Email, user.Password, user.Roll);
                }
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                _logger.LogInformation($"{user.UserName} is Edit Successfully");
                return new OperationResult
                {
                    Success = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<OperationResult> Delete(string UserName,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await _userRepositorie.DeleteAsync(UserName);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                _logger.LogInformation($"{UserName} is Delete Successfully");
                return new OperationResult
                {
                    Success = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<OperationResult<List<User>>> GetAll()
        {
            try
            {
                var result = await _userRepositorie.GetAllAsync();
                _logger.LogInformation("GetAll is Successfully");
                return new OperationResult<List<User>>
                {
                    Success = true,
                    Result= result
                    
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new OperationResult<List<User>>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
            
        }
    }
}
