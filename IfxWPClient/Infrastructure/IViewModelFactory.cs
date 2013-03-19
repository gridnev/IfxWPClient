namespace IfxWPClient.Infrastructure
{
    public interface IViewModelFactory
    {
        T Create<T>();
        void Release(object viewModel);
    }
}