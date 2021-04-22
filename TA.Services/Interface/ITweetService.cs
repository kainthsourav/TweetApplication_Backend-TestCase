using System.Collections.Generic;
using TA.DAL;

namespace TA.Services.Interface
{
   public interface ITweetService
    {
        List<TweetModel> GetAllTweets();
        List<TweetModel> GetAllTweetsOfUser(string username);
        List<TweetCommentsModel> GetAllComments();
        TweetModel GetTweetById(string tweetId);
        List<TweetCommentsModel> GetTweetCommentsById(string tweetId);

        bool CreateTweet(TweetModel tweet);
        bool UpdateTweet(TweetModel tweetModel);
        bool DeleteTweet(string tweetId);
        TweetLikesModel GetLikeByTweetIdandUserID(string tweetId, string userId);
        bool LikeTweet(TweetLikesModel tweetLikes);
        bool UnLike(TweetLikesModel tweetLike);

        List<TweetLikesModel> GetAllLikes();
        List<TweetLikesModel> GetLikesByTweetId(string tweetId);
        bool ReplyTweet(TweetCommentsModel tweetModel);
    }
}
