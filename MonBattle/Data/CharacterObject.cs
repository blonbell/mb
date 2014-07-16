using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/*
 * Represents an opponent found by the system.
 */
namespace MonBattle.Data
{
    public class CharacterObject
    {

        public int charId { get; set; }
        public string Name { get; set; }
        public int Attack { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Speed { get; set; }
        public int Reward { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? trainingFinishTime { get; set; }
        public trainingTypeEnum? trainingType { get; set; }
        public enum trainingTypeEnum { Attack = 1, Health = 2, Speed = 3 };

        public CharacterObject(int charId, string name, int atk, int hp,
            int maxhp, int spd, int rew, string imageUrl, DateTime? finishTime, int? trainingType)
        {
            this.charId = charId;
            this.Name = name;
            this.Attack = atk;
            this.Health = hp;
            this.MaxHealth = maxhp;
            this.Speed = spd;
            this.Reward = rew;
            this.ImageUrl = imageUrl;
            this.trainingFinishTime = finishTime;
            if (trainingType.HasValue)
            {
                this.trainingType = (trainingTypeEnum)trainingType;
            }
            else
            {
                this.trainingType = null;
            }
        }

        public override string ToString()
        {
            return "Character: { " + charId + ", " + Name + ", " + Attack + ", " + Health
                + ", " + MaxHealth + ", " + Speed + ", " + ImageUrl + ", " + trainingFinishTime + ", " + trainingType + "}";
        }
    }
}