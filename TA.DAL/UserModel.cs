using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TA.DAL
{

    public class UserModel
    {

        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string userId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string first_name { get; set; }


        [Required(ErrorMessage = "Last Name is required")]
        public string last_name { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string email { get; set; }


        [Required(ErrorMessage = "LoginId is required")]
        public string loginId { get; set; }


        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }


        [Required(ErrorMessage = "Contact Number is required")]
        public string contactNumber { get; set; }

        public string token { get; set; }


        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime createdAt { get; set; }


        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime updatedAt { get; set; }

    }
}
