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

using Verse;

namespace Foundation
{
    [StaticConstructorOnStartup]
    public class Foundation_Startup

    {
        public static string version = "v0.0.1";

        static Foundation_Startup()
        {
            Log.Message("Level 5 security credentials accepted. Welcome back to the Foundation, [REDACTED]\nSecure Contain Protect [" + Foundation_Startup.version + "] loaded.");
        }
    }
}