using System;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace TA.DAL
{
    public class TweetCommentsModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string commentId { get; set; }

        [Required(ErrorMessage = "Comment is required")]
        public string comment { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string tweetId { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string userId { get; set; }
        public string loginId { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime createdAt { get; set; }

    }
}
