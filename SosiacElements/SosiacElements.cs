using Quintessential;
using SosiacElements.Components;
using SosiacElements.Util;

namespace SosiacElements;

internal class SosiacElements : QuintessentialMod, IDataMod {
    public static SosiacElements Instance { get; }
    public override string ModId => "sosiac_elements";

    public override void Load() { }

    public override void LoadContent() {
        Atoms.LoadAtomTypes();
        SosiacSideComponent.Register();
        SosiacComposition.DictionaryInit();
        Parts.LoadPartTypes();
    }
    public override void LoadCompatContent() { }
    public override void FinaliseContent() { }

    public override void PostLoad() { }
	public override void Unload() { }
}
