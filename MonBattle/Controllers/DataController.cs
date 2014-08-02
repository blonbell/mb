using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MonBattle.Models;
using MonBattle.Data;
using System.Data;
using System.Data.SqlClient;
using MonBattle.Data.BattleMechanics;

namespace MonBattle.Controllers
{
    public class DataController
    {
        DataModel dataModel = new DataModel();

        public DataController()
        {

        }

        /// <summary>
        /// Insert a new user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="activationCode"></param>
        /// <returns></returns>
        public int? insertUser(UserObject user, string password, string activationCode)
        {
            int? userId = dataModel.insertUser(user, password, activationCode);

            return userId;
        }

        /// <summary>
        /// Updates the specified user's username
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool updateUsername(int userId, string username)
        {
            bool successfulUpdate = dataModel.updateUsername(userId, username);

            return successfulUpdate;
        }

        /// <summary>
        /// Validates user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool validateLogin(string email, string password)
        {
            return dataModel.validateLogin(email, password);
        }

        /// <summary>
        /// Checks if the email is already used
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool emailExists(string email)
        {
            return dataModel.emailExists(email);
        }

        /// <summary>
        /// Checks if the username is already used
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool usernameExists(string username)
        {
            return dataModel.usernameExists(username);
        }

        /// <summary>
        /// Checks if user already voted today
        /// </summary>
        /// <returns></returns>
        public bool checkIfVotedToday(int userId, int cardBattleId)
        {
            return dataModel.checkIfVotedToday(userId, cardBattleId);
        }

        /// <summary>
        /// Checks if the battle's winner can be announced
        /// </summary>
        /// <returns></returns>
        public bool checkIfBattleResultCanAnnounce(int cardBattleId)
        {
            return dataModel.checkIfBattleResultCanAnnounce(cardBattleId);
        }

        /// <summary>
        /// Checks if the battle date is already scheduled
        /// </summary>
        /// <returns></returns>
        public bool checkIfBattleDateAvailable(DateTime battleDate)
        {
            return dataModel.checkIfBattleDateAvailable(battleDate);
        }

        ///// <summary>
        ///// Get the user for the email specified
        ///// </summary>
        ///// <param name="email"></param>
        ///// <returns></returns>
        //public UserObject getUser(string email)
        //{
        //    SqlDataReader userResult = dataModel.getUser(email);

        //    UserObject user = null;

        //    if (userResult.Read())
        //    {
        //        int? userId = null;
        //        string Email = null;
        //        bool? active = null;
        //        DateTime? dateCreated = null;
        //        bool? isAdmin = null;
        //        int points = 0;

        //        if (!userResult.IsDBNull(userResult.GetOrdinal("UserID")))
        //        {
        //            userId = userResult.GetInt32(userResult.GetOrdinal("UserID"));
        //        }

        //        if (!userResult.IsDBNull(userResult.GetOrdinal("Email")))
        //        {
        //            Email = userResult.GetString(userResult.GetOrdinal("Email"));
        //        }

        //        if (!userResult.IsDBNull(userResult.GetOrdinal("Active")))
        //        {
        //            active = userResult.GetBoolean(userResult.GetOrdinal("Active"));
        //        }

        //        if (!userResult.IsDBNull(userResult.GetOrdinal("DateCreated")))
        //        {
        //            dateCreated = userResult.GetDateTime(userResult.GetOrdinal("DateCreated"));
        //        }

        //        if (!userResult.IsDBNull(userResult.GetOrdinal("IsAdmin")))
        //        {
        //            isAdmin = userResult.GetBoolean(userResult.GetOrdinal("IsAdmin"));
        //        }

        //        if (!userResult.IsDBNull(userResult.GetOrdinal("Points")))
        //        {
        //            points = userResult.GetInt32(userResult.GetOrdinal("Points"));
        //        }

        //        user = new UserObject(userId, Email, active, dateCreated, isAdmin, points);
        //    }
        //    userResult.Close();
        //    return user;
        //}


