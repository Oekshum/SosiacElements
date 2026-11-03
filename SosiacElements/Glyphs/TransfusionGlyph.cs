using Quintessential;
using SosiacElements.Util;

namespace Glyphs
{
    internal class TransfusionGlyph
    {
        public static HexIndex InputHex = new(0, 0);
        public static HexIndex BowlHex = new(1, 0);

        public static Texture TransfusionBase = AssetLoaderHelper.LoadTexture("textures/sosiac_elements/parts/transfusion_base");
        public static Texture TransfusionBowl = Assets.textures.parts.calcinator_bowl;
        public static Texture[] GlowAnim = Assets.textures.parts.rejection_glyph_flash;

        public static void Render(Part part, Vector2 pos, SolutionEditorBase editor, PartRenderer renderer)
        {
            PartSimState pss = editor.GetSimulation().GetSimState(part);
            IntermediatePartState uco = editor.GetIntermState(part, pos);

            //render base
            Vector2 centre = TransfusionBase.GetHalfSizeRounded() - new Vector2(41, 0); 
            renderer.RenderBase(TransfusionBase, new(0,0), centre, 0f);

            //render bowl
            renderer.RenderRotating(TransfusionBowl, BowlHex, new(0, 0));

            //render input
            renderer.RenderShadowCircle(Assets.textures.parts.projection_glyph.quicksilver_input, InputHex, 0);

            //render under output iris
            renderer.RenderRotating(Assets.textures.parts.animismus.output_under_iris, InputHex, Vector2.Zero);

            //iris, then lip above
            renderer.RenderRotating(Assets.textures.parts.animismus.output_above_iris, InputHex, Vector2.Zero);
        }

        public static bool OnCycle(Sim sim, Part part, PartSimState pss, GlyphRecipe recipe, bool first)
        {
            if (first)
            {
                SolutionEditorBase seb = sim.solutionEditor;

                if (!sim.FindAtom(part.InFrontBy(BowlHex)).GetOrDefault(out AtomReference bowlAtom))
                { //bowl empty
                    return false;
                }
                if (!sim.FindAtom(part.InFrontBy(InputHex)).GetOrDefault(out AtomReference inputAtom) || inputAtom.inMultiAtomMolecule || inputAtom.isHeldByArm)
                { // no atom over bowl, atom over hole is part of molecule (is bonded), or atom is held
                    return false;
                }

                if (!SosiacComposition.IsSosiac(bowlAtom.atomType))
                { // bowl atom is not sosiac, cannot be transmuted
                    return false;
                }

                AtomType transfusedType;
                if (inputAtom.atomType == AtomTypes.vitae)
                { //if vitae input, shift down-right
                    transfusedType = SosiacComposition.ShiftSosiac(bowlAtom.atomType, 1, -1);
                } 
                else if (inputAtom.atomType == AtomTypes.mors)
                { //if mors input, shift up-left
                    transfusedType = SosiacComposition.ShiftSosiac(bowlAtom.atomType, -1, 1);
                } else
                {
                    return false;
                }   

                bowlAtom.molecule.ReplaceAtom(transfusedType, bowlAtom.pos);
                Vector2 centerVecLeft = HexGrid.standardGrid.ToPixelCoords(part.InFrontBy(BowlHex));
                bowlAtom.atom.transmutationEffect = new TransmutationEffect(seb, TransmutationEffectRenderMode.AsEffect, bowlAtom.atomType, GlowAnim, 10f);

                inputAtom.molecule.RemoveAtom(inputAtom.pos);
                seb.DrawFallingAtom(inputAtom);

                pss.isProcessing = true;
                return true;
            }

            return false;
        }
    }
}
