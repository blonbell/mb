using MonBattle.Data.BattleMechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/*
 * Represents an opponent found by the system.
 */
namespace MonBattle.Data {

    public class CharacterObject {
        //database values
        public int charId { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Attack { get; set; }
        public int Speed { get; set; }
        public List<Move> chosenMoves = new List<Move>();
        public List<Move> moveset = new List<Move>();
        public int Reward { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? trainingFinishTime { get; set; }
        public trainingTypeEnum? trainingType { get; set; }

        public enum trainingTypeEnum { Attack = 1, Health = 2, Speed = 3, Move = 4 };

        //Skills should consume meter
        public int Meter = 0;

        //battle calculations members
        public double percentDamage = 0;
        public double percentDamageResist = 0;
        public double percentHeal = 0;

        public int flatDamage = 0;
        public int flatDamageResist = 0;
        public int flatHeal = 0;

        /**
         * Deprecate and use NeutralMonsters
         */
        public CharacterObject(int charId, string name, int Reward, string ImageUrl) {
            this.charId = charId;
            this.Name = name;
            this.Attack = 10;
            this.Health = 150;
            this.MaxHealth = 150;
            Move k1 = new Move(100, "Critical Strike", "desc", true, 1, 4, "DMG050-HEP020", 0, null);
            k1.ownerId = charId;
            Move k2 = new Move(100, "Balanced Attack", "desc", true, 1, 4, "DMG030-RES030", 0, null);
            k1.ownerId = charId;
            Move k3 = new Move(100, "Massive Blow", "desc", true, 1, 4, "DMG100-HEP020", 0, null);
            k1.ownerId = charId;
            Move k4 = new Move(100, "Weakass Hit", "desc", true, 1, 4, "DMG020-HEP005", 0, null);
            k1.ownerId = charId;

            moveset.Add(k1);
            moveset.Add(k2);
            moveset.Add(k3);
            moveset.Add(k4);
        }

        public CharacterObject(int charId, string name, int atk, int hp,
            int maxhp, int spd, int rew, string imageUrl, DateTime? finishTime, int? trainingType) {
            this.charId = charId;
            this.Name = name;
            this.Attack = atk;
            this.Health = hp * 50;
            this.MaxHealth = maxhp * 50;
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

        public void resolveBattleCalc(List<string> battleLog) {
            Meter += 2;

            int totalHeal = Convert.ToInt32(MaxHealth * percentHeal) + flatHeal;
            if (totalHeal > 0) {
                battleLog.Add(this.Name + " has regain " + totalHeal + "HP this turn");
            }

            double beforeResistDamage = (MaxHealth * percentDamage) + flatDamage;
            int totalDamageTaken = Convert.ToInt32(beforeResistDamage * (1 - percentDamageResist) - flatDamageResist);
            
            // apply heals first, then damage and then log all actions
            Health = Math.Min(MaxHealth, Health + Convert.ToInt32(totalHeal));  
            if (totalDamageTaken > 0) {
                battleLog.Add(this.Name + " received " + totalDamageTaken + " damage this turn");
                Health -= totalDamageTaken;
            }

            resetCalculationsParams();
        }

        private void resetCalculationsParams() {
            percentDamage = 0;
            percentDamageResist = 0;
            percentHeal = 0;

            flatDamage = 0;
            flatDamageResist = 0;
            flatHeal = 0;
        }

        public override string ToString() {
            return "Character: { " + charId + ", " + Name + ", " + Attack + ", " + Health
                + ", " + MaxHealth + ", " + Speed + ", " + ImageUrl + ", " + trainingFinishTime + ", " + trainingType + "}";
        }
    
        public void clearMoveSet() {
            chosenMoves.Clear();
            moveset.Clear();
        }

        public void chooseMoves() {
            int length = Math.Min(4, moveset.Count);
            chosenMoves = moveset.OrderBy(x => Guid.NewGuid()).Take(length).ToList();
        }
    }
}

