namespace TodoList_Base.Shared
{
    public class UserMangerResonse
    {
        public UserMangerResonse(string Message, bool IsSuccess, DateTime? ExpireDate, string[] Errors = default)
        {
            this.Message = Message;
            this.IsSuccess = IsSuccess;
            this.ExpireDate = ExpireDate;
            this.Errors = Errors;
        }
        public UserMangerResonse(string Message, bool IsSuccess, DateTime ExpireDate) : this(Message, IsSuccess, ExpireDate, default)
        {

        }
        public UserMangerResonse(string Message, bool IsSuccess) : this(Message, IsSuccess, null)
        {

        }

        public UserMangerResonse(string message, bool isSuccess, int modelId) : this(message, isSuccess, null)
        {
            this.ModelId = modelId;
        }

        public UserMangerResonse()
        {

        }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public DateTime? ExpireDate { get; set; }
        public int ModelId { get; set; }
    }
}
