using Foundation;
using UIKit;
using Urho;
using Urho.iOS;
using System.Threading.Tasks;

using CoreGraphics;
using CoreMotion;

namespace SamplyGame.iOS
{


    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {

        private CMMotionManager motionManager = new CMMotionManager();

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
            {
            

            LaunchGame();
			return true;
		}

        

		async void LaunchGame()
		{
			await Task.Yield();
            var Game = new SamplyGame();


            motionManager.StartAccelerometerUpdates(NSOperationQueue.CurrentQueue, (data, error) =>
            {
          //      Game.accx = data.Acceleration.X;
           //     Game.accy = data.Acceleration.Y;
             //   Game.accz = data.Acceleration.Z;

                ((SamplyGame)Urho.Application.Current).accx = (float)data.Acceleration.X;
                ((SamplyGame)Urho.Application.Current).accy = (float)data.Acceleration.Y;
                ((SamplyGame)Urho.Application.Current).accz = (float)data.Acceleration.Z;

            });


            Game.Run();
		}


        
        

    }
}