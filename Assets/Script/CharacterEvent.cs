using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;
using UnityEngine;

namespace Assets.Script
{
    public class CharacterEvent
    {
       //character damaged and damage value
        public static UnityAction<GameObject, int> characterDamaged;

        //character healed and amount healed
        public static UnityAction<GameObject, int> characterHealed;
    }
}
