namespace Doc.Management.Common
{
    public abstract class ContextFilteredRequestBase
    {
        protected ContextFilteredRequestBase(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }
}
