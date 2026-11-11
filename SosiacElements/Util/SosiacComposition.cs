using Quintessential;
using System;
using System.Collections.Generic;

namespace SosiacElements.Util
{
    internal class SosiacComposition
    {
        public static Dictionary<(int, int), AtomType> pairToSosiacAtom = [];
        public static Dictionary<AtomType, (int, int)> sosiacAtomToPair = [];
        public static ((int, int), AtomType)[] ComponentPairs = [
            ((0,0), Atoms.cognin),
            ((1,0), Atoms.scion),
            ((2,0), Atoms.pseudon),
            ((0,1), Atoms.aestha),
            ((1,1), Atoms.synthro),
            ((2,1), Atoms.algia),
            ((0,2), Atoms.certas),
            ((1,2), Atoms.servas),
            ((2,2), Atoms.physos)
            ];

        public static void DictionaryInit()
        {
            foreach (((int, int), AtomType) pair in ComponentPairs)
            {
                pairToSosiacAtom.Add(pair.Item1, pair.Item2);
                sosiacAtomToPair.Add(pair.Item2, pair.Item1);
            }
        }

        public static AtomType ComposeAtomTypes(AtomType atom1, AtomType atom2)
        {
            if (!sosiacAtomToPair.TryGetValue(atom1, out (int, int) pair1) || !sosiacAtomToPair.TryGetValue(atom2, out (int, int) pair2))
            {
                SosiacElements.Instance.Log.Error("tried to compose " + atom1.name + " and " + atom2.name);
                throw new ArgumentException("Both atoms composed must be Sosiac");
            }
            return pairToSosiacAtom[(pair1.Item2, pair2.Item1)];
        }

        public static bool IsSosiac(AtomType type)
        {
            return sosiacAtomToPair.ContainsKey(type);
        }

        private static int mod(int x, int m)
        {
            return (x % m + m) % m;
        }

        public static (int, int) ShiftSosiac((int, int) inputPair, int x, int y)
        {
            return (mod(inputPair.Item1 + x, 3), mod(inputPair.Item2 + y, 3));
        }

        public static AtomType ShiftSosiac(AtomType inputType, int x, int y)
        {
            return pairToSosiacAtom[ShiftSosiac(sosiacAtomToPair[inputType], x, y)];
        }
    }
}
