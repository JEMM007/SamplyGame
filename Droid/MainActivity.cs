using Android.App;
using Android.Content.PM;
using Android.Widget;
using Android.OS;
using Android.Views;
using Urho.Droid;
using Android.Hardware;

namespace SamplyGame.Droid
{
	[Activity(Label = "SamplyGame", MainLauncher = true, 
		Icon = "@drawable/icon", Theme = "@android:style/Theme.NoTitleBar.Fullscreen",
		ConfigurationChanges = ConfigChanges.KeyboardHidden | ConfigChanges.Orientation,
		ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : Activity, ISensorEventListener
    {

        static readonly object _syncLock = new object();
        SensorManager _sensorManager;



        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {
            // We don't want to do anything here.
        }


        protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			var mLayout = new AbsoluteLayout(this);
			var surface = UrhoSurface.CreateSurface<SamplyGame>(this);
            _sensorManager = (SensorManager)GetSystemService(SensorService);
            mLayout.AddView(surface);
			SetContentView(mLayout);

           

        }

        public void OnSensorChanged(SensorEvent e)
        {
            lock (_syncLock)
            {
                if (Urho.Application.HasCurrent)
                { 
                ((SamplyGame)Urho.Application.Current).accx = -1*e.Values[0]/6;
                    ((SamplyGame)Urho.Application.Current).accy = -1 * e.Values[1] / 6 + (float).6;
                ((SamplyGame)Urho.Application.Current).accz = e.Values[2];
            }
            }
        }




        protected override void OnPause()
        {
            UrhoSurface.OnPause();
            base.OnPause();
            _sensorManager.UnregisterListener(this);
        }


        protected override void OnResume()
		{
			UrhoSurface.OnResume();
			base.OnResume();
            _sensorManager.RegisterListener(this,
                                            _sensorManager.GetDefaultSensor(SensorType.Accelerometer),
                                            SensorDelay.Ui);
        }



		public override void OnLowMemory()
		{
			UrhoSurface.OnLowMemory();
			base.OnLowMemory();
		}

		protected override void OnDestroy()
		{
			UrhoSurface.OnDestroy();
			base.OnDestroy();
		}

		public override bool DispatchKeyEvent(KeyEvent e) 
		{
			if (!UrhoSurface.DispatchKeyEvent(e))
				return false;
			return base.DispatchKeyEvent(e);
		}

		public override void OnWindowFocusChanged(bool hasFocus)
		{
			UrhoSurface.OnWindowFocusChanged(hasFocus);
			base.OnWindowFocusChanged(hasFocus);
		}
	}
}