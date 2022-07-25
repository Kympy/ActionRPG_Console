using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionRPG
{
    internal class MainFunction
    {
        static void Main()
        {
            GameManager gameManager = new GameManager();
            gameManager.Awake();
            gameManager.Start();
            gameManager.Update();
        }
    }
}
