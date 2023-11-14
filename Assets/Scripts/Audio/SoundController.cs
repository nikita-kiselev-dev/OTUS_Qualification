namespace Audio
{
    public class SoundController : AudioController
    {
        public static SoundController Instance { get; private set; }
        
        public void Awake()
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