namespace RecipeBook.Exceptions.ExceptionCustom
{
    public class ErrorOnValidationException : RecipeBookException
    {
        public IList<string> ErrorMessages { get; set; } = new List<string>();
        public ErrorOnValidationException(IList<string> errors)
        {
            ErrorMessages = errors;
        }
    }
}
