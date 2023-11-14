namespace Audio
{
    public class MusicController : AudioController
    {
        public static MusicController Instance { get; private set; }
        
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