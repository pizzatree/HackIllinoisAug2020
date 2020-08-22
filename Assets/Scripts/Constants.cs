public static class Constants
{
    // Number of points required to revive all bases
    public const int POINTS_TO_REVIVE         = 1000;
    public const int DIFFICULTY_INCREASE_RATE = 3;
    
    // Low and high end of enemy spawn time basis.
    public const float ENEMY_BASE_SPAWN_TIME_LOW  = 2.5f;
    public const float ENEMY_BASE_SPAWN_TIME_HIGH = 3.25f;
    public const int   ENEMY_SPECIAL_SPAWN_RATE   = 25;
    
    // Basic enemy range L/R of center above screen to spawn
    public const float ENEMY_HORIZONTAL_SPAWN_RANGE = 6f;
    public const float ENEMY_VERTICAL_SPAWN_HEIGHT  = 7f;
}