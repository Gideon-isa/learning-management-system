namespace Lms.SharedKernel.Common.Wrappers
{
    public class ResponseWrapper : IResponseWrapper
    {
        public List<string> Messages { get; set; } = [];
        public bool IsSuccessful { get ; set; }

        public ResponseWrapper() { }

        // Flag - Failure
        public static IResponseWrapper Fail()
        {
            return new ResponseWrapper()
            {
                IsSuccessful = false
            };
        }

        public static IResponseWrapper Fail(List<string> message)
        {
            return new ResponseWrapper()
            {
                Messages = message,
                IsSuccessful = false
            };
        }

        public static IResponseWrapper Fail(string message)
        {
            return new ResponseWrapper()
            {
                Messages = [message],
                IsSuccessful = false
            };
        }

        public static Task<IResponseWrapper> FailAsync() => Task.FromResult(Fail());
        public static Task<IResponseWrapper> FailAsync(string message) => Task.FromResult(Fail(message));
        public static Task<IResponseWrapper> FailAsync(List<string> message) => Task.FromResult(Fail(message ));


        // Flag - Success
        public static IResponseWrapper Success()
        {
            return new ResponseWrapper() { IsSuccessful = true };
        }

        public static IResponseWrapper Success(string message)
        {
            return new ResponseWrapper()
            {
                Messages = [message],
                IsSuccessful = true
            };
        }

        public static IResponseWrapper Success(List<string> messages)
        {
            return new ResponseWrapper()
            {
                Messages = messages,
                IsSuccessful = true
            };
        }

        public static Task<IResponseWrapper> SuccessAsync() => Task.FromResult(Success());
        public static Task<IResponseWrapper> SuccessAsync(string message) => Task.FromResult(Success(message));
        public static Task<IResponseWrapper> SuccessAsync(List<string> messages) => Task.FromResult(Success(messages));



    }
    public class ResponseWrapper<T> : ResponseWrapper, IResponseWrapper<T>
    {
        public T Data { get; set; }
        public ResponseWrapper()
        {
            
        }

        public new static ResponseWrapper<T> Fail() => new() { IsSuccessful = false };

        public new static ResponseWrapper<T> Fail(string message) => new() { IsSuccessful = false, Messages = [message] };

        public new static ResponseWrapper<T> Fail(List<string> messages) => new() { IsSuccessful = false, Messages = messages };
        public new static Task<ResponseWrapper<T>> FailAsync() => Task.FromResult(Fail());
        public new static Task<ResponseWrapper<T>> FailAsync(string message) => Task.FromResult(Fail(message));
        public new static Task<ResponseWrapper<T>> FailAsync(List<string> messages) => Task.FromResult(Fail(messages));

        public new static ResponseWrapper<T> Success() => new() { IsSuccessful = true };
        public new static ResponseWrapper<T> Success(string message) => new() { IsSuccessful = true, Messages = [message] };
        public static ResponseWrapper<T> Success(T data) => new() { IsSuccessful = true, Data = data }; 
        public static ResponseWrapper<T> Success(T data, string message) 
            => new() {IsSuccessful = true, Data = data, Messages = [message] };

        public static ResponseWrapper<T> Success(T data, List<string> messages)
            => new() { IsSuccessful = true, Data = data, Messages = messages };

        public new static Task<ResponseWrapper<T>> SuccessAsync() => Task.FromResult(Success());
        public new static Task<ResponseWrapper<T>> SuccessAsync(string message) => Task.FromResult(Success(message));
        public static Task<ResponseWrapper<T>> SuccessAsync(T data) => Task.FromResult(Success(data));
        public static Task<ResponseWrapper<T>> SuccessAsync(T data, List<string> messages) => Task.FromResult(Success(data, messages));

    }
}
 