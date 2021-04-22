using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TA.DAL;
using TA.Repository.Interface;
using TA.Repository;

namespace TA.Repository.Implementation
{
    public class TweetLikesRepository : ITweetLikesRepository
    {
        private readonly IMongoCollection<TweetLikesModel> _tweetLikeData;

        public TweetLikesRepository(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _tweetLikeData = database.GetCollection<TweetLikesModel>("TweetLikesData");
        }

        public List<TweetLikesModel> FindAll()
        {
            return _tweetLikeData.Find(CommentModel => true).ToList(); ;
        }

        public List<TweetLikesModel> FindAllByCondition(Expression<Func<TweetLikesModel, bool>> expression)
        {
            return _tweetLikeData.Find(expression).ToList();
        }

        public TweetLikesModel FindByCondition(Expression<Func<TweetLikesModel, bool>> expression)
        {
            return _tweetLikeData.Find(expression).FirstOrDefault();
        }

        public bool Create(TweetLikesModel like)
        {
            bool isCreated = false;
            try
            {
                _tweetLikeData.InsertOne(like);
                isCreated = true;
            }
            catch (Exception)
            {
                throw;
            }
            return isCreated;
        }

        public bool Delete(TweetLikesModel unlike)
        {
            bool isDeleted = false;
            try
            {
                var data = _tweetLikeData.DeleteOne(x => x.likeId.Equals(unlike.likeId) & x.tweetId.Equals(unlike.tweetId) & x.userId.Equals(unlike.userId));
                isDeleted = true;
            }
            catch (Exception)
            {
                throw;
            }
            return isDeleted;
        }

        //public bool Update(TweetLikesModel tweetLike)
        //{
        //    bool isUpdated = false;
        //    try
        //    {
        //        _tweetCommentsData.ReplaceOne(x => x.likeId.Equals(tweetLike.likeId) & x.tweetId.Equals(tweetLike.tweetId) & x.userId.Equals(tweetLike.userId), tweetLike);
        //        isUpdated = true;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return isUpdated;
        //}

    }
}
