namespace MarktguruApi.Models.Results
{
    public struct CreateResult<TModel>
    {
        public bool Success { get; }
        public TModel? Entry { get; }
        public string? ErrorMessage { get; }

        private CreateResult(TModel? entry)
        {
            Success = true;
            Entry = entry;
        }

        private CreateResult(string? errorMessage)
        {
            Success = false;
            ErrorMessage = errorMessage;
        }

        public static CreateResult<TModel?> CreateSuccess(TModel entry) => new CreateResult<TModel?>(entry);
        public static CreateResult<TModel> CreateFailure(string? errorMessage) => new CreateResult<TModel>(errorMessage);
    }
}