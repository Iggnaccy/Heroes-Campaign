using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache 
{
    public static Dictionary<DictKey, Sprite> PortraitHolder;


    public  static Sprite GetPortrait(Hero.HeroSex sex, Hero.HeroRace race, Hero.HeroProfession profession)
    {
        Sprite TemporarySprite;
        DictKey TemporaryDictKey;
        if (PortraitHolder == null)
            PortraitHolder = new Dictionary<DictKey, Sprite>();
        TemporaryDictKey = new DictKey(sex, race, profession);
        if (!PortraitHolder.ContainsKey(TemporaryDictKey))
        {
            TemporarySprite = Resources.Load<Sprite>("Portraits/" + sex.ToString() + race.ToString() + profession.ToString());
            PortraitHolder.Add(TemporaryDictKey, TemporarySprite);
        }
        return  PortraitHolder[TemporaryDictKey];
        
    }

    public class DictKey
    {
        Hero.HeroSex KeySex;
        Hero.HeroRace KeyRace;
        Hero.HeroProfession KeyProfession;

        public DictKey(Hero.HeroSex keySex, Hero.HeroRace keyRace, Hero.HeroProfession keyProfession)
        {
            KeySex = keySex;
            KeyRace = keyRace;
            KeyProfession = keyProfession;
        }
    }

}
