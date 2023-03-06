using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit
{
    public class DamageEP : EffectPresentor
    {
        public DamageEP(IUnitModel unit, EffectModel model) : base(unit, model) { }


        public override void Update()
        {
            MakeAction();
            EndAction();
        }

        public override void MakeAction()
        {
            int value = (int)model.Settings.Value;
            player.Health.Damage(value);
        }
    }
}
