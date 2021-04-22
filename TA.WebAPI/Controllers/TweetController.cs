using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TA.DAL;
using TA.Services.Interface;

namespace TA.WebApi.Controllers
{
    [Route("api/v1.0/tweets/")]
    [Authorize]
    public class TweetController : Controller
    {
        private ITweetService _tweetService;
        private IUserService _userService;

        public TweetController(ITweetService tweetService, IUserService userService)
        {
            _tweetService = tweetService;
            _userService = userService;
        }

        // Getting User's Tweets By User Id
        [HttpGet]
        [Route("{username}")]
        public JsonResult GetTweetsByUserName(string username)
        {
            List<TweetModel> tweetsByUserId = new List<TweetModel>();
            try
            {
                tweetsByUserId = _tweetService.GetAllTweetsOfUser(username);
                if (tweetsByUserId != null)
                {
                    for (int i = 0; i < tweetsByUserId.Count; i++)
                    {

                        List<TweetCommentsModel> tweetCommentsModels = new List<TweetCommentsModel>();
                        tweetCommentsModels = GetTweetCommentsById(tweetsByUserId[i].tweetId);
                        if (tweetCommentsModels.Count != 0)
                        {
                            if (tweetCommentsModels[0].tweetId == tweetsByUserId[i].tweetId)
                            {
                                tweetsByUserId[i].commentsCount = tweetCommentsModels.Count;
                            }
                        }
                        tweetCommentsModels = null;
                    }


                    var userTweets = (from t1 in tweetsByUserId
                               orderby t1.createdAt descending
                    select new { t1.tweetId , t1.userId, t1.tweetTag, t1.tweetDescription, t1.createdAt, t1.commentsCount, t1.likesCount }).ToList();
                    


                    return new JsonResult(userTweets);
                }
                return new JsonResult("Tweet not found");
            }
            catch (Exception ex)
            {
                string message = "Meesage : " + ex.Message + " & Stacktrace: " + ex.StackTrace;
            }
            return new JsonResult("Error");
        }

        [HttpGet]
        [Route("user/search/{username}")]
        public JsonResult SearchUserTweet(string username)
        {
            List<TweetModel> tweetsByUserId = new List<TweetModel>();
            List<UserModel> users = new List<UserModel>();
            UserModel user = new UserModel();

            try
            {
                tweetsByUserId = _tweetService.GetAllTweetsOfUser(username);

                users = _userService.GetAllUsers();

                if (tweetsByUserId != null)
                {
                    for (int i = 0; i < tweetsByUserId.Count; i++)
                    {

                        List<TweetCommentsModel> tweetCommentsModels = new List<TweetCommentsModel>();
                        tweetCommentsModels = GetTweetCommentsById(tweetsByUserId[i].tweetId);
                        if (tweetCommentsModels.Count != 0)
                        {
                            if (tweetCommentsModels[0].tweetId == tweetsByUserId[i].tweetId)
                            {
                                tweetsByUserId[i].commentsCount = tweetCommentsModels.Count;
                            }
                        }
                        tweetCommentsModels = null;
                    }


                    var usr = (from t1 in tweetsByUserId
                               orderby t1.createdAt descending
                               join t2 in users
                               on t1.userId equals t2.userId
                               select new { t1.tweetId, t1.userId, t1.tweetTag, t1.tweetDescription, t1.createdAt, t1.likesCount, t1.commentsCount, t2.first_name, t2.last_name }).ToList();
                    return new JsonResult(usr);
                }
                return new JsonResult("Tweet not found");
            }
            catch (Exception ex)
            {
                string message = "Meesage : " + ex.Message + " & Stacktrace: " + ex.StackTrace;
            }
            return new JsonResult("Error");
        }


        // Getting All Tweets
        [Route("all")]
        public JsonResult GetAllTweets()
        {
            List<TweetModel> allTweets = new List<TweetModel>();
            List<UserModel> allUsers = new List<UserModel>();

            try
            {
                allTweets = _tweetService.GetAllTweets();
                allUsers = _userService.GetAllUsers();

                for (int i = 0; i < allTweets.Count; i++)
                {

                    List<TweetCommentsModel> tweetCommentsModels = new List<TweetCommentsModel>();
                    tweetCommentsModels = GetTweetCommentsById(allTweets[i].tweetId);
                    if(tweetCommentsModels.Count != 0)
                    {
                        if (tweetCommentsModels[0].tweetId == allTweets[i].tweetId)
                        {
                            allTweets[i].commentsCount = tweetCommentsModels.Count;
                        }
                    }
                    tweetCommentsModels = null;
                }

                for (int i = 0; i < allTweets.Count; i++)
                {
                    List<TweetLikesModel> tweetLikesModel = new List<TweetLikesModel>();
                    tweetLikesModel = GetTweetLikesByTweetId(allTweets[i].tweetId);
                    if (tweetLikesModel.Count != 0)
                    {
                        if (tweetLikesModel[0].tweetId == allTweets[i].tweetId)
                        {
                            allTweets[i].likesCount = tweetLikesModel.Count;
                            allTweets[i].likeId = tweetLikesModel[0].likeId;
                        }
                    }
                    tweetLikesModel = null;
                }


                var usr = (from t1 in allTweets
                           orderby t1.createdAt descending
                           join t2 in allUsers
                           on t1.userId equals t2.userId
                           select new
                           {
                               t1.tweetId,
                               t1.userId,
                               t1.tweetDescription,
                               t1.tweetTag,
                               t1.createdAt,
                               t2.first_name,
                               t2.last_name,
                               t1.commentsCount,
                               t1.likesCount,
                               t1.likeId
                           }).ToList();

                if (usr != null)
                {
                    return new JsonResult(usr);
                }
                return new JsonResult("Tweets not found");
            }
            catch (Exception ex)
            {
                string message = "Meesage : " + ex.Message + " & Stacktrace: " + ex.StackTrace;
            }
            return new JsonResult("Error");
        }