        /// <summary>
        /// Get the user for the email specified
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public UserObject getUser(string email)
        {
            SqlDataReader userResult = dataModel.getUser(email);

            UserObject user = null;

            if (userResult.Read())
            {
                int? userId = null;
                string Email = null;
                bool? active = null;
                DateTime? dateCreated = null;
                bool? isAdmin = null;
                int points = 0;
                int battleCounter = 0;
                string username = null;

                if (!userResult.IsDBNull(userResult.GetOrdinal("UserID")))
                {
                    userId = userResult.GetInt32(userResult.GetOrdinal("UserID"));
                }

                if (!userResult.IsDBNull(userResult.GetOrdinal("Email")))
                {
                    Email = userResult.GetString(userResult.GetOrdinal("Email"));
                }

                if (!userResult.IsDBNull(userResult.GetOrdinal("Username")))
                {
                    username = userResult.GetString(userResult.GetOrdinal("Username"));
                }

                if (!userResult.IsDBNull(userResult.GetOrdinal("Active")))
                {
                    active = userResult.GetBoolean(userResult.GetOrdinal("Active"));
                }

                if (!userResult.IsDBNull(userResult.GetOrdinal("DateCreated")))
                {
                    dateCreated = userResult.GetDateTime(userResult.GetOrdinal("DateCreated"));
                }

                if (!userResult.IsDBNull(userResult.GetOrdinal("IsAdmin")))
                {
                    isAdmin = userResult.GetBoolean(userResult.GetOrdinal("IsAdmin"));
                }

                if (!userResult.IsDBNull(userResult.GetOrdinal("Points")))
                {
                    points = userResult.GetInt32(userResult.GetOrdinal("Points"));
                }
                if (!userResult.IsDBNull(userResult.GetOrdinal("BattleCounter")))
                {
                    battleCounter = userResult.GetInt32(userResult.GetOrdinal("BattleCounter"));
                }

                user = new UserObject(userId, Email, active, dateCreated, isAdmin, points, battleCounter, username);
            }
            userResult.Close();

            attachUserCharacterObject(user);
            return user;
        }

        public void attachUserCharacterObject(UserObject user)
        {
            user.character = null;
            CharacterObject character = dataModel.getCharacterByUserId(user.userId.Value);
            user.character = character;
        }

        public int refreshUserBattleCount(int userId) {
            return dataModel.refreshUserBattleCount(userId);
        }

        /*
         * Better logic, create a SQL job that runs daily to refresh the battle counters.
         */
        public void updateBattleCounter()
        {
            dataModel.updateBattleCounter();
        }

        public void awardUser(int userId, int reward)
        {
            dataModel.awardUser(userId, reward);
        }

        public int addCharacter(int ownerId, String name, String imageUrl)
        {
            int charId = dataModel.addCharacter(ownerId, name, imageUrl);
            return charId;
        }

        public CharacterObject getCharacterByCharId(int charId)
        {
            return dataModel.getCharacterByCharId(charId);
        }

        public int startTrainCharacter(UserObject user, CharacterObject character, DateTime trainingTime, int trainingType, int cost)
        {
            int succ = dataModel.startTrainCharacter(user.userId.Value, character.charId, trainingTime, trainingType, cost);
            if (succ != 0)
            {
                //character.trainingFinishTime = trainingTime;
                //character.trainingType = (CharacterObject.trainingTypeEnum)trainingType;
                user.points -= cost;
            }
            return succ;
        }

        public int finishTrainCharacter(int charId, int effect)
        {
            return dataModel.finishTrainCharacter(charId, effect);
        }

        public int useBattleCounter(int userId)
        {
            return dataModel.useBattleCounter(userId);
        }

        public CharacterObject[] getOpponentsList(int rowsize)
        {
            return dataModel.getOpponentsList(rowsize);
        }

        public void addNeutralCharacter(String name, String atk, String hp, String spd, String rew, String url)
        {
            dataModel.addNeutralCharacter(name, atk, hp, spd, rew, url);
        }

        /// <summary>
        /// Gets list of cards
        /// </summary>
        /// <returns></returns>
        public DataTable getCards()
        {
            DataTable cardsList = dataModel.getCards();

            return cardsList;
        }

