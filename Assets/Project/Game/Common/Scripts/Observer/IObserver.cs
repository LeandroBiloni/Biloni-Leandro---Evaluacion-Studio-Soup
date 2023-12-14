namespace ObserverPattern
{
    public interface IObserver
    {
        void Notify<T>(string action, T observed);
    }
}

namespace ScoreSystem
{

    public interface IScoreService
    {
        ScoreManager GetScoreManager();
    }
}
