public interface IPlaySound
{
    public void Play();
    public void Play(float delay);
}

public interface IAgent
{
    public void SetBehaviorState(int behavior);
}