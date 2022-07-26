using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionRPG
{
    public class MainFunction
    {
        static void Main()
        {
            GameManager.Instance.Awake();
            GameManager.Instance.Start();
            GameManager.Instance.Update();
        }
    }
}
