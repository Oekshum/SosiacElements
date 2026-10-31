using Quintessential;
using Quintessential.Components;
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
            QApi.AddComponentToAtom("sosiac_elements:cognin",  atom => new SosiacSideComponent(0, 0));
            QApi.AddComponentToAtom("sosiac_elements:scion",   atom => new SosiacSideComponent(0, 1));
            QApi.AddComponentToAtom("sosiac_elements:pseudon", atom => new SosiacSideComponent(0, 2));
            QApi.AddComponentToAtom("sosiac_elements:aestha",  atom => new SosiacSideComponent(1, 0));
            QApi.AddComponentToAtom("sosiac_elements:synthro", atom => new SosiacSideComponent(1, 1));
            QApi.AddComponentToAtom("sosiac_elements:algia",   atom => new SosiacSideComponent(1, 2));
            QApi.AddComponentToAtom("sosiac_elements:certas",  atom => new SosiacSideComponent(2, 0));
            QApi.AddComponentToAtom("sosiac_elements:servas",  atom => new SosiacSideComponent(2, 1));
            QApi.AddComponentToAtom("sosiac_elements:physos",  atom => new SosiacSideComponent(2, 2));

            QApi.AddComponentToAtomOnReplace("sosiac_elements:cognin",  (atom, _) => new SosiacSideComponent(0, 0));
            QApi.AddComponentToAtomOnReplace("sosiac_elements:scion",   (atom, _) => new SosiacSideComponent(0, 1));
            QApi.AddComponentToAtomOnReplace("sosiac_elements:pseudon", (atom, _) => new SosiacSideComponent(0, 2));
            QApi.AddComponentToAtomOnReplace("sosiac_elements:aestha",  (atom, _) => new SosiacSideComponent(1, 0));
            QApi.AddComponentToAtomOnReplace("sosiac_elements:synthro", (atom, _) => new SosiacSideComponent(1, 1));
            QApi.AddComponentToAtomOnReplace("sosiac_elements:algia",   (atom, _) => new SosiacSideComponent(1, 2));
            QApi.AddComponentToAtomOnReplace("sosiac_elements:certas",  (atom, _) => new SosiacSideComponent(2, 0));
            QApi.AddComponentToAtomOnReplace("sosiac_elements:servas",  (atom, _) => new SosiacSideComponent(2, 1));
            QApi.AddComponentToAtomOnReplace("sosiac_elements:physos",  (atom, _) => new SosiacSideComponent(2, 2));
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
