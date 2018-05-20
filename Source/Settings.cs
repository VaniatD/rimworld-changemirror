﻿using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace ChangeMirror
{
    public class SettingsController : Mod
    {
        public SettingsController(ModContentPack content) : base(content)
        {
            base.GetSettings<Settings>();
        }

        public override string SettingsCategory()
        {
            return "ChangeMirror".Translate();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Settings.DoSettingsWindowContents(inRect);
        }
    }

    public class Settings : ModSettings
    {
        private static bool showGenderAgeChange = true;
        private static bool showBodyChange = true;
        private static bool keepForcedApparel = true;

        public static bool ShowGenderAgeChange { get { return showGenderAgeChange; } }
        public static bool ShowBodyChange { get { return showBodyChange; } }
        public static bool KeepForcedApparel { get { return keepForcedApparel; } }
        public static int RepairAttachmentDistance { get { return 6; } }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<bool>(ref showGenderAgeChange, "ChangeMirror.ShowGenderAgeChange", true, true);
            Scribe_Values.Look<bool>(ref showBodyChange, "ChangeMirror.ShowBodyChange", true, true);
            Scribe_Values.Look<bool>(ref keepForcedApparel, "ChangeMirror.KeepForcedApparel", false, true);

            VerifySupportedEditors(showBodyChange);
        }

        public static void DoSettingsWindowContents(Rect rect)
        {
            Listing_Standard l = new Listing_Standard(GameFont.Small);
            l.ColumnWidth = System.Math.Min(400, rect.width / 2);
            l.Begin(rect);
            l.CheckboxLabeled("ChangeMirror.ShowBodyChange".Translate(), ref showBodyChange);
            if (showBodyChange)
            {
                l.Gap(4);
                l.CheckboxLabeled("ChangeMirror.ShowGenderAgeChange".Translate(), ref showGenderAgeChange);
                l.Gap(20);
            }
            else
            {
                l.Gap(48);
            }
            l.End();

            VerifySupportedEditors(showBodyChange);
        }

        private static void VerifySupportedEditors(bool showBodyChange)
        {
            if (showBodyChange && Building_ChangeMirror.SupportedEditors.Count == 2)
            {
                Building_ChangeMirror.SupportedEditors.Add(UI.Enums.CurrentEditorEnum.ChangeMirrorBody);
                Building_ChangeMirror.SupportedEditors.Add(UI.Enums.CurrentEditorEnum.ChangeMirrorBody);
            }
            else if (!showBodyChange && Building_ChangeMirror.SupportedEditors.Count == 3)
            {
                Building_ChangeMirror.SupportedEditors.Remove(UI.Enums.CurrentEditorEnum.ChangeMirrorBody);
                Building_ChangeMirror.SupportedEditors.Remove(UI.Enums.CurrentEditorEnum.ChangeMirrorBody);
            }
        }
    }
}