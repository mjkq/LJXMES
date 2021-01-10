using DataHelp;
using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Business
{
    public class Test : IJob, IRegisteredObject
    {
        private readonly object _lockMin = new object();
        private bool _shuttingDown;
        public void Execute()
        {
            lock (_lockMin)
            {
                if (_shuttingDown)
                {
                    return;
                }
                SQLHelper sql = new SQLHelper("DefaultConnection");
                string sqlText = "select id from [dbo].[Business] where CODE='Test'";
                string id = sql.ExecuteReader(sqlText);
                log4net.ILog log = log4net.LogManager.GetLogger("Test");//获取一个日志记录器
                log.Info("测试：" + DateTime.Now + ",id:" + id);//写入一条新log
            }
        }

        public Test()
        {
            // Register this job with the hosting environment.
            // Allows for a more graceful stop of the job, in the case of IIS shutting down.
            HostingEnvironment.RegisterObject(this);
        }
        public void Stop(bool immediate)
        {
            // Locking here will wait for the lock in Execute to be released until this code can continue.
            lock (_lockMin)
            {
                _shuttingDown = true;
            }

            HostingEnvironment.UnregisterObject(this);
        }
    }
}
