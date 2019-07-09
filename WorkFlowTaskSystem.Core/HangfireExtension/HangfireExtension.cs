using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.BackgroundJobs;
using HangfireBackgroundJob = Hangfire.BackgroundJob;
using HangfireRecurringJob = Hangfire.RecurringJob;
namespace WorkFlowTaskSystem.Core.HangfireExtension
{
   public static class HangfireExtension
    {
        /// <summary>
        /// 延续性任务执行(Continuations)
        /// </summary>
        /// <typeparam name="TJob"></typeparam>
        /// <typeparam name="TArgs"></typeparam>
        /// <param name="backgroundJobManager"></param>
        /// <param name="args"></param>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public static Task<string> EnqueueAsync<TJob, TArgs>(this IBackgroundJobManager backgroundJobManager, TArgs args, string jobId) where TJob : IBackgroundJob<TArgs>
        {
            string jobUniqueIdentifier = string.Empty;
            jobUniqueIdentifier=HangfireBackgroundJob.ContinueWith<TJob>(jobId, job => job.Execute(args));
            return Task.FromResult(jobUniqueIdentifier);
        }
        /// <summary>
        /// 定时任务执行(Recurring jobs)
        /// </summary>
        /// <typeparam name="TJob"></typeparam>
        /// <typeparam name="TArgs"></typeparam>
        /// <param name="backgroundJobManager"></param>
        /// <param name="args"></param>
        /// <param name="cronExpression"></param>
        /// <param name="timeZone"></param>
        /// <param name="queue"></param>
        /// <returns></returns>
        public static Task EnqueueAsync<TJob, TArgs>(this IBackgroundJobManager backgroundJobManager, TArgs args, string cronExpression, TimeZoneInfo timeZone = null, string queue = "default") where TJob : IBackgroundJob<TArgs>
        {
            HangfireRecurringJob.AddOrUpdate<TJob>(job => job.Execute(args), cronExpression, timeZone, queue);
            return Task.CompletedTask;
        }
    }
}
