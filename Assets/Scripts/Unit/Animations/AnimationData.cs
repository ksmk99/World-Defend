using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Unit
{
    public class AnimationData 
    {
        public string Death => "Death";
        public string Respawn => "Respawn";
        public string IsMoving => "IsMoving";
        public string ShootTrigger => "Shoot";

        private Dictionary<string, int> idDictionary;

        public int GetAnimationID(string name)
        {
            if(idDictionary == null)
            {
                Init();
            }

            if(!idDictionary.ContainsKey(name))
            {
                throw new ArgumentException();
            }

            return idDictionary[name];
        }

        private void Init()
        {
            idDictionary = new Dictionary<string, int>()
           {
               { Death, Animator.StringToHash(Death) },
               { Respawn, Animator.StringToHash(Respawn) },
               { IsMoving, Animator.StringToHash(IsMoving) },
               { ShootTrigger, Animator.StringToHash(ShootTrigger) }
           };
        }
    }
}
