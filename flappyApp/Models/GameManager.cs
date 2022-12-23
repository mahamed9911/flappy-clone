
namespace FlappyPlane.Web.Game.Models
{
    public class GameManager
    {
        private readonly int _gravity = 2;

        public event EventHandler? MainLoopCompleted;

        public BirdModel Bird { get; set; }
        public List<PipeModel> Pipes { get; set; }
        public bool IsRunning { get; set; } = false;

        public GameManager()
        {
            Bird = new BirdModel();
            Pipes = new List<PipeModel>();
        }

        public async void MainLoop()
        {
            IsRunning = true;
            while(IsRunning)
            {
                MoveObjects();
                Collision();
                ManagePipes();

                MainLoopCompleted?.Invoke(this, EventArgs.Empty);
                
                await Task.Delay(20);
            }
        }

        public void StartGame()
        {
            if (!IsRunning)
            {
                Bird = new BirdModel();
                Pipes = new List<PipeModel>();
                MainLoop();
            }       
        }

        public void Jump()
        {
            if(IsRunning)
            {
                Bird.Jump();
            }
        }
        void Collision()
        {
            if (Bird.IsOnGround()) 
            {
                GameOver();
            }


            var centeredPipe = Pipes.FirstOrDefault(p => p.isCentered());

            if (centeredPipe != null)
            {
                bool hasCollidedBottom = Bird.DistanceFromGround < centeredPipe.GapBottom - 150;
                bool hasCollidedTop = Bird.DistanceFromGround + 45 > centeredPipe.GapTop - 150;

                if (hasCollidedBottom || hasCollidedTop)
                {
                    GameOver();
                }
            }
        }

        void ManagePipes()
        {
            if(!Pipes.Any() || Pipes.Last().DistanceFromLeft <= 250)
            {
                Pipes.Add(new PipeModel());
            }
            if(Pipes.First().OffScreen())
            {
                Pipes.Remove(Pipes.First());
            }
        }
        void MoveObjects()
        {
            Bird.Fall(_gravity);
            foreach (var pipe in Pipes)
            {
                pipe.Move();
            }
        }

        public void GameOver()
        {
            IsRunning = false;
        }
    }
}