        /// <summary>
        /// Get the card for the id specified
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public CardObject getCard(int cardId)
        {
            SqlDataReader cardResult = dataModel.getCard(cardId);

            CardObject card = null;

            if (cardResult.Read())
            {
                int? CardId = null;
                string name = null;
                string description = null;
                string imageURL = null;
                DateTime? dateCreated = null;

                if (!cardResult.IsDBNull(cardResult.GetOrdinal("CardID")))
                {
                    CardId = cardResult.GetInt32(cardResult.GetOrdinal("CardID"));
                }

                if (!cardResult.IsDBNull(cardResult.GetOrdinal("Name")))
                {
                    name = cardResult.GetString(cardResult.GetOrdinal("Name"));
                }

                if (!cardResult.IsDBNull(cardResult.GetOrdinal("Description")))
                {
                    description = cardResult.GetString(cardResult.GetOrdinal("Description"));
                }

                if (!cardResult.IsDBNull(cardResult.GetOrdinal("ImageURL")))
                {
                    imageURL = cardResult.GetString(cardResult.GetOrdinal("ImageURL"));
                }

                if (!cardResult.IsDBNull(cardResult.GetOrdinal("DateCreated")))
                {
                    dateCreated = cardResult.GetDateTime(cardResult.GetOrdinal("DateCreated"));
                }

                card = new CardObject(CardId, name, description, imageURL, dateCreated);
            }
            cardResult.Close();
            return card;
        }

        /// <summary>
        /// Insert a new card
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public int? insertCard(CardObject card)
        {
            int? cardId = dataModel.insertCard(card);

            return cardId;
        }

        /// <summary>
        /// Update a card
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public bool? updateCard(CardObject card)
        {
            bool? successfulUpdate = dataModel.updateCard(card);

            return successfulUpdate;
        }

        public Dictionary<string, List<CardVoterObject>> getCardBattleVoters(int cardBattleId) {
            return dataModel.getCardBattleVoters(cardBattleId);
        }

        /// <summary>
        /// Gets list of card battles
        /// </summary>
        /// <returns></returns>
        public DataTable getCardBattles()
        {
            DataTable cardBattlesList = dataModel.getCardBattles();

            return cardBattlesList;
        }

        /// <summary>
        /// Gets list of past card battles
        /// </summary>
        /// <returns></returns>
        public DataTable getPastCardBattles(int? userId)
        {
            DataTable cardBattlesList = dataModel.getPastCardBattles(userId);

            return cardBattlesList;
        }

        /// <summary>
        /// Get the card battle for the id specified
        /// </summary>
        /// <param name="cardBattleId"></param>
        /// <returns></returns>
        public CardBattleObject getCardBattle(int cardBattleId)
        {
            SqlDataReader cardBattleResult = dataModel.getCardBattle(cardBattleId);

            CardBattleObject cardBattle = getCardBattleHelper(cardBattleResult);

            return cardBattle;
        }

        public List<CardVoterCounterObject> getVoteWinsAfterDate(String date) {
            return dataModel.getVoteWinsAfterDate(date);
        }

        /// <summary>
        /// Get the card battle for today
        /// </summary>
        /// <param name="cardBattleId"></param>
        /// <returns></returns>
        public CardBattleObject getCardBattleToday()
        {
            SqlDataReader cardBattleResult = dataModel.getCardBattleToday();

            CardBattleObject cardBattle = getCardBattleHelper(cardBattleResult);

            return cardBattle;
        }

        /// <summary>
        /// Get the card battle for tomorrow
        /// </summary>
        /// <param name="cardBattleId"></param>
        /// <returns></returns>
        public CardBattleObject getCardBattleTomorrow()
        {
            SqlDataReader cardBattleResult = dataModel.getCardBattleTomorrow();

            CardBattleObject cardBattle = getCardBattleHelper(cardBattleResult);

            return cardBattle;
        }

