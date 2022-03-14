using Android.App;
using Android.App.Job;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;

namespace JobSchedulerEx
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button _startButton;
        private Button _stopButton;
        private JobInfo.Builder _jobBuilder;
        private JobScheduler _jobScheduler;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            _jobScheduler = (JobScheduler)GetSystemService(JobSchedulerService);
            UIReferences();
            UIClickEvents();
        }

        private void UIClickEvents()
        {
            _startButton.Click += _startButton_Click;
            _stopButton.Click += _stopButton_Click;
        }

        private void _stopButton_Click(object sender, EventArgs e)
        {
            _jobScheduler.Cancel(1);
            Toast.MakeText(this, "Job Stopped", ToastLength.Short).Show();

        }

        private void CreateJobInfoBuilder()
        {
            _jobBuilder = this.CreateJobBuilderUsingJobId<UploadImageJobServices>(1)
                .SetPeriodic(15 * 60 * 1000)
                .SetRequiredNetworkType(NetworkType.Unmetered);

        }



        private void _startButton_Click(object sender, EventArgs e)
        {
            CreateJobInfoBuilder();

            JobInfo jobInfo = _jobBuilder.Build();
            var scheduleResult = _jobScheduler.Schedule(jobInfo);
            if (JobScheduler.ResultSuccess == scheduleResult)
            {

                Toast.MakeText(this, "Job Scheduled Sucessfully", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(this, "Job Failed", ToastLength.Short).Show();
            }
        }

        private void UIReferences()
        {
            _startButton = FindViewById<Button>(Resource.Id.buttonStratJob);
            _stopButton = FindViewById<Button>(Resource.Id.buttonStopJob);

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}