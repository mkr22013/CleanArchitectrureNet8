using CleanArchitectrure.Application.UseCases.Commons.Bases;

namespace CleanArchitectrure.Application.Util
{
    /// <summary>
    /// Util class 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Util<T>
    {
        /// <summary>
        /// Generic method to generate the error
        /// </summary>
        /// <param name="response"></param>
        /// <param name="ex"></param>
        public static void GenerateResponse(BaseResponse<T> response, Exception ex)
        {
            var lstErrors = new List<BaseError>();
            var error = new BaseError() { ErrorMessage = Convert.ToString(ex.Message) };
            lstErrors.Add(error);
            response.Errors = lstErrors;
            response.Message = ex.Message;
        }
    }
}
