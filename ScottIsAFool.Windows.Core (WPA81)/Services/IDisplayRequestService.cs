namespace ScottIsAFool.Windows.Core.Services
{
    public interface IDisplayRequestService
    {
        void Request();
        void Release();
        bool IsActive { get; }
    }
}