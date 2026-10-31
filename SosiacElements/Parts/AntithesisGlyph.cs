using Quintessential;
using SosiacElements.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Parts
{
    internal class AntithesisGlyph
    {
        public static HexIndex InputLeftHex = new(-1, 0);
        public static HexIndex InputRightHex = new(1, 0);
        public static HexIndex OutputHex = new(0, 0);

        public static Texture AntithesisBase = AssetLoaderHelper.LoadTexture("textures/sosiac_elements/parts/antithesis_base");
        public static Texture AntithesisPartialBase = AssetLoaderHelper.LoadTexture("textures/sosiac_elements/parts/antithesis_partial_base");
        public static Texture AntithesisBowlLeft = AssetLoaderHelper.LoadTexture("textures/sosiac_elements/parts/left_bowl_symbol");
        public static Texture AntithesisBowlRight = AssetLoaderHelper.LoadTexture("textures/sosiac_elements/parts/right_bowl_symbol");
        public static Texture[] SmokeAnim = Assets.textures.parts.disposal_flash;
        public static Texture AntithesisBowl = Assets.textures.parts.calcinator_bowl;

        public static void RenderTotal(Part part, Vector2 pos, SolutionEditorBase editor, PartRenderer renderer)
        {
            PartSimState pss = editor.GetSimulation().GetSimState(part);
            IntermediatePartState uco = editor.GetIntermState(part, pos);
            float time = editor.GetCycleTime();

            Vector2 centre = AntithesisBase.GetHalfSizeRounded();
            renderer.RenderBase(AntithesisBase, new(0, 0), centre, 0f);

            renderer.RenderRotating(AntithesisBowl, InputRightHex, Vector2.Zero);
            renderer.RenderRotating(AntithesisBowl, InputLeftHex, Vector2.Zero);
            renderer.RenderUpright(AntithesisBowlRight, InputLeftHex, Vector2.Zero);
            renderer.RenderUpright(AntithesisBowlLeft, InputRightHex, Vector2.Zero);
            // if a partial antithesis is hooked onto this, add right side symbol to right bowl
            if (editor.GetSimulation() is Sim sim && sim.GetSolution().parts.Count(foundPart => (foundPart.GetType() == SosiacElements.Parts.PartialAntithesis
                    && foundPart.InFrontBy(InputLeftHex) == part.InFrontBy(InputRightHex))) == 1)
            {
                renderer.RenderUpright(AntithesisBowlRight, InputRightHex, Vector2.Zero);
            }

            // standard iris code
            int IrisFrame = 15;
            bool AfterIrisOpens = false;
            Molecule RisingAtom = null;
            Vector2 RisingOffset = uco.pos + HexGrid.standardGrid.ToPixelCoords(OutputHex).Rotated(uco.rotation);

            //render under output iris
            renderer.RenderRotating(Assets.textures.parts.animismus.output_under_iris, OutputHex, Vector2.Zero);

            //if ejecting, render animation
            if (pss.isProcessing)
            {
                // LERP at double speed (from 1 to 0 is closed to open).
                IrisFrame = Math.Clamp((int)(16 - time * 16f), 0, 15);
                AfterIrisOpens = time > 0.5f;
                RisingAtom = Molecule.CreateMonoatomic(pss.processingAtoms[0]);
                if (!AfterIrisOpens)
                {
                    // if before iris opened halfway, draw rising atom under
                    Editor.RenderMolecule(RisingAtom, RisingOffset, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
                }
            }

            //iris, then lip above
            renderer.RenderUpright(Assets.textures.parts.iris_full[IrisFrame], OutputHex, Vector2.Zero);
            renderer.RenderRotating(Assets.textures.parts.animismus.output_above_iris, OutputHex, Vector2.Zero);

            // if iris half open, draw rising atom on top
            if (pss.isProcessing && AfterIrisOpens)
            {
                Editor.RenderMolecule(RisingAtom, RisingOffset, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
            }
        }

        public static void RenderPartial(Part part, Vector2 pos, SolutionEditorBase editor, PartRenderer renderer)
        {
            PartSimState pss = editor.GetSimulation().GetSimState(part);
            IntermediatePartState uco = editor.GetIntermState(part, pos);
            float time = editor.GetCycleTime();

            Vector2 centre = AntithesisPartialBase.GetHalfSizeRounded();
            renderer.RenderBase(AntithesisPartialBase, new(0, 0), centre, 0f);

            renderer.RenderRotating(AntithesisBowl, InputRightHex, Vector2.Zero);
            renderer.RenderUpright(AntithesisBowlLeft, InputRightHex, Vector2.Zero);
            // if a partial antithesis is hooked onto this, add right side symbol to right bowl
            if (editor.GetSimulation() is Sim sim && sim.GetSolution().parts.Count(foundPart => (foundPart.GetType() == SosiacElements.Parts.PartialAntithesis
                    && foundPart.InFrontBy(InputLeftHex) == part.InFrontBy(InputRightHex))) == 1)
            {
                renderer.RenderUpright(AntithesisBowlRight, InputRightHex, Vector2.Zero);
            }

            // standard iris code
            int IrisFrame = 15;
            bool AfterIrisOpens = false;
            Molecule RisingAtom = null;
            Vector2 RisingOffset = uco.pos + HexGrid.standardGrid.ToPixelCoords(OutputHex).Rotated(uco.rotation);

            //render under output iris
            renderer.RenderRotating(Assets.textures.parts.animismus.output_under_iris, OutputHex, Vector2.Zero);

            //if ejecting, render animation
            if (pss.isProcessing)
            {
                // LERP at double speed (from 1 to 0 is closed to open).
                IrisFrame = Math.Clamp((int)(16 - time * 16f), 0, 15);
                AfterIrisOpens = time > 0.5f;
                RisingAtom = Molecule.CreateMonoatomic(pss.processingAtoms[0]);
                if (!AfterIrisOpens)
                {
                    // if before iris opened halfway, draw rising atom under
                    Editor.RenderMolecule(RisingAtom, RisingOffset, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
                }
            }

            //iris, then lip above
            renderer.RenderUpright(Assets.textures.parts.iris_full[IrisFrame], OutputHex, Vector2.Zero);
            renderer.RenderRotating(Assets.textures.parts.animismus.output_above_iris, OutputHex, Vector2.Zero);

            // if iris half open, draw rising atom on top
            if (pss.isProcessing && AfterIrisOpens)
            {
                Editor.RenderMolecule(RisingAtom, RisingOffset, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
            }
        }

        public static bool OnCycleTotal(Sim sim, Part part, PartSimState pss, GlyphRecipe recipe, bool first)
        {
            SolutionEditorBase seb = sim.solutionEditor;

            if (first)
            {
                List<Part> chain = [part];

                IEnumerable<Part> nextParts = sim.GetSolution().parts.Where(foundPart => (foundPart.GetType() == SosiacElements.Parts.PartialAntithesis
                    && foundPart.InFrontBy(InputLeftHex) == chain.Last().InFrontBy(InputRightHex)));

                while (nextParts.Count() == 1)
                {
                    chain.Add(nextParts.First());
                    nextParts = sim.GetSolution().parts.Where(foundPart => (foundPart.GetType() == SosiacElements.Parts.PartialAntithesis
                        && foundPart.InFrontBy(InputLeftHex) == chain.Last().InFrontBy(InputRightHex)));
                }

                foreach (Part chainedPart in chain)
                {
                    if (sim.FindAtom(chainedPart.InFrontBy(OutputHex)).HasValue())
                    { //iris full
                        return false;
                    }
                    if (!sim.FindAtom(chainedPart.InFrontBy(InputLeftHex)).GetOrDefault(out AtomReference inputAtomA) || inputAtomA.inMultiAtomMolecule || inputAtomA.isHeldByArm)
                    { // no atom over bowl, atom over hole is part of molecule (is bonded), or atom is held
                        return false;
                    }
                    if (!sim.FindAtom(chainedPart.InFrontBy(InputRightHex)).GetOrDefault(out AtomReference inputAtomB) || inputAtomB.inMultiAtomMolecule || inputAtomB.isHeldByArm)
                    { // no atom over bowl, atom over hole is part of molecule (is bonded), or atom is held
                        return false;
                    }
                    if (!inputAtomA.atom.HasComponent("sosiac_elements:side_component") || !inputAtomB.atom.HasComponent("sosiac_elements:side_component"))
                    { // one of the inputs is not a sosiac atom
                        return false;
                    }
                }

                Vector2 centerVecLeft = HexGrid.standardGrid.ToPixelCoords(part.InFrontBy(InputLeftHex) + InputRightHex);
                seb.consumptionEffects.Add(new GlyphEffect(sim.solutionEditor, (EffectTimescaleType)1, centerVecLeft, SmokeAnim, 30, Vector2.Zero, 0));

                foreach (Part chainedPart in chain)
                {

                    if (!sim.FindAtom(chainedPart.InFrontBy(InputLeftHex)).GetOrDefault(out AtomReference inputAtomA))
                    { // no atom over bowl, even thhough i already checked. howd that happen?
                        Logger.Log("[Sosiac Elements] How did you manage that? (the atom on an antithesis glyph disappeared half-way through the OnCycle!)");
                        return false;
                    }
                    if (!sim.FindAtom(chainedPart.InFrontBy(InputRightHex)).GetOrDefault(out AtomReference inputAtomB))
                    { // no atom over bowl, even thhough i already checked. howd that happen 2 electric boogaloo
                        Logger.Log("[Sosiac Elements] How did you manage that? (the atom on an antithesis glyph disappeared half-way through the OnCycle!)");
                        return false;
                    }

                    Vector2 centerVecRight = HexGrid.standardGrid.ToPixelCoords(chainedPart.InFrontBy(InputRightHex) + InputRightHex);
                    seb.consumptionEffects.Add(new GlyphEffect(sim.solutionEditor, (EffectTimescaleType)1, centerVecRight, SmokeAnim, 30, Vector2.Zero, 0));

                    PartSimState chainedPss = sim.simulationDict[chainedPart];
                    chainedPss.isProcessing = true;
                    chainedPss.processingAtoms = [SosiacComposition.ComposeAtomTypes(inputAtomA.atomType, inputAtomB.atomType)];
                }

                if (!sim.FindAtom(part.InFrontBy(InputLeftHex)).GetOrDefault(out AtomReference inputAtomFirst))
                { // no atom over bowl, even thhough i already checked. howd that happen?
                    return false;
                }
                inputAtomFirst.molecule.RemoveAtom(inputAtomFirst.pos);
                seb.DrawFallingAtom(inputAtomFirst);

                foreach (Part chainedPart in chain)
                {
                    if (!sim.FindAtom(chainedPart.InFrontBy(InputRightHex)).GetOrDefault(out AtomReference inputAtomB))
                    { // no atom over bowl, even thhough i already checked. howd that happen 2 electric boogaloo
                        Logger.Log("[Sosiac Elements] How did you manage that? (the atom on an antithesis glyph disappeared half-way through the OnCycle!)");
                        return false;
                    }

                    inputAtomB.molecule.RemoveAtom(inputAtomB.pos);
                    seb.DrawFallingAtom(inputAtomB);
                }

                return true;
            }
            else if (pss.isProcessing)
            {   // add atom to sim on the second half-cycle
                sim.SpawnAtomRelative(pss.processingAtoms[0], part, OutputHex);
                return true;
            }
            return false;
        }

        public static bool OnCyclePartial(Sim sim, Part part, PartSimState pss, GlyphRecipe recipe, bool first)
        {
            SolutionEditorBase seb = sim.solutionEditor;

            if (first)
            {
                if (pss.isProcessing)
                {
                    return false;
                }

                List<Part> chain = [part];
                bool foundLoop = false;

                IEnumerable<Part> nextParts = sim.GetSolution().parts.Where(foundPart => (foundPart.GetType() == SosiacElements.Parts.PartialAntithesis
                    && foundPart.InFrontBy(InputLeftHex) == chain.Last().InFrontBy(InputRightHex)));

                while (nextParts.Count() == 1)
                {
                    if (nextParts.First() == part)
                    {
                        foundLoop = true;
                        break;
                    }
                    chain.Add(nextParts.First());
                    nextParts = sim.GetSolution().parts.Where(foundPart => (foundPart.GetType() == SosiacElements.Parts.PartialAntithesis
                        && foundPart.InFrontBy(InputLeftHex) == chain.Last().InFrontBy(InputRightHex)));
                }

                if (!foundLoop)
                {
                    return false;
                }

                foreach (Part chainedPart in chain)
                {
                    if (sim.FindAtom(chainedPart.InFrontBy(OutputHex)).HasValue())
                    { //iris full
                        return false;
                    }
                    if (!sim.FindAtom(chainedPart.InFrontBy(InputLeftHex)).GetOrDefault(out AtomReference inputAtomA) || inputAtomA.inMultiAtomMolecule || inputAtomA.isHeldByArm)
                    { // no atom over bowl, atom over hole is part of molecule (is bonded), or atom is held
                        return false;
                    }
                    if (!sim.FindAtom(chainedPart.InFrontBy(InputRightHex)).GetOrDefault(out AtomReference inputAtomB) || inputAtomB.inMultiAtomMolecule || inputAtomB.isHeldByArm)
                    { // no atom over bowl, atom over hole is part of molecule (is bonded), or atom is held
                        return false;
                    }
                    if (!inputAtomA.atom.HasComponent("sosiac_elements:side_component") || !inputAtomB.atom.HasComponent("sosiac_elements:side_component"))
                    { // one of the inputs is not a sosiac atom
                        return false;
                    }
                }


                foreach (Part chainedPart in chain)
                {

                    if (!sim.FindAtom(chainedPart.InFrontBy(InputLeftHex)).GetOrDefault(out AtomReference inputAtomA))
                    { // no atom over bowl, even thhough i already checked. howd that happen?
                        Logger.Log("[Sosiac Elements] How did you manage that? (the atom on an antithesis glyph disappeared half-way through the OnCycle!)");
                        return false;
                    }
                    if (!sim.FindAtom(chainedPart.InFrontBy(InputRightHex)).GetOrDefault(out AtomReference inputAtomB))
                    { // no atom over bowl, even thhough i already checked. howd that happen 2 electric boogaloo
                        Logger.Log("[Sosiac Elements] How did you manage that? (the atom on an antithesis glyph disappeared half-way through the OnCycle!)");
                        return false;
                    }

                    Vector2 centerVecRight = HexGrid.standardGrid.ToPixelCoords(chainedPart.InFrontBy(InputRightHex) + InputRightHex);
                    seb.consumptionEffects.Add(new GlyphEffect(sim.solutionEditor, (EffectTimescaleType)1, centerVecRight, SmokeAnim, 30, Vector2.Zero, 0));

                    PartSimState chainedPss = sim.simulationDict[chainedPart];
                    chainedPss.isProcessing = true;
                    Logger.Log(pss.isProcessing);
                    chainedPss.processingAtoms = [SosiacComposition.ComposeAtomTypes(inputAtomA.atomType, inputAtomB.atomType)];
                }

                foreach (Part chainedPart in chain)
                {
                    if (!sim.FindAtom(chainedPart.InFrontBy(InputRightHex)).GetOrDefault(out AtomReference inputAtomB))
                    { // no atom over bowl, even thhough i already checked. howd that happen 2 electric boogaloo
                        Logger.Log("[Sosiac Elements] How did you manage that? (the atom on an antithesis glyph disappeared half-way through the OnCycle!)");
                        return false;
                    }

                    inputAtomB.molecule.RemoveAtom(inputAtomB.pos);
                    seb.DrawFallingAtom(inputAtomB);
                }
                return true;
            }
            else if (pss.isProcessing)
            {   // add atom to sim on the second half-cycle
                sim.SpawnAtomRelative(pss.processingAtoms[0], part, OutputHex);
                return true;
            }
            return false;
        }
    }
}