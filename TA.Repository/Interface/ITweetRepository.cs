using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TA.DAL;

namespace TA.Repository.Interface
{
   public interface ITweetRepository
    {
        List<TweetModel> FindAll();
        TweetModel FindByCondition(string tweetId);
        List<TweetModel> FindAllByCondition(Expression<Func<TweetModel,bool>> expression);
        bool Create(TweetModel tweet);
        bool Update(TweetModel tweet);
        bool Delete(string tweetId);
    }
}
