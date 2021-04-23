using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TA.DAL;
using TA.Repository.Interface;
using TA.Services.Implementation;
using TA.Services.Interface;

namespace TA.Test
{
    [TestFixture]
    public class TweetServiceTest
    {
        private ITweetService _tweetService;
        private readonly IUserRepository _userRepository;
        private readonly ITweetRepository _tweetRepository;
        private readonly ITweetCommentsRepository _commentRepository;
        private readonly ITweetLikesRepository _likesRepository;

        [SetUp]
        public void Setup()
        {
            _tweetService = new TweetService(_tweetRepository, _userRepository, _commentRepository, _likesRepository);

        }

        [TearDown]
        public void TearDown()
        {
            _tweetService = null;
        }

        [Test]
        public void shouldPostTweet()
        {
            TweetModel tweetModel = new TweetModel();
            tweetModel.tweetDescription = "This is Test Tweet";
            tweetModel.tweetTag = "Test";

            var result= _tweetService.CreateTweet(tweetModel);

            Assert.IsTrue(result);
        }

        [Test]
        public void shouldGetAllTweets()
        {
            var result=_tweetService.GetAllTweets();
            Assert.IsTrue(result!=null);
        }

        [Test]
        public void  shouldGetAllComments()
        {
            var result = _tweetService.GetAllComments();

            Assert.That(result != null);
        }

        [Test]
        public void shouldGetTweetById(string tweetId)
        {
            var result = _tweetService.GetTweetById(tweetId);

            Assert.That(result.tweetId.Equals(tweetId));
        }

        [Test]
        public void shouldGetTweetCommentsById(string tweetId)
        {

            var result=_tweetService.GetTweetCommentsById(tweetId);

            Assert.That(result.Count != 0);
        }

        [Test]
        public void shouldGetAllTweetsOfUser(string loginId)
        {
            var result = _tweetService.GetAllTweetsOfUser(loginId);

            Assert.That(result.Count != 0);
        }

        [Test]
        public void shouldUpdateTweet()
        {
            TweetModel tweetModel = new TweetModel();
            tweetModel.tweetId = "";
            tweetModel.tweetDescription = "This is Test Tweet";
            tweetModel.tweetTag = "Test";

            var result = _tweetService.UpdateTweet(tweetModel);

            Assert.IsTrue(result);
        }

        [Test]
        public void shouldDeleteTweet(string tweetId)
        {
            var result = _tweetService.DeleteTweet(tweetId);

            Assert.IsTrue(result);
        }

        [Test]
        public void shouldGetAllLikes()
        {
            var result = _tweetService.GetAllLikes();
            Assert.That(result.Count != 0);
        }

        [Test]
        public void shouldGetLikesByTweetId(string tweetId)
        {
            var result = _tweetService.GetLikesByTweetId(tweetId);

            Assert.That(result.Count != 0);
        }

        [Test]
        public void shouldGetLikeByTweetIdandUserID(string tweetId, string userId)
        {
            var result = _tweetService.GetLikeByTweetIdandUserID(tweetId, userId);

            Assert.That(result.tweetId.Equals(tweetId) && result.userId.Equals(userId));
        }

        [Test]
        public void shouldLikeTweet()
        {
            TweetLikesModel tweetLikesModel = new TweetLikesModel();
            tweetLikesModel.tweetId = "";
            tweetLikesModel.userId = "";
            tweetLikesModel.username = "";

            var result = _tweetService.LikeTweet(tweetLikesModel);

            Assert.IsTrue(result);
        }

        [Test]
        public void shouldUnLike()
        {
            TweetLikesModel tweetLikesModel = new TweetLikesModel();
            tweetLikesModel.tweetId = "";
            tweetLikesModel.userId = "";
            tweetLikesModel.username = "";
            var result = _tweetService.UnLike(tweetLikesModel);

            Assert.IsTrue(result);
        }

        [Test]
        public void shouldReplyTweet(TweetCommentsModel comment)
        {
            TweetCommentsModel tweetCommentsModel = new TweetCommentsModel();
            tweetCommentsModel.tweetId = "";
            tweetCommentsModel.userId = "";

            var result = _tweetService.ReplyTweet(comment);

            Assert.IsTrue(result);
        }

    }
}
