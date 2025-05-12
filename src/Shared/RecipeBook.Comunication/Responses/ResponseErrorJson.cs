namespace RecipeBook.Comunication.Responses
{
    public class ResponseErrorJson
    {
        public IList<string> Errors { get; set; } = new List<string>(); /// <summary>Erros de validação</summary>
        public ResponseErrorJson(IList<string> errors) => Errors = errors;

        public ResponseErrorJson(string error) => Errors = new List<string> { error };
    }
}
