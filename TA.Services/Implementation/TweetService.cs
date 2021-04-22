using System;
using System.Collections.Generic;
using TA.DAL;
using TA.Repository.Interface;
using TA.Services.Interface;

namespace TA.Services.Implementation
{
    public class TweetService : ITweetService
    {

        private readonly IUserRepository _userRepository;
        private readonly ITweetRepository _tweetRepository;
        private readonly ITweetCommentsRepository _commentRepository;
        private readonly ITweetLikesRepository _likesRepository;
        public TweetService(ITweetRepository tweetRepository, IUserRepository userRepository,
            ITweetCommentsRepository commentRepository, ITweetLikesRepository likesRepository)
        {
            _tweetRepository = tweetRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
            _likesRepository = likesRepository;
        }
     

        public List<TweetModel> GetAllTweets()
        {
            return _tweetRepository.FindAll();
        }   
        
        public List<TweetCommentsModel> GetAllComments()
        {
            return _commentRepository.FindAll();
        }

        public TweetModel GetTweetById(string tweetId)
        {
            TweetModel tweetModel = new TweetModel();

            tweetModel = _tweetRepository.FindByCondition(tweetId);

            return tweetModel;
        }

        public List<TweetCommentsModel> GetTweetCommentsById(string tweetId)
        {
            List<TweetCommentsModel> tweetModels = new List<TweetCommentsModel>();
          
            tweetModels = _commentRepository.FindAllByCondition(tweetId);
            
            return tweetModels;
        }

        public List<TweetModel> GetAllTweetsOfUser(string loginId)
        {
            List<TweetModel> tweetModels = new List<TweetModel>();
            UserModel user = new UserModel();
            user=_userRepository.FindByCondition(x => x.loginId.Equals(loginId));
            if(user!=null)
            {
                tweetModels = _tweetRepository.FindAllByCondition(x => x.userId.Equals(user.userId));
            }
            return tweetModels;
        }

        public bool CreateTweet(TweetModel tweet)
        {
            bool isCreated = false;
            try
            {
                tweet.createdAt = DateTime.Now;
                isCreated = _tweetRepository.Create(tweet);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                isCreated = false;
            }
            return isCreated;
        }


        public bool UpdateTweet(TweetModel tweet)
        {
            bool isUpdated = false;
            try
            {
                tweet.updatedAt = DateTime.Now;
                isUpdated = _tweetRepository.Update(tweet);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                isUpdated = false;
            }
            return isUpdated;
        }

        public bool DeleteTweet(string tweetId)
        {
            bool isDeleted = false;
            try
            {
                isDeleted = _tweetRepository.Delete(tweetId);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                isDeleted = false;
            }
            return isDeleted;
        }

        public List<TweetLikesModel> GetAllLikes()
        {
            return _likesRepository.FindAll();
        }

        public List<TweetLikesModel> GetLikesByTweetId(string tweetId)
        {
            return _likesRepository.FindAllByCondition((x => x.tweetId.Equals(tweetId)));
        }

        public TweetLikesModel GetLikeByTweetIdandUserID(string tweetId, string userId)
        {
            return _likesRepository.FindByCondition((x => x.tweetId.Equals(tweetId) & x.userId.Equals(userId)));
        }


        public bool LikeTweet(TweetLikesModel tweetLike)
        {
            bool isLiked = false;
            try
            {
                tweetLike.createdAt = DateTime.Now;
                isLiked = _likesRepository.Create(tweetLike);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                isLiked = false;
            }
            return isLiked;
        }

        public bool UnLike(TweetLikesModel tweetLike)
        {
            bool isDeleted = false;
            try
            {
                isDeleted = _likesRepository.Delete(tweetLike);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                isDeleted = false;
            }
            return isDeleted;
        }

        public bool ReplyTweet(TweetCommentsModel comment)
        {
            bool isCreated = false;
            try
            {
                comment.createdAt = DateTime.Now;
                isCreated = _commentRepository.Create(comment);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                isCreated = false;
            }
            return isCreated;
        }
    }
}
