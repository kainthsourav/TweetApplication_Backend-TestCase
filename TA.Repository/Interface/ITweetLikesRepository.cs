using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TA.DAL;

namespace TA.Repository.Interface
{
    public interface ITweetLikesRepository
    {
        List<TweetLikesModel> FindAll();
        List<TweetLikesModel> FindAllByCondition(Expression<Func<TweetLikesModel, bool>> expression);
        TweetLikesModel FindByCondition(Expression<Func<TweetLikesModel, bool>> expression);
        bool Create(TweetLikesModel tweetLike);
        bool Delete(TweetLikesModel unlike);

        //  bool Update(TweetLikesModel data);
    }
}
