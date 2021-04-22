using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TA.DAL;
using TA.Repository.Interface;
using TA.Repository;

namespace TA.Repository.Implementation
{
 
    public class TweetCommentsRepository : ITweetCommentsRepository
    {
        private readonly IMongoCollection<TweetCommentsModel> _tweetCommentsData;

        public TweetCommentsRepository(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _tweetCommentsData = database.GetCollection<TweetCommentsModel>("TweetCommentsData");
        }
      
        public List<TweetCommentsModel> FindAll()
        {
            return _tweetCommentsData.Find(CommentModel => true).ToList(); ;
        }

        public List<TweetCommentsModel> FindAllByCondition(string tweetId)
        {
            return _tweetCommentsData.Find(x=> x.tweetId.Equals(tweetId)).ToList();
        }

        public TweetCommentsModel FindByCondition(Expression<Func<TweetCommentsModel, bool>> expression)
        {
            return _tweetCommentsData.Find(expression => true).FirstOrDefault();
        }

        public bool Create(TweetCommentsModel tweetComment)
        {
            bool isCreated = false;
            try
            {
                _tweetCommentsData.InsertOne(tweetComment);
                isCreated = true;
            }
            catch (Exception)
            {
                throw;
            }
            return isCreated;
        }
     
        public bool Update(TweetCommentsModel tweetComment)
        {
            bool isUpdated = false;
            try
            {
                _tweetCommentsData.ReplaceOne(x => x.commentId.Equals(tweetComment.commentId), tweetComment);
                isUpdated = true;
            }
            catch (Exception)
            {
                throw;
            }
            return isUpdated;
        }

        public bool Delete(string tweetCommentId)
        {
            bool isDeleted = false;
            try
            {
                _tweetCommentsData.DeleteOne(x => x.commentId.Equals(tweetCommentId));
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
