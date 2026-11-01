using Quintessential;
using Quintessential.Components;
using SosiacElements.Util;
using System;

namespace SosiacElements.Components
{
    public class SosiacSideComponent(int i, int j) : IAtomComponent
    {
        public static readonly Identifier Id = "sosiac_elements:side_component";
        Identifier IComponentBase<Atom>.Id => Id;

        public Atom self = null;
        public int first = i;
        public int second = j;

        private static Random random = new(123456);
        public int time = random.GetInt(1,1000);
        private static readonly Texture[] leftTextures = [AssetLoaderHelper.LoadTexture("textures/sosiac_elements/atoms/left_symbol_1"),
                                                          AssetLoaderHelper.LoadTexture("textures/sosiac_elements/atoms/left_symbol_2"),
                                                          AssetLoaderHelper.LoadTexture("textures/sosiac_elements/atoms/left_symbol_3")];
        private static readonly Texture[] rightTextures = [AssetLoaderHelper.LoadTexture("textures/sosiac_elements/atoms/right_symbol_1"),
                                                           AssetLoaderHelper.LoadTexture("textures/sosiac_elements/atoms/right_symbol_2"),
                                                           AssetLoaderHelper.LoadTexture("textures/sosiac_elements/atoms/right_symbol_3")];

        private static ((int, int), AtomType)[] componentIndicesList = SosiacComposition.ComponentPairs;

        public void OnBind(Atom self, Molecule arg1) => this.self = self;
        public void OnReplace(AtomType newType) => self.RemoveComponent(Id);
        public void OnUnbind(Atom self, Molecule arg1) => this.self = null;

        public Vector2 OffsetForSymbol(int type, bool isLeft)
        {
            float x = (float)Math.Sin(.005 * (9  + type) * time) * (isLeft ? -1 : 1);
            float y = (float)Math.Cos(.005 * (12 + type) * time);
            return 1.5f * new Vector2(x,y);
        }

        public static void Register()
        { // Called in LoadContent();
            foreach (((int, int), AtomType) pair in componentIndicesList)
            {
                QApi.AddComponentToAtom(pair.Item2.Id, atom => new SosiacSideComponent(pair.Item1.Item1, pair.Item1.Item2));
                QApi.AddComponentToAtomOnReplace(pair.Item2.Id, (atom, _) => new SosiacSideComponent(pair.Item1.Item1, pair.Item1.Item2));
            }
        }

        void IAtomComponent.OnRender(Action doRender, ref AtomType type, ref Vector2 translation, ref float scaleMultiplier, ref float opacityMultiplier, ref float height, ref float shadowStrength, ref float shadowOffset, ref float shadowAngle, Texture shadow, Texture overlayEffect, bool isOutputRender, SolutionEditorBase solutionEditor)
        {
            doRender();
            TextureRenderer.Render(leftTextures[first], translation - new Vector2(30f, 30f) + OffsetForSymbol(first, true));
            TextureRenderer.Render(rightTextures[second], translation - new Vector2(30f, 30f) + OffsetForSymbol(second, false));
            time++;
        }

        public CycleEvent.CycleEventExecutionType CallbackType => CycleEvent.CycleEventExecutionType.None;

        public void OnClone(ref Atom cloned) { }

        public void OnRemoveFromMolecule(Molecule molecule, HexIndex hexPos) { }

        public void OnAddToMolecule(Molecule molecule, HexIndex hexPos) { }

        public void OnCycleCallback(Sim sim, CycleEvent.CycleEventExecutionType executionType) { }
    }
}
