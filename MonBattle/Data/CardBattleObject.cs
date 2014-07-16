using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MonBattle.Data;

namespace MonBattle.Data
{
    public class CardBattleObject
    {
        // Member variables
        public int? cardBattleId;
        public DateTime? battleDate;
        public CardObject cardOne;
        public CardObject cardTwo;
        public int? winnerCardId;
        public int? cardOneVotes;
        public int? cardTwoVotes;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CardBattleObject()
        {

        }

        /// <summary>
        /// Constructor to set all member variables
        /// </summary>
        /// <param name="CardBattleId"></param>
        /// <param name="BattleDate"></param>
        /// <param name="CardOne"></param>
        /// <param name="CardTwo"></param>
        /// <param name="WinnerCardId"></param>
        public CardBattleObject(int? CardBattleId, DateTime? BattleDate, CardObject CardOne, CardObject CardTwo, int? WinnerCardId, int? CardOneVotes, int? CardTwoVotes)
        {
            cardBattleId = CardBattleId;
            battleDate = BattleDate;
            cardOne = CardOne;
            cardTwo = CardTwo;
            winnerCardId = WinnerCardId;
            cardOneVotes = CardOneVotes;
            cardTwoVotes = CardTwoVotes;
        }
    }
}