        [Route("latestTweet")]
        public JsonResult GetLatestTweets()
        {
            List<TweetModel> allTweets = new List<TweetModel>();

            List<UserModel> users = new List<UserModel>();
            try
            {
                allTweets = _tweetService.GetAllTweets();
                users = _userService.GetAllUsers();


                var usr = (from t1 in allTweets
                           orderby t1.createdAt descending
                           join t2 in users
                           on t1.userId equals t2.userId
                           select new { t1.tweetId, t1.userId, t1.tweetTag, t1.tweetDescription, t1.createdAt,
                           t2.first_name, t2.last_name }).FirstOrDefault();

                if (usr != null)
                {
                    return new JsonResult(usr);
                }
                return new JsonResult("Tweets not found");
            }
            catch (Exception ex)
            {
                string message = "Meesage : " + ex.Message + " & Stacktrace: " + ex.StackTrace;
            }
            return new JsonResult("Error");
        }

        [Route("tweetById/{tweetId}/userId/{userID}")]
        [HttpGet]
        public TweetModel GetTweetById(string tweetId, string userId)
        {
            TweetModel tweet = new TweetModel();

            try
            {
                    tweet = _tweetService.GetTweetById(tweetId);

                    List<TweetCommentsModel> tweetCommentsModels = new List<TweetCommentsModel>();
                    tweetCommentsModels = GetTweetCommentsById(tweetId);
                    if (tweetCommentsModels.Count != 0)
                    {
                        if (tweetCommentsModels[0].tweetId == tweetId)
                        {
                            tweet.commentsCount = tweetCommentsModels.Count;
                        }
                    }
                    tweetCommentsModels = null;
            
                    List<TweetLikesModel> tweetLikesModel = new List<TweetLikesModel>();
                  
                    tweetLikesModel = GetTweetLikesByTweetId(tweetId);
                    if (tweetLikesModel.Count != 0)
                    {
                        if (tweetLikesModel[0].tweetId == tweetId)
                        {
                            tweet.likesCount = tweetLikesModel.Count;
                        }
                    }
                    tweetLikesModel = null;

                    var tweetLike = _tweetService.GetLikeByTweetIdandUserID(tweetId, userId);
                    if (tweetLike != null)
                    {
                        tweet.likeId = tweetLike.likeId;
                    }

            }
            catch (Exception ex)
            {
                string message = "Meesage : " + ex.Message + " & Stacktrace: " + ex.StackTrace;
            }

            return tweet;
        }

        [Route("tweetCommentsById/{tweetId}")]
        [HttpGet]
        public List<TweetCommentsModel> GetTweetCommentsById(string tweetId)
        {
            List<TweetCommentsModel> tweetCommentsModels = new List<TweetCommentsModel>();
            try
            {
                tweetCommentsModels = _tweetService.GetTweetCommentsById(tweetId);
            }
            catch (Exception ex)
            {
                string message = "Meesage : " + ex.Message + " & Stacktrace: " + ex.StackTrace;
            }

            return tweetCommentsModels;
        }


        // Create a Tweet
        [Route("{username}/add")]
        [HttpPost]
        public JsonResult CreateTweet([FromBody] TweetModel tweet)
        {
            try
            {
                bool creationStatus = _tweetService.CreateTweet(tweet);
                if (creationStatus)
                {
                    return new JsonResult("Tweet created successfully");
                }
            }
            catch (Exception ex)
            {
                string message = "Meesage : " + ex.Message + " & Stacktrace: " + ex.StackTrace;
            }
            return new JsonResult("Error");
        }

        // Creatae a Tweet
        [Route("AddComment")]
        [HttpPost]
        public JsonResult ReplyTweet([FromBody] TweetCommentsModel addComment)
        {
            try
            {
                bool creationStatus = _tweetService.ReplyTweet(addComment);
                if (creationStatus)
                {
                    return new JsonResult("Tweet replied successfully");
                }
            }
            catch (Exception ex)
            {
                string message = "Meesage : " + ex.Message + " & Stacktrace: " + ex.StackTrace;
            }
            return new JsonResult("Error");
        }

        [HttpGet]
        [Route("GetTweetLikesByTweetId/{tweetId}")]
        public List<TweetLikesModel> GetTweetLikesByTweetId(string tweetId)
        {
            List<TweetLikesModel> tweetLikedModel = new List<TweetLikesModel>();
            try
            {
                tweetLikedModel = _tweetService.GetLikesByTweetId(tweetId);
            }
            catch (Exception ex)
            {
                string message = "Meesage : " + ex.Message + " & Stacktrace: " + ex.StackTrace;
            }
            return tweetLikedModel;
        }

        [Route("LikeTweet")]
        [HttpPost]
        public JsonResult TweetLikeUnlikeAction([FromBody] TweetLikesModel tweetLikesModel)
        {
            try
            {
                if(tweetLikesModel.liked == "like")
                {
                    var likeStatus = _tweetService.LikeTweet(tweetLikesModel);
                    if (likeStatus)
                    {
                        return new JsonResult("Tweet liked successfully");
                    }
                }
                else
                {
                    var unlikeStatus = _tweetService.UnLike(tweetLikesModel);
                    if (unlikeStatus)
                    {
                        return new JsonResult("Tweet unliked successfully");
                    }
                }

              
            }
            catch (Exception)
            {
                throw;
            }
            return new JsonResult("Tweet not liked successfully");
        }



    }
}

