using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using MonBattle.Data;
using System.Data;
using MonBattle.Data.BattleMechanics;

namespace MonBattle.Models
{
    public class DataModel
    {
        private SqlConnection sqlConnection;
        private ConnectionStringSettings connectionString;

        public DataModel()
        {
            Configuration rootWebConfig = WebConfigurationManager.OpenWebConfiguration("~");

            connectionString = rootWebConfig.ConnectionStrings.ConnectionStrings["MonBattleConnectionString"];
        }

        protected void openSQLConnection()
        {
            try
            {
                sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = connectionString.ConnectionString;
                sqlConnection.Open();
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        protected void closeSQLConnection()
        {
            try
            {
                sqlConnection.Close();
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Inserts a new user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="activationCode"></param>
        /// <returns></returns>
        public int? insertUser(UserObject user, string password, string activationCode)
        {
            openSQLConnection();

            int? userId = null;

            using (SqlCommand cmd = new SqlCommand("monbattle.InsertUser", this.sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
                cmd.Parameters["@Email"].Value = user.email;

                cmd.Parameters.Add("@Username", SqlDbType.NVarChar);
                cmd.Parameters["@Username"].Value = user.username;

                cmd.Parameters.Add("@Password", SqlDbType.NVarChar);
                cmd.Parameters["@Password"].Value = password;

                cmd.Parameters.Add("@Active", SqlDbType.Bit);
                cmd.Parameters["@Active"].Value = user.active;

                cmd.Parameters.Add("@ActivationCode", SqlDbType.NVarChar);
                if (String.IsNullOrEmpty(activationCode))
                {
                    cmd.Parameters["@ActivationCode"].Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters["@ActivationCode"].Value = activationCode;
                }

                SqlParameter parameter = cmd.Parameters.Add("@UserID", SqlDbType.Int);
                parameter.Direction = ParameterDirection.Output;

                try
                {
                    cmd.ExecuteNonQuery();
                    userId = (int)cmd.Parameters["@UserID"].Value;
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    closeSQLConnection();
                }
            }
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
            openSQLConnection();

            bool successfulUpdate = false;

            using (SqlCommand cmd = new SqlCommand("monbattle.UpdateUsername", this.sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserID", SqlDbType.Int);
                cmd.Parameters["@UserID"].Value = userId;

                cmd.Parameters.Add("@Username", SqlDbType.NVarChar);
                cmd.Parameters["@Username"].Value = username;

                try
                {
                    cmd.ExecuteNonQuery();
                    successfulUpdate = true;
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    closeSQLConnection();
                }
            }
            return successfulUpdate;
        }

        /// <summary>
        /// Validates user login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool validateLogin(string email, string password)
        {
            openSQLConnection();

            using (SqlCommand cmd = new SqlCommand("monbattle.ValidateLogin", this.sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
                cmd.Parameters["@Email"].Value = email;

                cmd.Parameters.Add("@Password", SqlDbType.NVarChar);
                cmd.Parameters["@Password"].Value = password;

                SqlParameter parameter = cmd.Parameters.Add("@ValidLogin", SqlDbType.Bit);
                parameter.Direction = ParameterDirection.ReturnValue;

                try
                {
                    cmd.ExecuteNonQuery();
                    return (bool)cmd.Parameters["@ValidLogin"].Value;
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    closeSQLConnection();
                }
            }
        }

        /// <summary>
        /// Checks if the email is already used
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool emailExists(string email)
        {
            openSQLConnection();

            using (SqlCommand cmd = new SqlCommand("monbattle.EmailExists", this.sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
                cmd.Parameters["@Email"].Value = email;

                SqlParameter parameter = cmd.Parameters.Add("@EmailExists", SqlDbType.Bit);
                parameter.Direction = ParameterDirection.ReturnValue;

                try
                {
                    cmd.ExecuteNonQuery();
                    return (bool)cmd.Parameters["@EmailExists"].Value;
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    closeSQLConnection();
                }
            }
        }

        /// <summary>
        /// Checks if the username is already used
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool usernameExists(string username)
        {
            openSQLConnection();

            using (SqlCommand cmd = new SqlCommand("monbattle.UsernameExists", this.sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Username", SqlDbType.NVarChar);
                cmd.Parameters["@Username"].Value = username;

                SqlParameter parameter = cmd.Parameters.Add("@UsernameExists", SqlDbType.Bit);
                parameter.Direction = ParameterDirection.ReturnValue;

                try
                {
                    cmd.ExecuteNonQuery();
                    return (bool)cmd.Parameters["@UsernameExists"].Value;
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    closeSQLConnection();
                }
            }
        }

        /// <summary>
        /// Checks if the user already voted today
        /// </summary>
        /// <returns></returns>
        public bool checkIfVotedToday(int userId, int cardBattleId)
        {
            openSQLConnection();

            using (SqlCommand cmd = new SqlCommand("monbattle.CheckIfVotedToday", this.sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserID", SqlDbType.Int);
                cmd.Parameters["@UserID"].Value = userId;

                cmd.Parameters.Add("@CardBattleID", SqlDbType.Int);
                cmd.Parameters["@CardBattleID"].Value = cardBattleId;

                SqlParameter parameter = cmd.Parameters.Add("@VotedToday", SqlDbType.Bit);
                parameter.Direction = ParameterDirection.ReturnValue;

                try
                {
                    cmd.ExecuteNonQuery();
                    return (bool)cmd.Parameters["@VotedToday"].Value;
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    closeSQLConnection();
                }
            }
        }

        /// <summary>
        /// Checks if the battle's winner can be announced
        /// </summary>
        /// <returns></returns>
        public bool checkIfBattleResultCanAnnounce(int cardBattleId)
        {
            openSQLConnection();

            using (SqlCommand cmd = new SqlCommand("monbattle.CheckIfBattleResultCanAnnounce", this.sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@CardBattleID", SqlDbType.Int);
                cmd.Parameters["@CardBattleID"].Value = cardBattleId;

                SqlParameter parameter = cmd.Parameters.Add("@CanAnnounceBattleResult", SqlDbType.Bit);
                parameter.Direction = ParameterDirection.ReturnValue;

                try
                {
                    cmd.ExecuteNonQuery();
                    return (bool)cmd.Parameters["@CanAnnounceBattleResult"].Value;
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    closeSQLConnection();
                }
            }
        }

        /// <summary>
        /// Checks if the battle date is already scheduled
        /// </summary>
        /// <returns></returns>
        public bool checkIfBattleDateAvailable(DateTime battleDate)
        {
            openSQLConnection();

            using (SqlCommand cmd = new SqlCommand("monbattle.CheckIfBattleDateAvailable", this.sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@BattleDate", SqlDbType.DateTime);
                cmd.Parameters["@BattleDate"].Value = battleDate;

                SqlParameter parameter = cmd.Parameters.Add("@@BattleDateAvailable", SqlDbType.Bit);
                parameter.Direction = ParameterDirection.ReturnValue;

                try
                {
                    cmd.ExecuteNonQuery();
                    return (bool)cmd.Parameters["@@BattleDateAvailable"].Value;
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    closeSQLConnection();
                }
            }
        }

        /// <summary>
        /// Gets the user for the specified email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public SqlDataReader getUser(string email)
        {
            openSQLConnection();

            using (SqlCommand cmd = new SqlCommand("monbattle.GetUser", this.sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
                cmd.Parameters["@Email"].Value = email;

                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    return dr;
                }
                catch (SqlException e)
                {
                    throw e;
                }
            }
        }

        public int refreshUserBattleCount(int userId) {
            openSQLConnection();
            string query = "SELECT BattleCounter " +
                           "FROM monbattle.Users " +
                           "WHERE UserID = @UserID";
            using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
            {
                try
                {
                    cmd.Parameters.Add(new SqlParameter("UserID", userId));
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
                finally
                {
                    closeSQLConnection();
                }
            }
        }

        //For all users
        public void updateBattleCounter()
        {
            openSQLConnection();
            string query = "UPDATE monbattle.Users " +
                           "SET BattleCounter = 5 ";
            using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
            {
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
                finally
                {
                    closeSQLConnection();
                }
            }
        }

        public void awardUser(int userId, int reward)
        {
            openSQLConnection();
            string query = "UPDATE monbattle.Users " +
                           "SET points = points + @Reward " +
                           "WHERE UserId = @UserID";
            using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
            {
                try
                {
                    cmd.Parameters.Add(new SqlParameter("UserID", userId));
                    cmd.Parameters.Add(new SqlParameter("Reward", reward));
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
                finally
                {
                    closeSQLConnection();
                }
            }
        }

        public int addCharacter(int ownerId, String name, String imageUrl)
        {
            openSQLConnection();
            int charId;
            using (SqlCommand cmd = new SqlCommand("monbattle.CreateChar", this.sqlConnection))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("OwnerID", ownerId));
                    cmd.Parameters.Add(new SqlParameter("Name", name));
                    cmd.Parameters.Add(new SqlParameter("ImageUrl", imageUrl));

                    charId = Convert.ToInt32(cmd.ExecuteScalar());
                    return charId;
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
                finally
                {
                    closeSQLConnection();
                }
            }
        }

        public int startTrainCharacter(int userId, int charId, DateTime trainingTime, int trainingType, int cost)
        {
            openSQLConnection();
            using (SqlCommand cmd = new SqlCommand("monbattle.StartTraining", this.sqlConnection))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("UserID", userId));
                    cmd.Parameters.Add(new SqlParameter("CharID", charId));
                    cmd.Parameters.Add(new SqlParameter("trainingTime", trainingTime));
                    cmd.Parameters.Add(new SqlParameter("trainingType", trainingType));
                    cmd.Parameters.Add(new SqlParameter("cost", cost));
                    int succ = Convert.ToInt32(cmd.ExecuteScalar());
                    return succ;
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
                finally
                {

                }
            }
        }

        public int finishTrainCharacter(int charId, int effect)
        {
            openSQLConnection();
            using (SqlCommand cmd = new SqlCommand("monbattle.FinishTraining", this.sqlConnection))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("CharId", charId));
                    cmd.Parameters.Add(new SqlParameter("Effect", effect));
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
                finally
                {
                    closeSQLConnection();
                }
            }
        }

        public int useBattleCounter(int userId)
        {
            openSQLConnection();
            try
            {
                SqlCommand cmd = new SqlCommand("monbattle.CharacterDuel", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("UserID", userId));
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            finally
            {
                closeSQLConnection();
            }
        }

        public CharacterObject[] getOpponentsList(int rowsize)
        {
            CharacterObject[] opp = new CharacterObject[rowsize];

            openSQLConnection();
            string query = "SELECT TOP " + rowsize + " CharId, Name, ImageUrl, Attack, Health, Speed, Rewards " +
                           "FROM monbattle.NeutralCharacters";
            using (SqlCommand cmd = new SqlCommand(query, this.sqlConnection))
            {
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    int ind = 0;
                    while (reader.Read())
                    {
                        int charID = Convert.ToInt32(reader["CharID"]);
                        string name = Convert.ToString(reader["Name"]);
                        int attack = Convert.ToInt32(reader["Attack"]);
                        int health = Convert.ToInt32(reader["Health"]);
                        int speed = Convert.ToInt32(reader["Speed"]);
                        int rew = Convert.ToInt32(reader["Rewards"]);
                        string imageUrl = Convert.ToString(reader["ImageUrl"]);
                        //opp[ind] = new CharacterObject(charID, name, attack, health, health, speed, rew, imageUrl, null, null);
                        ind++;
                    }
                    return opp;
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
                finally
                {
                    closeSQLConnection();
                }
            }
        }

        public CharacterObject getCharacterByCharId(int charId)
        {
            openSQLConnection();
            using (SqlCommand cmd = new SqlCommand("monbattle.GetCharacterByCharId", this.sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("charId", charId));
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    return buildCharacter(reader);
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    closeSQLConnection();
                }
            }
        }

        public CharacterObject getCharacterByUserId(int userId)
        {
            openSQLConnection();

            using (SqlCommand cmd = new SqlCommand("monbattle.GetCharacterByUserId", this.sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("OwnerID", userId));
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    return buildCharacter(reader);
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    closeSQLConnection();
                }
            }
        }

        private CharacterObject buildCharacter(SqlDataReader reader)
        {
            CharacterObject character = null;
            if (reader.Read())
            {
                int charId = Convert.ToInt32(reader["CharId"]);
                String name = Convert.ToString(reader["Name"]);
                String imageUrl = Convert.ToString(reader["imageUrl"]);
                //remove
                int attack = Convert.ToInt32(reader["Attack"]);
                int health = Convert.ToInt32(reader["Health"]);
                int maxhp = Convert.ToInt32(reader["MaxHealth"]);
                int speed = Convert.ToInt32(reader["Speed"]);
                DateTime? finishTime = null;
                int? trainingType = null;
                if (!System.DBNull.Value.Equals(reader["TrainingFinishTime"]))
                {
                    finishTime = Convert.ToDateTime(reader["TrainingFinishTime"]);
                    trainingType = Convert.ToInt32(reader["TrainingType"]);
                }
                //remove end

                character = new CharacterObject(charId, name, 1, imageUrl);
            }
            return character;
        }

        public void addNeutralCharacter(String name, String atk, String hp, String spd, String rew, String url)
        {
            openSQLConnection();
            string query = "INSERT INTO monbattle.NeutralCharacters (Name,Attack,Health,Speed,rewards,ImageUrl) " +
                                   "VALUES (@Name, @Attack, @Health, @Speed, @Reward, @ImageUrl)";
            using (SqlCommand cmd = new SqlCommand(query, this.sqlConnection))
            {
                try
                {
                    cmd.Parameters.Add(new SqlParameter("Name", name));
                    cmd.Parameters.Add(new SqlParameter("Attack", atk));
                    cmd.Parameters.Add(new SqlParameter("Health", hp));
                    cmd.Parameters.Add(new SqlParameter("Speed", spd));
                    cmd.Parameters.Add(new SqlParameter("Reward", rew));
                    cmd.Parameters.Add(new SqlParameter("ImageUrl", url));
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException exception)
                {
                    throw exception;
                }
                finally
                {
                    closeSQLConnection();
                }
            }
        }

        /// <summary>
        /// Gets list of all cards
        /// </summary>
        /// <returns></returns>
        public DataTable getCards()
        {
            openSQLConnection();

            DataTable results = new DataTable("Results");

            using (SqlCommand cmd = new SqlCommand("monbattle.GetCards", this.sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    results.Load(dr);
                    return results;
                }
                catch (SqlException e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Gets the card for the specified id
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public SqlDataReader getCard(int cardId)
        {
            openSQLConnection();

            using (SqlCommand cmd = new SqlCommand("monbattle.GetCard", this.sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@CardID", SqlDbType.Int);
                cmd.Parameters["@CardID"].Value = cardId;

                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    return dr;
                }
                catch (SqlException e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Inserts a card
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public int? insertCard(CardObject card)
        {
            openSQLConnection();

            int? cardId = null;

            using (SqlCommand cmd = new SqlCommand("monbattle.InsertCard", this.sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                cmd.Parameters["@Name"].Value = card.name;

                cmd.Parameters.Add("@Description", SqlDbType.NVarChar);
                if (String.IsNullOrEmpty(card.description))
                {
                    cmd.Parameters["@Description"].Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters["@Description"].Value = card.description;
                }

                cmd.Parameters.Add("@ImageURL", SqlDbType.NVarChar);
                if (String.IsNullOrEmpty(card.imageURL))
                {
                    cmd.Parameters["@ImageURL"].Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters["@ImageURL"].Value = card.imageURL;
                }

                SqlParameter parameter = cmd.Parameters.Add("@CardID", SqlDbType.Int);
                parameter.Direction = ParameterDirection.Output;

                try
                {
                    cmd.ExecuteNonQuery();
                    cardId = (int)cmd.Parameters["@CardID"].Value;
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    closeSQLConnection();
                }
            }
            return cardId;
        }

        /// <summary>
        /// Updates a card
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public bool? updateCard(CardObject card)
        {
            openSQLConnection();

            bool? updateSuccessful = false;

            using (SqlCommand cmd = new SqlCommand("monbattle.UpdateCard", this.sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@CardID", SqlDbType.Int);
                cmd.Parameters["@CardID"].Value = card.cardId;

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                cmd.Parameters["@Name"].Value = card.name;

                cmd.Parameters.Add("@Description", SqlDbType.NVarChar);
                if (String.IsNullOrEmpty(card.description))
                {
                    cmd.Parameters["@Description"].Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters["@Description"].Value = card.description;
                }

                cmd.Parameters.Add("@ImageURL", SqlDbType.NVarChar);
                if (String.IsNullOrEmpty(card.imageURL))
                {
                    cmd.Parameters["@ImageURL"].Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters["@ImageURL"].Value = card.imageURL;
                }

                try
                {
                    cmd.ExecuteNonQuery();
                    updateSuccessful = true;
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    closeSQLConnection();
                }
            }
            return updateSuccessful;
        }

        /// <summary>
        /// Gets list of all card battles
        /// </summary>
        /// <returns></returns>
        public DataTable getCardBattles()
        {
            openSQLConnection();

            DataTable results = new DataTable("Results");

            using (SqlCommand cmd = new SqlCommand("monbattle.GetCardBattles", this.sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    results.Load(dr);
                    return results;
                }
                catch (SqlException e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Gets list of all past card battles
        /// </summary>
        /// <returns></returns>
        public DataTable getPastCardBattles(int? userId)
        {
            openSQLConnection();

            DataTable results = new DataTable("Results");

            using (SqlCommand cmd = new SqlCommand("monbattle.GetPastBattles", this.sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserID", SqlDbType.Int);

                if (userId == null)
                {
                    cmd.Parameters["@UserID"].Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters["@UserID"].Value = userId;
                }

                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    results.Load(dr);
                    return results;
                }
                catch (SqlException e)
                {
                    throw e;
                }
            }
        }

        public Dictionary<string, List<CardVoterObject>> getCardBattleVoters(int cardBattleId) {
            openSQLConnection();
            
            using (SqlCommand cmd = new SqlCommand("monbattle.ViewVotes", this.sqlConnection)) {
                Dictionary<string, List<CardVoterObject>> cardVotersMap = new Dictionary<string, List<CardVoterObject>>();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@CardBattleID", SqlDbType.Int);
                cmd.Parameters["@CardBattleID"].Value = cardBattleId;

                try {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int userId = Convert.ToInt32(reader["userId"]);
                        string email = Convert.ToString(reader["email"]);
                        string username = Convert.ToString(reader["username"]);
                        string name = Convert.ToString(reader["name"]);
                        CardVoterObject voter = new CardVoterObject(userId, username, email);
                        List<CardVoterObject> voters = null;
                        try {
                            voters = cardVotersMap[name];
                        } catch(KeyNotFoundException ex) {
                            voters = new List<CardVoterObject>();
                            cardVotersMap.Add(name, voters);
                        }
                        voters.Add(voter);
                    }
                    return cardVotersMap;
                } catch (SqlException e) {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Gets the card battle for the specified id
        /// </summary>
        /// <param name="cardBattleId"></param>
        /// <returns></returns>
        public SqlDataReader getCardBattle(int cardBattleId)
        {
            openSQLConnection();

            using (SqlCommand cmd = new SqlCommand("monbattle.GetCardBattle", this.sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@CardBattleID", SqlDbType.Int);
                cmd.Parameters["@CardBattleID"].Value = cardBattleId;

                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    return dr;
                }
                catch (SqlException e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Inserts a battle
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public int? insertCardBattle(CardBattleObject cardBattle)
        {
            openSQLConnection();

            int? cardBattleId = null;

            using (SqlCommand cmd = new SqlCommand("monbattle.InsertCardBattle", this.sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@BattleDate", SqlDbType.DateTime);
                cmd.Parameters["@BattleDate"].Value = cardBattle.battleDate;

                cmd.Parameters.Add("@CardOneID", SqlDbType.Int);
                cmd.Parameters["@CardOneID"].Value = cardBattle.cardOne.cardId;

                cmd.Parameters.Add("@CardTwoID", SqlDbType.Int);
                cmd.Parameters["@CardTwoID"].Value = cardBattle.cardTwo.cardId;

                SqlParameter parameter = cmd.Parameters.Add("@CardBattleID", SqlDbType.Int);
                parameter.Direction = ParameterDirection.Output;

                try
                {
                    cmd.ExecuteNonQuery();
                    cardBattleId = (int)cmd.Parameters["@CardBattleID"].Value;
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    closeSQLConnection();
                }
            }
            return cardBattleId;
        }

        /// <summary>
        /// Updates a card battle
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public bool? updateCardBattle(CardBattleObject cardBattle)
        {
            openSQLConnection();

            bool? updateSuccessful = false;

            using (SqlCommand cmd = new SqlCommand("monbattle.UpdateCard", this.sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@CardBattleID", SqlDbType.Int);
                cmd.Parameters["@CardBattleID"].Value = cardBattle.cardBattleId;

                cmd.Parameters.Add("@BattleDate", SqlDbType.DateTime);
                cmd.Parameters["@BattleDate"].Value = cardBattle.battleDate;

                cmd.Parameters.Add("@CardOneID", SqlDbType.Int);
                cmd.Parameters["@CardOneID"].Value = cardBattle.cardOne;

                cmd.Parameters.Add("@CardTwoID", SqlDbType.Int);
                cmd.Parameters["@CardTwoID"].Value = cardBattle.cardTwo;

                try
                {
                    cmd.ExecuteNonQuery();
                    updateSuccessful = true;
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    closeSQLConnection();
                }
            }
            return updateSuccessful;
        }

        public List<CardVoterCounterObject> getVoteWinsAfterDate(String date) {
            openSQLConnection();
            List<CardVoterCounterObject> list = new List<CardVoterCounterObject>();

            using (SqlCommand cmd = new SqlCommand("monbattle.GetWinnerVoteCounts", this.sqlConnection)) {
                cmd.CommandType = CommandType.StoredProcedure;

                try {
                    cmd.Parameters.Add(new SqlParameter("StartDate", date));

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read()) {
                        CardVoterCounterObject obj = new CardVoterCounterObject();
                        obj.name = Convert.ToString(reader["username"]);
                        obj.winCount = Convert.ToInt32(reader["votecounts"]);

                        list.Add(obj);
                    }




                    return list;
                } catch (SqlException e) {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Gets the card battle for today
        /// </summary>
        /// <param name="cardBattleId"></param>
        /// <returns></returns>
        public SqlDataReader getCardBattleToday()
        {
            openSQLConnection();

            using (SqlCommand cmd = new SqlCommand("monbattle.GetCardBattleToday", this.sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    return dr;
                }
                catch (SqlException e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Gets the card battle for tomorrow
        /// </summary>
        /// <param name="cardBattleId"></param>
        /// <returns></returns>
        public SqlDataReader getCardBattleTomorrow()
        {
            openSQLConnection();

            using (SqlCommand cmd = new SqlCommand("monbattle.GetCardBattleTomorrow", this.sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    return dr;
                }
                catch (SqlException e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Inserts a battle pick
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cardBattleId"></param>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public int? insertCardPick(int userId, int cardBattleId, int cardId)
        {
            openSQLConnection();

            int? cardPickId = null;

            using (SqlCommand cmd = new SqlCommand("monbattle.InsertCardPick", this.sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserID", SqlDbType.Int);
                cmd.Parameters["@UserID"].Value = userId;

                cmd.Parameters.Add("@CardBattleID", SqlDbType.Int);
                cmd.Parameters["@CardBattleID"].Value = cardBattleId;

                cmd.Parameters.Add("@CardID", SqlDbType.Int);
                cmd.Parameters["@CardID"].Value = cardId;

                SqlParameter parameter = cmd.Parameters.Add("@CardPickID", SqlDbType.Int);
                parameter.Direction = ParameterDirection.Output;

                try
                {
                    cmd.ExecuteNonQuery();
                    cardPickId = (int)cmd.Parameters["@CardPickID"].Value;
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    closeSQLConnection();
                }
            }
            return cardPickId;
        }


        /// <summary>
        /// Gets list of all users
        /// </summary>
        /// <returns></returns>
        public DataTable getUsers()
        {
            openSQLConnection();

            DataTable results = new DataTable("Results");

            using (SqlCommand cmd = new SqlCommand("monbattle.GetUsers", this.sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    results.Load(dr);
                    return results;
                }
                catch (SqlException e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Updates card battle's result
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public bool? updateCardBattleVotesAndWinner(int cardBattleId)
        {
            openSQLConnection();

            bool? updateSuccessful = false;

            using (SqlCommand cmd = new SqlCommand("monbattle.UpdateCardBattleVotesAndWinner", this.sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@CardBattleID", SqlDbType.Int);
                cmd.Parameters["@CardBattleID"].Value = cardBattleId;

                try
                {
                    cmd.ExecuteNonQuery();
                    updateSuccessful = true;
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    closeSQLConnection();
                }
            }
            return updateSuccessful;
        }

        /// <summary>
        /// Updates user points from battle
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public bool? updateUserPointsFromBattle(int cardBattleId, int winnerCardId)
        {
            openSQLConnection();

            bool? updateSuccessful = false;

            using (SqlCommand cmd = new SqlCommand("monbattle.UpdateUserPointsFromBattle", this.sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@CardBattleID", SqlDbType.Int);
                cmd.Parameters["@CardBattleID"].Value = cardBattleId;

                cmd.Parameters.Add("@WinnerCardID", SqlDbType.Int);
                cmd.Parameters["@WinnerCardID"].Value = winnerCardId;

                try
                {
                    cmd.ExecuteNonQuery();
                    updateSuccessful = true;
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    closeSQLConnection();
                }
            }
            return updateSuccessful;
        }

        public DataTable getBanner()
        {
            openSQLConnection();

            DataTable results = new DataTable("Results");

            using (SqlCommand cmd = new SqlCommand("monbattle.GetBanner", this.sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    results.Load(dr);
                    return results;
                }
                catch (SqlException e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// Inserts a new banner
        /// </summary>
        /// <returns></returns>
        public int? insertBanner(string fileName, string URL)
        {
            openSQLConnection();

            int? bannerId = null;

            using (SqlCommand cmd = new SqlCommand("monbattle.InsertBanner", this.sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@FileName", SqlDbType.NVarChar);
                cmd.Parameters["@FileName"].Value = fileName;

                cmd.Parameters.Add("@URL", SqlDbType.NVarChar);
                cmd.Parameters["@URL"].Value = URL;

                SqlParameter parameter = cmd.Parameters.Add("@BannerID", SqlDbType.Int);
                parameter.Direction = ParameterDirection.Output;

                try
                {
                    cmd.ExecuteNonQuery();
                    bannerId = (int)cmd.Parameters["@BannerID"].Value;
                }
                catch (SqlException e)
                {
                    throw e;
                }
                finally
                {
                    closeSQLConnection();
                }
            }
            return bannerId;
        }

        public void addMove(string Name, string Description, string Turns, string Linger, 
            string MeterCost, string CommandStr, string redeemCost, string imageUrl) {
            openSQLConnection();

            using (SqlCommand cmd = new SqlCommand("monbattle.CreateMove", this.sqlConnection)) {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("Name", Name));
                cmd.Parameters.Add(new SqlParameter("Description", Description));
                cmd.Parameters.Add(new SqlParameter("Turns", Turns));
                cmd.Parameters.Add(new SqlParameter("Linger", Linger));
                cmd.Parameters.Add(new SqlParameter("MeterCost", MeterCost));
                cmd.Parameters.Add(new SqlParameter("CommandStr", CommandStr));
                cmd.Parameters.Add(new SqlParameter("redeemCost", redeemCost));
                cmd.Parameters.Add(new SqlParameter("imageUrl", imageUrl));

                try {
                    cmd.ExecuteNonQuery();
                } catch (SqlException e) {
                    throw e;
                } finally  {
                    closeSQLConnection();
                }
            }
        }

        public List<Move> getMoveCatalog() {
            openSQLConnection();
            List<Move> moveList = new List<Move>();
            using (SqlCommand cmd = new SqlCommand("monbattle.GetAllMoveCatalog", this.sqlConnection)) {
                try {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        int moveId = Convert.ToInt32(reader["MoveId"]);
                        string name = Convert.ToString(reader["name"]);
                        string description = Convert.ToString(reader["description"]);
                        int turns = Convert.ToInt32(reader["turns"]);
                        bool linger = Convert.ToBoolean(reader["linger"]); 
                        int meterCost = Convert.ToInt32(reader["MeterCost"]);
                        string commandStr = Convert.ToString(reader["CommandStr"]);
                        int redeemCost = Convert.ToInt32(reader["redeemCost"]);
                        string imageUrl = Convert.ToString(reader["imageUrl"]);

                        Move move = new Move(name, description, linger, turns, meterCost, commandStr, 0);
                        moveList.Add(move);
                    }
                    return moveList;
                } catch (SqlException e) {
                    throw e;
                } finally  {
                    closeSQLConnection();
                }
            }
        }

        public void assignMove(int charId, string moveId) {
            openSQLConnection();
            using (SqlCommand cmd = new SqlCommand("monbattle.AssignMoveToCharacter", this.sqlConnection)) {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("charId", charId));
                cmd.Parameters.Add(new SqlParameter("moveId", moveId));
                try {
                    cmd.ExecuteNonQuery();
                } catch (SqlException e) {
                    throw e;
                } finally  {
                    closeSQLConnection();
                }
            }
        }
    }
}