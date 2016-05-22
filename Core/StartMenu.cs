using System.Threading.Tasks;
using Urho;
using Urho.Gui;
using Urho.Actions;


namespace SamplyGame
{
	public class StartMenu : Component
	{
		TaskCompletionSource<bool> menuTaskSource;
		Node bigAircraft;
		Node rotor;
		Text textBlock;
		Node menuLight;
		bool finished = true;
        System.DateTime MenuClickDTS = System.DateTime.Now;

        public StartMenu()
		{
			ReceiveSceneUpdates = true;
		}

		public async Task ShowStartMenu()
		{
			var cache = Application.ResourceCache;
			bigAircraft = Node.CreateChild();
			var model = bigAircraft.CreateComponent<StaticModel>();
			model.Model = cache.GetModel(Assets.Models.Player);
			model.SetMaterial(cache.GetMaterial(Assets.Materials.Player).Clone(""));
			bigAircraft.SetScale(1.2f);
			bigAircraft.Rotate(new Quaternion(0, 220, 40), TransformSpace.Local);
			bigAircraft.Position = new Vector3(10, 2, 10);
			bigAircraft.RunActionsAsync(new RepeatForever(new Sequence(new RotateBy(1f, 0f, 0f, 5f), new RotateBy(1f, 0f, 0f, -5f))));

			//TODO: rotor should be defined in the model + animation
			rotor = bigAircraft.CreateChild();
			var rotorModel = rotor.CreateComponent<StaticModel>();
			rotorModel.Model = cache.GetModel(Assets.Models.Box);
			rotorModel.SetMaterial(cache.GetMaterial(Assets.Materials.Black));
			rotor.Scale = new Vector3(0.1f, 1.6f, 0.1f);
			rotor.Rotation = new Quaternion(0, 0, 0);
			rotor.Position = new Vector3(0, -0.15f, 1);
			rotor.RunActionsAsync(new RepeatForever(new RotateBy(1f, 0, 0, 360f * 3))); //RPM

			menuLight = bigAircraft.CreateChild();
			menuLight.Position = new Vector3(-3, 6, 2); 
			menuLight.AddComponent(new Light { LightType = LightType.Point, Range = 14, Brightness = 1f });

			await bigAircraft.RunActionsAsync(new EaseIn(new MoveBy(1f, new Vector3(-10, -2, -10)), 2));

			textBlock = new Text();
			textBlock.HorizontalAlignment = HorizontalAlignment.Center;
			textBlock.VerticalAlignment = VerticalAlignment.Bottom;
            if (Assets.Sounds.Muted)
            {
                textBlock.Value = "MUTED\n\n\nSTART MOTION\n\n\nSTART TOUCH";
            }
            else
            {
                textBlock.Value = "SOUND\n\n\nSTART MOTION\n\n\nSTART TOUCH";
            }
			
			textBlock.SetFont(cache.GetFont(Assets.Fonts.Font), Application.Graphics.Width / 18);
			Application.UI.Root.AddChild(textBlock);



            menuTaskSource = new TaskCompletionSource<bool>();
			finished = false;
			await menuTaskSource.Task;
		}

		protected override async void OnUpdate(float timeStep)
		{


                if (finished)
                    return;

            if (MenuClickDTS.AddSeconds(.1) > System.DateTime.Now)
                return;

            MenuClickDTS = System.DateTime.Now;


            var input = Application.Current.Input;
			if (input.GetMouseButtonDown(MouseButton.Left) || input.NumTouches > 0)
            {
                input.RemoveAllGestures();
                TouchState state = input.GetTouch(0);
                var touchPosition = state.Position;
                Vector3 destWorldPos = ((SamplyGame)Application).Viewport.ScreenToWorldPoint(touchPosition.X, touchPosition.Y, 10);

                if (destWorldPos.Y > -2 && destWorldPos.Y < 0)
                {
                        if (Assets.Sounds.BigExplosion.Length > 0)
                        {
                            Assets.Sounds.SoundOff();
                            textBlock.Value = "MUTED\n\n\nSTART MOTION\n\n\nSTART TOUCH";
                        }
                        else
                        {
                            Assets.Sounds.SoundOn();
                            textBlock.Value = "SOUND\n\n\nSTART MOTION\n\n\nSTART TOUCH";
                        }
                    
                    return;
                }

                else

                {
                    if (destWorldPos.Y >= -3 && destWorldPos.Y <= -2)
                    {
                        ((SamplyGame)Application.Current).useAccelerometer = true;
                    }
                    else if (destWorldPos.Y < -3)
                    {
                        ((SamplyGame)Application.Current).useAccelerometer = false;
                    }

                    finished = true;
                    Application.UI.Root.RemoveChild(textBlock, 0);
                    await bigAircraft.RunActionsAsync(new EaseIn(new MoveBy(1f, new Vector3(-10, -2, -10)), 3));
                    rotor.RemoveAllActions();
                    menuTaskSource.TrySetResult(true);
                }
                
            }
		}
	}
}
