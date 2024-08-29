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
using System.Collections.Generic;
using Verse;

namespace Foundation
{
    [StaticConstructorOnStartup]
    public class StaticCollectionsClass
    {
        public static List<HeadTypeDef> maleHeadTypeList = new List<HeadTypeDef>();
        public static List<HeadTypeDef> femaleHeadTypeList = new List<HeadTypeDef>();
        public static List<BeardDef> beardDefList = new List<BeardDef>();

        static StaticCollectionsClass()
        {

            foreach (HeadTypeDef headType in DefDatabase<HeadTypeDef>.AllDefsListForReading)
            {
                string genderType = headType.gender.ToString();
                if (genderType == "Male")
                {
                    maleHeadTypeList.Add(headType);
                }
                if (genderType == "Female")
                {
                    femaleHeadTypeList.Add(headType);
                }
            }
            foreach (BeardDef beardDef in DefDatabase<BeardDef>.AllDefsListForReading)
            {
                beardDefList.Add(beardDef);
            }
        }
    }
}
