using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TA.DAL;
using TA.Repository.Interface;
using TA.Repository;

namespace TA.Repository.Implementation
{ 
    public class UserRepository :IUserRepository
    {
       private readonly IMongoCollection<UserModel> _userData;
      
        public UserRepository(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _userData = database.GetCollection<UserModel>("UserData");
        }

        public List<UserModel> FindAll()
        {
           return _userData.Find(UserData => true).ToList(); 
        }
        public List<UserModel> FindAllByCondition(Expression<Func<UserModel, bool>> expression)
        {
            return _userData.Find(expression => true).ToList();
        }

        public UserModel FindByCondition(Expression<Func<UserModel, bool>> expression)
        {
            return _userData.Find(expression).FirstOrDefault();
        }


        public bool Create(UserModel user)
        {
            bool isCreated = false;
            try
            { 
                _userData.InsertOne(user);
                isCreated = true;
            }
            catch (Exception)
            {
                throw;
            }
            return isCreated;
        }

        public bool Update(UserModel user)
        {
            bool isUpdated = false;
            try
            {
                _userData.ReplaceOne(x => x.userId.Equals(user.userId), user);
                isUpdated = true;
            }
            catch (Exception)
            {
                throw;
            }
            return isUpdated;
        }

        public bool Delete(string userId)
        {
            bool isDeleted = false;
            try
            {
                _userData.DeleteOne(x => x.userId.Equals(userId));
                isDeleted = true;
            }
            catch (Exception)
            {
                throw;
            }
            return isDeleted;
        }

    }
}
