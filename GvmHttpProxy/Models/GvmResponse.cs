namespace GvmHttpProxy.Models
{
    public class GvmResponse
    {
        public string Response { get; private set; }
        public string Error { get; private set; }

        public GvmResponse(string reponse, string error)
        {
            Response = reponse;
            Error = error;
        }
    }
}
