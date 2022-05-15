namespace Gameplay.Interactive
{
    public interface ICharacter
    {
        public Characters CharacterType { get; }
    }

    public enum Characters
    {
        Player,
        Enemy
    }
}
