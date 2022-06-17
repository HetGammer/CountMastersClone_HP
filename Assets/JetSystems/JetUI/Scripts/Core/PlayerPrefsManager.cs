using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JetSystems
{

    public static class PlayerPrefsManager
    {
        static string COINSKEY = "COINS";
        static string ITEMUNLOCKEDKEY = "ITEMUNLOCKED";
        static string SOUNDKEY = "SOUNDS";
        static string LEVELKEY = "LEVEL";



        public static int GetCoins()
        { return PlayerPrefs.GetInt(COINSKEY); }

        public static void SaveCoins(int coinsAmount)
        { PlayerPrefs.SetInt(COINSKEY, coinsAmount); }





        public static int GetItemUnlockedState(int itemIndex)
        { return PlayerPrefs.GetInt(ITEMUNLOCKEDKEY + itemIndex); }

        public static void SetItemUnlockedState(int itemIndex, int state)
        { PlayerPrefs.SetInt(ITEMUNLOCKEDKEY + itemIndex, state); }




        public static int GetSoundState()
        { return PlayerPrefs.GetInt(SOUNDKEY); }

        public static void SetSoundState(int state)
        { PlayerPrefs.SetInt(SOUNDKEY, state); }







        public static int GetLevel()
        { return PlayerPrefs.GetInt(LEVELKEY); }

        public static void SaveLevel(int level)
        { PlayerPrefs.SetInt(LEVELKEY, level); }
    }
}
