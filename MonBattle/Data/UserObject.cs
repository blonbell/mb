using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonBattle.Data
{
    public class UserObject
    {
        // Member variables
        public int? userId;
        public string username;
        public string email;
        public bool? active;
        public DateTime? dateCreated;
        public bool? isAdmin;
        public int points;
        public int battleCounter;

        public CharacterObject character { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public UserObject(){

        }

        /// <summary>
        /// Constructor to set all member variables
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Email"></param>
        /// <param name="Active"></param>
        /// <param name="DateCreated"></param>
        public UserObject(int? UserID, string Email, bool? Active, DateTime? DateCreated, 
            bool? IsAdmin, int Points, int battleCounter, string Username)
        {
            userId = UserID;
            email = Email;
            active = Active;
            dateCreated = DateCreated;
            isAdmin = IsAdmin;
            points = Points;
            this.battleCounter = battleCounter;
            username = Username;
        }

        public override string ToString() {
            String ch = "No Characters";
            if (character != null) {
                ch = character.ToString();
            }
            return "User: { " + userId + ", " + email + ", " + points + ", " +
                   ", " + battleCounter + ", " + ch + "}";
        }
    }
}