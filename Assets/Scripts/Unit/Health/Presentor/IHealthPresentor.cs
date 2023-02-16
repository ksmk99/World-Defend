using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit
{
    public interface IHealthPresentor
    {
        void Heal(int count);
        void Damage(int count);
        void AutoHeal();
        bool IsDeath();
    }
}
