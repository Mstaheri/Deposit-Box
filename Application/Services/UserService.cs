using Application.IRepositories;
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
        public UserService(IUserRepositorie userRepositorie , IUnitOfWork unitOfWork 
            , ILogger<UserService> logger)
        {
            _userRepositorie = userRepositorie;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async void Add(User user , CancellationToken cancellationToken)
        {
            try
            {
                await _userRepositorie.AddAsync(user);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                _logger.LogInformation($"{user.UserName} is Created Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            
        }
        public async void Edit(User user, CancellationToken cancellationToken)
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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
        public async void Delete(string UserName, CancellationToken cancellationToken)
        {
            try
            {
                await _userRepositorie.DeleteAsync(UserName);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                _logger.LogInformation($"{UserName} is Delete Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
