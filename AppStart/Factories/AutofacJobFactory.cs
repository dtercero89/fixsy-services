using Autofac;
using Quartz;
using Quartz.Simpl;
using Quartz.Spi;

namespace FixsyWebApi.AppStart.Factories
{
    public class AutofacJobFactory : IJobFactory
    {
        private readonly IContainer _container;

        public AutofacJobFactory(IContainer container)
        {
            _container = container;
        }

         public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return (IJob)_container.Resolve(bundle.JobDetail.JobType);
        }

        public void ReturnJob(IJob job)
        {
              var disposable = job as IDisposable;  
                disposable?.Dispose();  
        }
    }
}