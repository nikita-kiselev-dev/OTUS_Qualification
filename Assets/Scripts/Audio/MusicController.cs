namespace Audio
{
    public class MusicController : AudioController
    {
        public static MusicController Instance;
        
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