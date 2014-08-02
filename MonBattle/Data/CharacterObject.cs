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
        public int Health = 100;
        public int MaxHealth = 100;
        public int Attack = 10;
        public int Speed = 5;
        public Move[] moveset = new Move[4];
        public int Reward { get; set; }
        public string ImageUrl { get; set; }

        //Skills should consume meter
        public int Meter = 0;

        //battle calculations members
        public double percentDamage = 0;
        public double percentDamageResist = 0;
        public double percentHeal = 0;

        public int flatDamage = 0;
        public int flatDamageResist = 0;
        public int flatHeal = 0;

        public CharacterObject(int charId, string name, int Reward, string ImageUrl) {
            this.charId = charId;
            this.Name = name;
            //this.moveset = ; dataController get moveset
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
    }
}