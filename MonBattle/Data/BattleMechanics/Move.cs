using MonBattle.Data.BattleMechanics.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonBattle.Data.BattleMechanics {

    public class Move {
        //database values
        public int moveId;
        public string name, description;
        bool linger = true; //difference between activate on every turn and activate after n turns
        int activeTurns = 0, activateOnTurn = 0;
        public int meterCost = 0; 
        public int redeemCost;
        public string imageUrl;
        public bool inUse = false;

        // parsed from db string
        List<Effect> effects;
        
        //state vars
        bool expired = false;
        public int? ownerId; //the charId
        
        public Move(string name, string commandStr, int? ownerId) {
            this.name = name;
            this.ownerId = ownerId;

            effects = new List<Effect>();
            if(!String.IsNullOrEmpty(commandStr)) {
                string[] effectStrings = commandStr.Split('-');
                foreach(string effectStr in effectStrings) {
                    parseEffect(effectStr);
                }
            }
        }

        /*public Move(int moveId, string name, string description, bool isRepeatEffect, int turns, int meterCost, 
            string commandStr, string imageUrl, bool active) {
            init(moveId, name, description, false, 0, meterCost, commandStr, 0, imageUrl, 0);
            inUse = active;
        }*/

        public Move(int moveId, string name, string description, bool isRepeatEffect, int turns, 
            int meterCost, string commandStr, int redeemCost, string imageUrl) {
            this.moveId = moveId;
            this.name = name;
            this.description = description;
            this.meterCost = meterCost;
            this.linger = isRepeatEffect;
            this.redeemCost = redeemCost;
            this.imageUrl = imageUrl;
            //this.ownerId = ownerId;
            if (isRepeatEffect) {
                activeTurns = turns;
            } else {
                activateOnTurn = turns;
            }

            effects = new List<Effect>();
            if(!String.IsNullOrEmpty(commandStr)) {
                string[] effectStrings = commandStr.Split('-');
                foreach(string effectStr in effectStrings) {
                    parseEffect(effectStr);
                }
            }
        }


        public bool isExpired() {
            return expired;
        }

        /**
         * Resolves if the move should be activated this turn.
         */
        public bool applyThisTurn(List<string> battleLog) {
            if (linger) {
                activeTurns--;
            } else {
                activateOnTurn--;
                if (activateOnTurn > 0) {
                    battleLog.Add(activateOnTurn + " more turns until " + name + " activates");
                    return false;
                }
            }
            return true;
        }

        public void applyOnCharacter(CharacterObject character, List<string> battleLog) {
            foreach (Effect eff in effects) {
                string effMsg = eff.applyEffect(character, ownerId.Value, this.name);
                if(!String.IsNullOrEmpty(effMsg)) {
                    battleLog.Add(effMsg);
                }
            }

            //set Expiry
            if (activeTurns <= 0) {
                expired = true;
            }
        }

        /**
         * Example
         * DMG030 damage opp for 30
         * HEA040 heal for 40
         * TUR002 for the next two turns
         */
        private void parseEffect(string eStr) {
            if (string.IsNullOrEmpty(eStr) || eStr.Length != 6) {
                return;
            }
            String type = eStr.Substring(0, 3);
            String numberStr = eStr.Substring(3, 3);
            int number = Convert.ToInt32(numberStr);

            switch (type) {
                case "DMG": //damages
                    effects.Add(new FlatDamageEffect(number));
                    break;
                case "DMP":
                    effects.Add(new PercentDamageEffect(number));
                    break;
                case "HEA":
                    effects.Add(new FlatHealEffect(number));
                    break;
                case "HEP":
                    effects.Add(new PercentHealEffect(number));
                    break;
                case "RES":
                    effects.Add(new FlatResistanceEffect(number));
                    break;
                case "REP":
                    effects.Add(new PercentResistanceEffect(number));
                    break;
                case "WEP":
                    effects.Add(new PercentWeaknessEffect(number));
                    break;
                case "SID":
                    effects.Add(new SelfInflictDamageEffect(number));
                    break;
            }
        }
    }
}