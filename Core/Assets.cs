﻿namespace SamplyGame
{
	// Perhaps this file can be auto-generated by T4 script
	public static class Assets
	{
		public static class Materials
		{
			public const string Enemy1 = "Materials/Enemy1.xml";
			public const string Enemy2 = "Materials/Enemy2.xml";
			public const string Enemy3 = "Materials/Enemy3.xml";
			public const string Enemy3weapon = "Materials/Enemy3weapon.xml";
			public const string TreeMaterial = "Materials/TreeMaterial.xml";
			public const string Grass = "Materials/Grass.xml";
			public const string Player = "Materials/Player.xml";
			public const string Black = "Materials/Black.xml";
			public const string Coin = "Materials/Coin.xml";
			public const string MachineGun = "Materials/MachineGun.xml";
			public const string SMWeapon = "Materials/SMWeapon.xml";
		}

		public static class Models
		{
			public const string Tree = "Models/Tree.mdl";
			public const string Box = "Models/Box.mdl";
			public const string Plane = "Models/Plane.mdl";
			public const string Player = "Models/Player.mdl";
			public const string Enemy1 = "Models/Enemy1.mdl";
			public const string Enemy2 = "Models/Enemy2.mdl";
			public const string Enemy3 = "Models/Enemy3.mdl";
			public const string Enemy3weapon = "Models/Enemy3weapon.mdl";
			public const string Coin = "Models/Coin.mdl";
			public const string SMWeapon = "Models/SMWeapon.mdl";
		}

		public static class Particles
		{
			public const string Explosion = "Particles/Explosion.pex";
			public const string PlayerExplosion = "Particles/PlayerExplosion.pex";
			public const string HeavyMissileExplosion = "Particles/HeavyMissileExplosion.pex";
			public const string MissileTrace = "Particles/MissileTrace.pex";
		}

        public static class Sounds
        {
            public static string BigExplosion = "Sounds/BigExplosion.wav";
            public static string MachineGun = "Sounds/MachineGun.wav";
            public static string SmallExplosion = "Sounds/SmallExplosion.wav";
            public static string Powerup = "Sounds/Powerup.wav";
            public static bool Muted = false;

            public static void SoundOff()
            {
                BigExplosion = "";
                MachineGun = "";
                SmallExplosion = "";
                Powerup = "";
                Muted = true;
            }

            public static void SoundOn()
            {
                BigExplosion = "Sounds/BigExplosion.wav";
                MachineGun = "Sounds/MachineGun.wav";
                SmallExplosion = "Sounds/SmallExplosion.wav";
                Powerup = "Sounds/Powerup.wav";
                Muted = false;
            }
        }

        

		public static class Fonts
		{
			public const string Font = "Fonts/Font.ttf";
		}
	}
}
