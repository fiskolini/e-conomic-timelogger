namespace TimeLogger.Application.Common.Exceptions.Common
{
    public class ItemNotFoundException : NotFoundException
    {
        public ItemNotFoundException(int id) : base($"Item with id '{id}' was not found.")
        {
        }

        public ItemNotFoundException(string error) : base(error)
        {
        }
    }
}