using Quartz;
using Quartz.Impl;
using System.Configuration;

namespace Caracolknits.ScheduledTasks.services.Helpers
{
    public class GlobalJobListener : IJobListener
    {
        public string Name
        {
            get
            {
                return "GlobalJobListener";
            }
        }

        public async Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            await UpdateAssemblyScheduler(context, "ExecutionVetoed");
        }

        public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            await UpdateAssemblyScheduler(context, "ToBeExecuted");
        }

        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default(CancellationToken))
        {
            await UpdateAssemblyScheduler(context, "WasExecuted");
        }

        private async Task UpdateAssemblyScheduler(IJobExecutionContext context, string status)
        {
            JobExecutionContextImpl contextImp = (JobExecutionContextImpl)context;
            string name = contextImp.JobDetail.Key.Name;

            string columnUpdate = string.Empty;

            switch (status)
            {
                case "ExecutionVetoed":
                    columnUpdate = "Date";
                    break;

                case "ToBeExecuted":
                    columnUpdate = "ToBeExecutedDate";
                    break;

                case "WasExecuted":

                    columnUpdate = "WasExecutedDate";
                    break;

                case "VETOJOB":
                    columnUpdate = "VetoJobExecutionDate";
                    break;

                default:
                    break;
            }

            // string connectionStringName = ConfigurationManager.ConnectionStrings["ScheduledTaskConnectionString"].ToString();
            // Dictionary<string, object> parameters = new Dictionary<string, object>
            // {
            //     ["@Name"] = name,
            //     ["@Status"] = status,
            //     [columnUpdate] = DateTime.Now,
            // };
            // SqlCommandInfo sqlCommandInfo = new SqlCommandInfo.Builder()
            //                                                .WithNameOrConnectionString(connectionStringName)
            //                                                .WithSqlQuery($"UPDATE AssemblySchedulers SET Status = @Status,{columnUpdate} = @{columnUpdate} WHERE Name = @Name")
            //                                                .WithParameters(parameters)
            //                                                .Build();

            // await RawDbConnectionFactory.Create().ExecuteQueryAsync(sqlCommandInfo);
        }
    }
}