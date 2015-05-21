using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.XXXX.Common
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> where T : new()
    {
        private Singleton()
        {
        }

        private static T instance;
        /// <summary>
        /// 每个传入的消息由同一个对象实例提供服务。
        /// </summary>
        public static T Instance
        {
            get
            {
                if (null == instance)
                    instance = new T();
                return instance;
            }
        }
    }
}
