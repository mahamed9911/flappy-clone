namespace FlappyPlane.Web.Game.Models
{
    public class BirdModel
    {
        public int DistanceFromGround { get; set; } = 100;
        public int JumpStrength { get; set; } = 50;

        public bool IsOnGround()
        {
            return DistanceFromGround <= 0;
        }

        public void Fall(int gravity)
        {
            DistanceFromGround -= gravity;
        }

        public void Jump()
        {
            if(DistanceFromGround <= 520)
            {
                DistanceFromGround += JumpStrength;
            }
        }
    }
}
