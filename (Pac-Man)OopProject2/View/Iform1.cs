using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Pac_Man_OopProject2.View
{
    public interface Iform1
    {
            void InitializeGameUI();
            void UpdateScore(int score);
            void ShowStartScreen();
            void HideStartScreen();
    }
}
