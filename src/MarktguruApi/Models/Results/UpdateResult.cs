namespace MarktguruApi.Models.Results
{
    public struct UpdateResult<TModel>
    {
        public bool Success { get; }
        public TModel? Entry { get; }
        public string? ErrorMessage { get; }

        private UpdateResult(string errorMessage)
        {
            Success = false;
            ErrorMessage = errorMessage;
        }
        
        private UpdateResult(TModel entry)
        {
            Success = true;
            Entry = entry;
        }
        
        public static UpdateResult<TModel> CreateSuccess(TModel entry) => new UpdateResult<TModel>(entry);
        public static UpdateResult<TModel> CreateFailure(string errorMessage) => new UpdateResult<TModel>(errorMessage);
    }
}