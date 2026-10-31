using Quintessential;
using System;
using System.Collections.Generic;

namespace SosiacElements.Util
{
    internal class SosiacComposition
    {
        public static Dictionary<(int, int), AtomType> pairToSosiacAtom = [];
        public static Dictionary<AtomType, (int, int)> sosiacAtomToPair = [];
        private static ((int, int), AtomType)[] ListOfPairs = [
            ((0,0), Atoms.cognin),
            ((0,1), Atoms.scion),
            ((0,2), Atoms.pseudon),
            ((1,0), Atoms.aestha),
            ((1,1), Atoms.synthro),
            ((1,2), Atoms.algia),
            ((2,0), Atoms.certas),
            ((2,1), Atoms.servas),
            ((2,2), Atoms.physos)
            ];

        public static void DictionaryInit()
        {
            foreach (((int, int), AtomType) pair in ListOfPairs)
            {
                pairToSosiacAtom.Add(pair.Item1, pair.Item2);
                sosiacAtomToPair.Add(pair.Item2, pair.Item1);
            }
        }

        public static AtomType ComposeAtomTypes(AtomType atom1, AtomType atom2)
        {
            if (!sosiacAtomToPair.TryGetValue(atom1, out (int, int) pair1) || !sosiacAtomToPair.TryGetValue(atom2, out (int, int) pair2))
            {
                Logger.Log("tried to compose " + atom1.name + " and " + atom2.name);
                throw new ArgumentException("Both atoms composed must be Sosiac");
            }
            return pairToSosiacAtom[(pair1.Item2, pair2.Item1)];
        }
    }
}
