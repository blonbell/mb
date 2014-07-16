using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonBattle.Data
{
    public class CardObject
    {
        // Member variables
        public int? cardId;
        public string name;
        public string description;
        public string imageURL;
        public DateTime? dateCreated;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CardObject()
        {

        }

        /// <summary>
        /// Constructor to set all member variables
        /// </summary>
        /// <param name="CardId"></param>
        /// <param name="Name"></param>
        /// <param name="Description"></param>
        /// <param name="ImageURL"></param>
        /// <param name="DateCreated"></param>
        public CardObject(int? CardId, string Name, string Description, string ImageURL, DateTime? DateCreated)
        {
            cardId = CardId;
            name = Name;
            description = Description;
            imageURL = ImageURL;
            dateCreated = DateCreated;
        }
    }
}