using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonBattle.Data {
    public class CardVoterObject {
        public int userid { get; set; }
        public string username { get; set; }
        public string email { get; set; }

        public CardVoterObject(int userid, string username, string email) {
            this.userid = userid;
            this.username = username;
            this.email = email;
        }
    }
}