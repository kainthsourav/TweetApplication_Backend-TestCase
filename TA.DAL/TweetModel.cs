using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace TA.DAL
{
    public class TweetModel 
    {
      
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string tweetId { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string userId { get; set; }
        public string username { get; set; }
        public string tweetTag { get; set; }
        public string likeId { get; set; }

        [Required(ErrorMessage = "Tweet Description is required")]
        public string tweetDescription { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime createdAt { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime updatedAt { get; set; }
        public int commentsCount { get; set; }
        public int likesCount { get; set; }
    }
}
