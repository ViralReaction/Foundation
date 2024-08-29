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
using RimWorld;
using System;
using Verse;

namespace Foundation
{
    internal class CompUseEffect_UseGenderStone : CompUseEffect
    {
        public override void DoEffect(Pawn usedBy)
        {
            base.DoEffect(usedBy);
            DamageInfo dinfo = new DamageInfo(DamageDefOf.Burn, 1f, 2);
            usedBy.TakeDamage(dinfo);
            usedBy.gender = GenderUtility.Opposite(usedBy.gender);
            if (usedBy.story.bodyType == BodyTypeDefOf.Male)
            {

                usedBy.story.bodyType = BodyTypeDefOf.Female;
            }
            else if (usedBy.story.bodyType == BodyTypeDefOf.Female)
            {
                usedBy.story.bodyType = BodyTypeDefOf.Male;
                
            }
            string headGender = usedBy.story.headType.gender.ToString();
            if (headGender == "Male")
            {
                int num = Rand.Range(0,StaticCollectionsClass.femaleHeadTypeList.Count);
                usedBy.story.headType = StaticCollectionsClass.femaleHeadTypeList[num];
                usedBy.style.beardDef = BeardDefOf.NoBeard;
            }    
            else if (headGender == "Female")
            {
                int num = Rand.Range(0, StaticCollectionsClass.maleHeadTypeList.Count);
                usedBy.story.headType = StaticCollectionsClass.maleHeadTypeList[num];
                if (Rand.Chance(0.25f))
                {
                    int beardNum = Rand.Range(0, StaticCollectionsClass.beardDefList.Count);
                    usedBy.style.beardDef = StaticCollectionsClass.beardDefList[beardNum];
                }
               
            }
            usedBy.Drawer.renderer.SetAllGraphicsDirty();
            string label = usedBy.gender.GetLabel();
            if (usedBy.gender != 0)
            {
                Messages.Message((string)("Foundation_GenderStoneChange".Translate() + label), MessageTypeDefOf.NeutralEvent);
                Thought_Memory newThought = (Thought_Memory)ThoughtMaker.MakeThought(FoundationDefOf.Foundation_GenderStone_BadThought);
                usedBy.needs.mood.thoughts.memories.TryGainMemory(newThought);
            }
            else
                Messages.Message((string)"Foundation_GenderStoneFail".Translate(), MessageTypeDefOf.NeutralEvent);
        }
    }
}