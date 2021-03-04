using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Data
{
    /// <summary>
    /// Class containing all the game constants
    /// Centralized to enable ease of updating values
    /// </summary>
    public class Constants
    {
        // paths
        public const string AchievementPath = @"\Achievement.bin";
        public const string PropertyPath = @"\Property.bin";
        public const string HighscorePath = @"\highscore.bin";

        // game constants
        public const float CloudSpeed = 0.1f;
        public const float RockSpeed = 0.05f;
        public const float SpeedFactor = 2.5f;
        public const float SpawnX = 25f; // the value of x for the spawning of new objects for the game
        public const float RockSpawnFrequency = 3f;
        public const float CloudSpawnFrequency = 2f;
        public const float MaxSpeed = 10f;
        public const float BackgroundSpeed = 2f;
        public const int StartingLives = 3;

        public const int NumberHighscoreEntries = 10;

        // player constants
        public const float PlayerSpeed = 0.2f;
        public const float PlayerSpriteSize = 40f;
        

        // string Const
        public const string PlayerObjectName = "Airship";

        // intro values
        public const float IntroSceneTime = 10;
    }
}
