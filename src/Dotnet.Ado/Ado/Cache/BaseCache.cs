using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dotnet.Ado.Cache
{
    /// <summary>
    /// 通用缓存类 
    /// </summary>
    public class BaseCache<TKey, TValue> : IBaseCache<TKey, TValue>
    {
        /// <summary>
        /// 缓存数据键值对
        /// </summary>
        private Dictionary<TKey, CacheExpire<TValue>> cacheDatas;

        private object locker = new object();

        /// <summary>
        /// 定时器
        /// </summary>
        public Timer CacheTimer { get; set; }

        public BaseCache()
        {
            if (this.IsExpireRemove)
            {
                CacheTimer = new System.Threading.Timer(new TimerCallback(TimerCall), this, 1 * 30 * 1000, this.PeriodTime);
            }
            this.cacheDatas = new Dictionary<TKey, CacheExpire<TValue>>();
        }

        public void Clear()
        {
            lock (this.locker)
            {
                this.cacheDatas.Clear();
            }
        }

        public bool Exists(TKey key)
        {
            //T local;
            CacheExpire<TValue> local;
            if (key == null)
            {
                return false;
            }
            return this.cacheDatas.TryGetValue(key, out local);
        }

        public TValue Get(TKey key)
        {
            TValue local = default(TValue);
            CacheExpire<TValue> data;
            if (key == null)
            {
                return default(TValue);
            }
            if (!this.cacheDatas.TryGetValue(key, out data) && this.IsGetExternalData)
            {
                local = this.LoadData(key);
                if (local != null)
                {
                    this.Insert(key, local);
                }
            }
            if (data != null)
            {
                if (this.IsExpireRemove)
                {
                    if (data.VisitTime.AddMilliseconds(this.ExpireTime) > DateTime.Now)//没有过期
                    {
                        return data.Data;
                    }
                    else//过期
                    {
                        this.Remove(key);
                        return local;
                    }
                }
                //data.VisitTime = DateTime.Now;
                return data.Data;
            }
            else
            {
                return local;
            }
        }

        public void Insert(TKey key, TValue value)
        {
            if ((key != null) && (value != null))
            {
                Remove(key);
                lock (this.locker)
                {
                    CacheExpire<TValue> data = new CacheExpire<TValue>(value, DateTime.Now);
                    this.cacheDatas[key] = data;
                }
            }
        }

        protected virtual TValue LoadData(TKey key)
        {
            throw new Exception("请重载缓存加载数据方法！");
        }

        public void ReLoadData(TKey key)
        {
            Remove(key);
            Get(key);
        }

        public void ReLoadData()
        {
            Clear();
        }

        public void Remove(TKey key)
        {
            if (key != null)
            {
                lock (this.locker)
                {
                    this.cacheDatas.Remove(key);
                }
            }
        }

        protected void TimerCall(object obj)
        {
            TKey[] keys = cacheDatas.Keys.ToArray();
            foreach (TKey key in keys)
            {
                CacheExpire<TValue> data = cacheDatas[key];
                if (data != null)
                {
                    TimeSpan expire = DateTime.Now - data.VisitTime;
                    if (expire.TotalMilliseconds > ExpireTime)
                    {
                        lock (locker)
                        {
                            if (cacheDatas.ContainsKey(key))
                            {
                                this.cacheDatas.Remove(key);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 是否可以获取外部数据
        /// </summary>
        public virtual bool IsGetExternalData
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 是否做过期删除判断
        /// </summary>
        public virtual bool IsExpireRemove
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 过期时间默认60分钟 单位毫秒
        /// </summary>
        public virtual int ExpireTime
        {
            get { return 60 * 60 * 1000; }
        }

        /// <summary>
        /// 缓存间隔检查时间 默认30分钟 单位毫秒
        /// </summary>
        public virtual int PeriodTime
        {
            get { return 30 * 60 * 1000; }
        }

    }
}
