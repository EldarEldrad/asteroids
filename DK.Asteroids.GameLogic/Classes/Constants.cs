namespace DK.Asteroids.GameLogic.Classes
{
    public static class Constants
    {
        public static class UITexts
        {
            public const string LaserChargesUIText = "Заряды лазера: {0}";
            public const string ScoresUIText = "Очки: {0}\nРекорд: {1}";

            public const string NewRecordUIText = "\nВы установили новый рекорд!";
            public const string DestroyedUIText = "Вы уничтожены!\nВы набрали {0} очков!\nНажмите \"Enter\" чтобы начать заново";
        }

        public static class Inputs
        {
            public const string Restart = "Restart";
            public const string Representation = "Representation";
            public const string ExitButton = "ExitButton";
            public const string BulletShot = "BulletShot";
            public const string LaserShot = "LaserShot";
            public const string Horizontal = "Horizontal";
            public const string Vertical = "Vertical";
        }

        public static class Tags
        {
            public const string MainCamera = "MainCamera";
            public const string Background = "Background";
            public const string Boundary = "Boundary";
            public const string Bullet = "Bullet";
            public const string Laser = "Laser";
            public const string Player = "Player";
        }
    }
}
