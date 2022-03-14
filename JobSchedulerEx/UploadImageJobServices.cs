using Android.App;
using Android.App.Job;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JobSchedulerEx
{
    [Service(Name = "com.companyname.jobserviceex.BackgroundJob.UploadImageJobServices", Permission = "android.permission.BIND_JOB_SERVICE")]
    class UploadImageJobServices : JobService
    {
        private const string TAG = "UploadImageJob";
        private bool isJobCancelled = false;
        public override bool OnStartJob(JobParameters jobparams)
        {
            UploadImagesToTheServer(jobparams);
            return true;
        }

        private void UploadImagesToTheServer(JobParameters jobparams)
        {
            var loopCount = 10;

            Task.Run(() =>
            {

                for (int i = 0; i < loopCount; i++)
                {
                    if (isJobCancelled)
                    {
                        return;

                    }

                    Log.Debug(TAG, $"job Started: Image {i} uploaded successfully");
                    Thread.Sleep(500);

                }


            });
        }

        public override bool OnStopJob(JobParameters jobparams)
        {
            isJobCancelled = true;
            Log.Debug(TAG, $"Job Stopped");

            return true;
        }
    }
}