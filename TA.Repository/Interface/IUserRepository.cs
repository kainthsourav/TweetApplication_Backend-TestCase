using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TA.DAL;

namespace TA.Repository.Interface
{
    public interface IUserRepository
    {
        List<UserModel> FindAll();
        UserModel FindByCondition(Expression<Func<UserModel, bool>> expression);
        List<UserModel> FindAllByCondition(Expression<Func<UserModel,bool>> expression);
        bool Create(UserModel user);
        bool Update(UserModel user);
        bool Delete(string userId);
    }
}
