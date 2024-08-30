#region
//Foundation
//Copyright (C) 2023  ViralReaction
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
#endregion
using UnityEngine;
using Verse;

namespace Foundation
{
    public class CompProperties_HumeShield : CompProperties
    {
        public int startingTicksToReset = 600;
        public float minDrawSize = 1.2f;
        public float maxDrawSize = 1.55f;
        public float energyOnReset = 40f;
        public bool blocksRangedWeapons = true;
        public string texPath = "Other/ShieldBubble";
        public Color shieldColor = Color.white;
        public int EnergyShieldEnergyMax = 30;
        public int EnergyShieldRechargeRate = 30;

        public CompProperties_HumeShield() => this.compClass = typeof(CompHumeShield);
    }
}