        private CardBattleObject getCardBattleHelper(SqlDataReader cardBattleResult)
        {
            CardBattleObject cardBattle = null;
            CardObject cardOne = null;
            CardObject cardTwo = null;

            if (cardBattleResult.Read())
            {
                int? CardBattleId = null;
                int? winnerCardId = null;
                DateTime? battleDate = null;
                int? cardOneVotes = null;
                int? cardTwoVotes = null;

                if (!cardBattleResult.IsDBNull(cardBattleResult.GetOrdinal("CardBattleID")))
                {
                    CardBattleId = cardBattleResult.GetInt32(cardBattleResult.GetOrdinal("CardBattleID"));
                }

                if (!cardBattleResult.IsDBNull(cardBattleResult.GetOrdinal("CardOneID")))
                {
                    int cardOneId = cardBattleResult.GetInt32(cardBattleResult.GetOrdinal("CardOneID"));
                    cardOne = getCard(cardOneId);
                }

                if (!cardBattleResult.IsDBNull(cardBattleResult.GetOrdinal("CardTwoID")))
                {
                    int cardTwoId = cardBattleResult.GetInt32(cardBattleResult.GetOrdinal("CardTwoID"));
                    cardTwo = getCard(cardTwoId);
                }

                if (!cardBattleResult.IsDBNull(cardBattleResult.GetOrdinal("WinnerCardID")))
                {
                    winnerCardId = cardBattleResult.GetInt32(cardBattleResult.GetOrdinal("WinnerCardID"));
                }

                if (!cardBattleResult.IsDBNull(cardBattleResult.GetOrdinal("BattleDate")))
                {
                    battleDate = cardBattleResult.GetDateTime(cardBattleResult.GetOrdinal("BattleDate"));
                }

                if (!cardBattleResult.IsDBNull(cardBattleResult.GetOrdinal("CardOneVotes")))
                {
                    cardOneVotes = cardBattleResult.GetInt32(cardBattleResult.GetOrdinal("CardOneVotes"));
                }

                if (!cardBattleResult.IsDBNull(cardBattleResult.GetOrdinal("CardTwoVotes")))
                {
                    cardTwoVotes = cardBattleResult.GetInt32(cardBattleResult.GetOrdinal("CardTwoVotes"));
                }


                cardBattle = new CardBattleObject(CardBattleId, battleDate, cardOne, cardTwo, winnerCardId, cardOneVotes, cardTwoVotes);
            }
            cardBattleResult.Close();
            return cardBattle;
        }

        /// <summary>
        /// Insert a new card battle
        /// </summary>
        /// <param name="cardBattle"></param>
        /// <returns></returns>
        public int? insertCardBattle(CardBattleObject cardBattle)
        {
            int? cardBattleId = dataModel.insertCardBattle(cardBattle);

            return cardBattleId;
        }

        /// <summary>
        /// Update a card battle
        /// </summary>
        /// <param name="cardBattle"></param>
        /// <returns></returns>
        public bool? updateCardBattle(CardBattleObject cardBattle)
        {
            bool? successfulUpdate = dataModel.updateCardBattle(cardBattle);

            return successfulUpdate;
        }

        /// <summary>
        /// Inserts a card battle pick
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cardBattleId"></param>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public int? insertCardPick(int userId, int cardBattleId, int cardId)
        {
            int? cardPickId = dataModel.insertCardPick(userId, cardBattleId, cardId);

            return cardPickId;
        }

        /// <summary>
        /// Gets list of users
        /// </summary>
        /// <returns></returns>
        public DataTable getUsers()
        {
            DataTable usersList = dataModel.getUsers();

            return usersList;
        }

        /// <summary>
        /// Update a card battle's result
        /// </summary>
        /// <param name="cardBattleId"></param>
        /// <returns></returns>
        public bool? updateCardBattleVotesAndWinner(int cardBattleId)
        {
            bool? successfulUpdate = dataModel.updateCardBattleVotesAndWinner(cardBattleId);

            return successfulUpdate;
        }

        /// <summary>
        /// Update the points of users from battle result
        /// </summary>
        /// <param name="cardBattleId"></param>
        /// <returns></returns>
        public bool? updateUserPointsFromBattle(int cardBattleId, int winnerCardId)
        {
            bool? successfulUpdate = dataModel.updateUserPointsFromBattle(cardBattleId, winnerCardId);

            return successfulUpdate;
        }

        /// <summary>
        /// Gets banner
        /// </summary>
        /// <returns></returns>
        public DataTable getBanner()
        {
            DataTable bannerTable = dataModel.getBanner();

            return bannerTable;
        }

        /// <summary>
        /// Insert a new banner
        /// </summary>
        /// <returns></returns>
        public int? insertBanner(string fileName, string URL)
        {
            int? bannerId = dataModel.insertBanner(fileName, URL);

            return bannerId;
        }

        public void addMove(string Name, string Description, string Turns, string Linger, 
            string MeterCost, string CommandStr, string redeemCost, string imageUrl) {
            dataModel.addMove(Name, Description, Turns, Linger, MeterCost, CommandStr, redeemCost, imageUrl);
        }

        public List<Move> getMoveCatalog(){
            return dataModel.getMoveCatalog();
        }

        public void assignMove(int charId, string moveId) {
            dataModel.assignMove(charId, moveId);
        }
    }
}