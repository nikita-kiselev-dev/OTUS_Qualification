namespace Audio
{
    public class SoundController : AudioController
    {
        public static SoundController Instance;
        
        public void Init()
        {
            if (Instance == null) 
            {
                Instance = this;
            } 
            else if(Instance == this)
            {
                Destroy(gameObject);
            }
        }
    }
}