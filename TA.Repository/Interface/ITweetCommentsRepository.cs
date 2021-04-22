using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TA.DAL;

namespace TA.Repository.Interface
{
    public interface ITweetCommentsRepository
    {
        List<TweetCommentsModel> FindAll();
        List<TweetCommentsModel> FindAllByCondition(string tweetId);
        TweetCommentsModel FindByCondition(Expression<Func<TweetCommentsModel, bool>> expression);
        bool Create(TweetCommentsModel tweetComment);
        bool Update(TweetCommentsModel tweetComment);
        bool Delete(string tweetCommentId);
    }
}
