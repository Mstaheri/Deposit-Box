using Domain.Entity;
using Domain.IRepositories;
using Microsoft.Extensions.Logging;
using Persistence;
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

                }
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                _logger.LogInformation($"{user.UserName} is Created Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

        }
    }
}
