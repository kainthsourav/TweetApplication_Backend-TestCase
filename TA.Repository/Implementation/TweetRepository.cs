using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TA.DAL;
using TA.Repository.Interface;
using TA.Repository;

namespace TA.Repository.Implementation
{
    public class TweetRepository : ITweetRepository
    {
        private readonly IMongoCollection<TweetModel> _tweetData;

        public TweetRepository(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _tweetData = database.GetCollection<TweetModel>("TweetData");
        }
      

        public List<TweetModel> FindAll()
        {
           return _tweetData.Find(TweetModel => true).ToList(); ;
        }

        public List<TweetModel> FindAllByCondition(Expression<Func<TweetModel, bool>> expression)
        {
            return _tweetData.Find(expression).ToList();
        }

        public TweetModel FindByCondition(string tweetId)
        {
            List<TweetModel> list = new List<TweetModel>();
            list = _tweetData.Find(x=> x.tweetId.Equals(tweetId)).ToList();
            return list[0];
        }


        public bool Create(TweetModel tweet)
        {
            bool isCreated = false;
            try
            {
                _tweetData.InsertOne(tweet);
                isCreated = true;
            }
            catch (Exception)
            {
                throw;
            }
            return isCreated;
        }

        public bool Update(TweetModel tweet)
        {
            bool isUpdated = false;
            try
            {
                _tweetData.ReplaceOne(x => x.tweetId .Equals(tweet.tweetId), tweet);
                isUpdated = true;
            }
            catch (Exception)
            {
                throw;
            }
            return isUpdated;
        }

        public bool Delete(string tweetId)
        {
            bool isDeleted = false;
            try
            {
                _tweetData.DeleteOne(x => x.tweetId.Equals(tweetId));
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
