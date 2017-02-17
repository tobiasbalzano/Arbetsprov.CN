namespace Arbetsprov.CN.Integrations.Generic
{
    public class ApiRequestObject<T>
    {
        public bool IsSuccess { get; set; }
        public T Content { get; set; }
    }
}
