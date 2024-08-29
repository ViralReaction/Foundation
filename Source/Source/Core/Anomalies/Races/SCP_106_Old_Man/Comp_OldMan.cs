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
using Foundation.SRA;
using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace Foundation
{
    public class Comp_OldMan : ThingComp
    {
        public int tickCounter = 0;
        private Pawn targetHunted;
        public Pawn oldMan => this.parent as Pawn;
        private CompProperties_OldMan Props => (CompProperties_OldMan)this.props;


        public override void CompTick()
        {
            base.CompTick();
            ++tickCounter;
            if (tickCounter > Props.tickInterval)
            {
                if (!ScrantonCheck(oldMan) && !oldMan.Downed)
                {
                    if ((oldMan.CurJobDef == JobDefOf.PredatorHunt || oldMan.CurJob.def == JobDefOf.AttackMelee ) && this.WithinJumpRange(oldMan.CurJob.targetA.Thing.Position))
                    {
                        this.targetHunted = this.oldMan?.CurJob?.targetA.Thing as Pawn;
                    }
                    else if (oldMan.CurJob.def == JobDefOf.GotoWander || oldMan.CurJob.def == JobDefOf.Wait_Wander || oldMan.CurJob.def == JobDefOf.Wait_MaintainPosture || oldMan.IsOnHoldingPlatform || oldMan.mindState.mentalStateHandler.InMentalState)
                    {
                        List<Pawn> pawnList = SCPRadius.GetPawnsAround(oldMan.Position, Props.jumpRange, oldMan.MapHeld);
                        for (int index = 0; index < pawnList.Count; index++)
                        {
                            this.targetHunted = pawnList[index];
                            if (targetHunted != oldMan && !targetHunted.NonHumanlikeOrWildMan() && !this.TooCloseToJump())
                            {
                                goto label_19;
                            }
                        }
                    }
                label_19:
                    if (this.targetHunted != null)
                    {
                        if (!this.TooCloseToJump() && !ScrantonCheck(targetHunted))
                        {
                            List<Thing> source = new List<Thing>();
                            source.Add(oldMan);
                            IntVec3 pawnPos = targetHunted.Position;
                            GenExplosion.DoExplosion(oldMan.Position, targetHunted.MapHeld, 1, FoundationDefOf.Foundation_OldMan_Scratch, oldMan, 1, 100, SoundDefOf.Corpse_Drop, postExplosionSpawnThingDef: FoundationDefOf.Filth_OldMan, postExplosionSpawnChance: 1, postExplosionSpawnThingCount: 1, postExplosionGasType: null, applyDamageToExplosionCellsNeighbors: false, preExplosionSpawnThingDef: null, preExplosionSpawnChance: 0, preExplosionSpawnThingCount: 0, chanceToStartFire: 0, damageFalloff: false, ignoredThings: source, doVisualEffects: false, propagationSpeed: 1);
                            GenExplosion.DoExplosion(pawnPos, targetHunted.MapHeld, 1, FoundationDefOf.Foundation_OldMan_Scratch, oldMan, 1, 100, SoundDefOf.Corpse_Drop, postExplosionSpawnThingDef: FoundationDefOf.Filth_OldMan, postExplosionSpawnChance: 1, postExplosionSpawnThingCount: 1, postExplosionGasType: null, applyDamageToExplosionCellsNeighbors: false, preExplosionSpawnThingDef: null, preExplosionSpawnChance: 0, preExplosionSpawnThingCount: 0, chanceToStartFire: 0, damageFalloff: false, ignoredThings: source, doVisualEffects: false, propagationSpeed: 1);
                            oldMan.pather.StopDead();
                            oldMan.Position = pawnPos;
                            oldMan.pather.ResetToCurrentPosition();
                            if (oldMan.CurJobDef != JobDefOf.PredatorHunt || oldMan.CurJobDef != JobDefOf.AttackMelee || !oldMan.mindState.mentalStateHandler.InMentalState)
                            {
                                Job job = JobMaker.MakeJob(JobDefOf.PredatorHunt, (LocalTargetInfo)(Thing)targetHunted);
                                oldMan.jobs.TryTakeOrderedJob(job);
                            }
                            oldMan.pather.StartPath(oldMan.CurJob.targetA.Cell, PathEndMode.ClosestTouch);
                        }
                    }
                }
                tickCounter = 0;
            }
        }
        private bool ScrantonCheck(Pawn pawn)
        {
            var field = SuppressionFieldAccessUtility.GetSuppressionFieldManager(pawn.Map)
                            ?.GetEffectOnCell(pawn.Position) ?? 0f;
            if (field != 0f)
                return true;
            return false;
        }
        private bool TooCloseToJump()
        {
            if (this.targetHunted == null)
            {
                return true;
            }
            return this.targetHunted.Downed || this.targetHunted.Dead || this.targetHunted.CanSee((Thing)this.oldMan) && this.WithinJumpRange(this.targetHunted.Position) && SPExtra.Distance(this.oldMan.Position, this.targetHunted.Position) <= (double)this.Props.directAttackRange;
        }
        private bool WithinJumpRange(IntVec3 targetPos) => SPExtra.Distance(oldMan.Position, targetPos) <= (double)this.Props.jumpRange;
    }
}