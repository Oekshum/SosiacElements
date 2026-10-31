using Quintessential;
using System;
using System.Collections.Generic;
using System.Text;

namespace SosiacElements
{
    internal class Atoms
    {
        public static AtomType cognin, scion, pseudon, aestha, synthro, algia, certas, servas, physos;

        private static AtomType MakeAtomType(string name)
        {
            return new()
            {
                symbol = AssetLoaderHelper.LoadTexture("textures/sosiac_elements/atoms/null_symbol"),
                shadow = Assets.textures.atoms.shadow,
                matteTextures = new()
                {
                    /*
                    diffuse = AssetLoaderHelper.LoadTexture("textures/fanolytics/atoms/" + name + "_diffuse"),
                    shade = AssetLoaderHelper.LoadTexture("textures/fanolytics/atoms/" + name + "_shade")
                    */
                    diffuse = AssetLoaderHelper.LoadTexture("textures/sosiac_elements/atoms/sosiac_diffuse"),
                    shade = Assets.textures.atoms.mors_shade
                }
            };
        }

        public static void LoadAtomTypes()
        {
            cognin = MakeAtomType("cognin");
            scion = MakeAtomType("scion");
            pseudon = MakeAtomType("pseudon");
            aestha = MakeAtomType("aestha");
            synthro = MakeAtomType("synthro");
            algia = MakeAtomType("algia");
            certas = MakeAtomType("certas");
            servas = MakeAtomType("servas");
            physos = MakeAtomType("physos");

            SosiacElements.Instance.AddAtomType(cognin, "cognin");
            SosiacElements.Instance.AddAtomType(scion, "scion");
            SosiacElements.Instance.AddAtomType(pseudon, "pseudon");
            SosiacElements.Instance.AddAtomType(aestha, "aestha");
            SosiacElements.Instance.AddAtomType(synthro, "synthro");
            SosiacElements.Instance.AddAtomType(algia, "algia");
            SosiacElements.Instance.AddAtomType(certas, "certas");
            SosiacElements.Instance.AddAtomType(servas, "servas");
            SosiacElements.Instance.AddAtomType(physos, "physos");
    }
    }
}
