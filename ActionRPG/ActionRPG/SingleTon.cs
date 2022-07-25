using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionRPG
{
    public class SingleTon<T> where T : class, new()
    {
        private static volatile T instance = null;
        private static object lockObj = new object();

        public static T Instance
        {
            get
            {
                if(instance == null)
                {
                    lock(lockObj)
                    {
                        if(instance == null)
                        {
                            instance = new T();
                        }
                        return instance;
                    }
                }
                return instance;
            }
        }
    }
}
