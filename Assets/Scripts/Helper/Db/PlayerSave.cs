using Ig.Model;

namespace Ig.Helpers.Db
{
    public class PlayerSave
    {
        public int Id { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotate { get; set; }
        
        public static implicit operator PlayerSave(PlayerModel model)
        {
            return new PlayerSave
            {
                Position = model.Position,
                Rotate = model.Rotation
            };
        }
        
        public static implicit operator PlayerModel(PlayerSave save)
        {
            return new PlayerModel
            {
                Position = save.Position,
                Rotation = save.Rotate
            };
        }
    }
}