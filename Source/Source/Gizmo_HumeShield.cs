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
using Foundation.Comps;
using Verse;

namespace Foundation
{
    [StaticConstructorOnStartup]
    public class Gizmo_HumeShield : Gizmo
    {
        public CompHumeShield shield;
        private static readonly Texture2D FullShieldBarTex = SolidColorMaterials.NewSolidColorTexture(new Color(0.2f, 0.2f, 0.24f));
        private static readonly Texture2D EmptyShieldBarTex = SolidColorMaterials.NewSolidColorTexture(Color.clear);

        public Gizmo_HumeShield() => this.Order = -100f;

        public override float GetWidth(float maxWidth) => 140f;

        public override GizmoResult GizmoOnGUI(Vector2 topLeft, float maxWidth, GizmoRenderParms parms)
        {
            Rect rect = new Rect(topLeft.x, topLeft.y, GetWidth(maxWidth), 75f);
            Rect rect2 = rect.ContractedBy(6f);
            Widgets.DrawWindowBackground(rect);
            Rect rect3 = rect2;
            rect3.height = rect.height / 2f;
            Text.Font = GameFont.Tiny;
            Widgets.Label(rect3, shield.parent.LabelCap); //Should I rename to Hume Shield in game??
            Rect rect4 = rect2;
            rect4.yMin = rect2.y + rect2.height / 2f;
            float fillPercent = shield.Energy / shield.EnergyMax;
            Widgets.FillableBar(rect4, fillPercent, FullShieldBarTex, EmptyShieldBarTex, doBorder: false);
            Text.Font = GameFont.Small;
            Text.Anchor = TextAnchor.MiddleCenter;
            Widgets.Label(rect4, shield.Energy.ToString("F0") + " / " + shield.EnergyMax.ToString("F0"));
            Text.Anchor = TextAnchor.UpperLeft;
            return new GizmoResult(GizmoState.Clear);
        }
    }
}