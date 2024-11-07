using System;

namespace Environment.Spawning
{
    [Serializable]
    public struct Wave
    {
        public float timeUntilSpawn;
        public float spawningTime;
        public FallingObject[] fallingObjects;
    }
}